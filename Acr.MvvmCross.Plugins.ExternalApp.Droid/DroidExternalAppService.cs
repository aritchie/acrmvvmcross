using System;
using System.IO;
using Android.Content;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid;
using Cirrious.CrossCore.Droid.Platform;


namespace Acr.MvvmCross.Plugins.ExternalApp.Droid {

    public class DroidExternalAppService : MvxAndroidTask, IExternalAppService {
        private readonly string externalDirectory;


        public DroidExternalAppService() {
            var globals = Mvx.Resolve<IMvxAndroidGlobals>();
            this.externalDirectory = globals.ApplicationContext.GetExternalFilesDir(null).AbsolutePath;
        }


        public bool Open(string fileName) {
            if (String.IsNullOrWhiteSpace(fileName))
                return false;

            var dataType = GetMimeType(fileName);

            // external apps do not have access to cache directory, copy from the cache to an external location
            var newPath = Path.Combine(
                this.externalDirectory,
                Path.GetFileName(fileName)
            );
            File.Copy(fileName, newPath, true);

            try {
                var file = new Java.IO.File(newPath);
                var uri = Android.Net.Uri.FromFile(file);
                var intent = new Intent(Intent.ActionView);
                intent.SetDataAndType(uri, dataType);
                this.StartActivity(intent);
                return true;
            }
            catch (Exception ex) {
                Mvx.Warning(ex.ToString());
                return false;
            }
        }


        private static string GetMimeType(string fileName) {
            var ext = Path.GetExtension(fileName).ToLower();
            switch (ext) {
                case ".pdf"  : return "application/pdf";
                case ".docx" :
                case ".doc"  : return "application/msword";
                case ".xlsx" :
                case ".xls"  : return "application/excel";
                case ".jpeg" : 
                case ".jpg"  :
                case ".bmp"  :
                case ".gif"  :
                case ".png"  : return "image/*";
                default      : return "application/*";
            }
        }
    }
}
