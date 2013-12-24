using System;
using System.IO;
#if __ANDROID
using Android.App;
#endif


namespace Acr.MvvmCross.Plugins.Storage.Impl {
    
    public class StorageServiceImpl : IStorageService {
        private readonly string localPath;

#if __ANDROID
        public StorageServiceImpl() {
            this.localPath = Application.Context.FilesDir.Path;
            //this.externalDirectory = Env.GetExternalStoragePublicDirectory(Env.DirectoryDownloads).Path;
        }
#elif __IOS__
        public StorageServiceImpl() {
            this.localPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }
#endif

        #region IStorageService Members

        public string NativePath(string path) {
            if (!path.StartsWith("/")) {
                path = Path.Combine(this.localPath, path);
            }
            return path;
        }


        public IFileInfo GetFile(string path) {
            var fullPath = this.NativePath(path);
            return new FileInfoImpl(new FileInfo(fullPath));
        }


        public IDirectoryInfo GetDirectory(string path) {
            var fullPath = this.NativePath(path);
            return new DirectoryInfoImpl(new DirectoryInfo(fullPath));
        }


        public bool FileExists(string path) {
            var fullPath = this.NativePath(path);
            return File.Exists(fullPath);
        }


        public void DeleteFile(string file) {
            var fullPath = this.NativePath(file);
            File.Delete(fullPath);
        }


        public void MoveFile(string source, string destination) {
            var fullSourcePath = this.NativePath(source);
            var fullDestPath = this.NativePath(destination);
            File.Move(fullSourcePath, fullDestPath);
        }


        public void CopyFile(string source, string destination, bool overwrite) {
            var fullSourcePath = this.NativePath(source);
            var fullDestPath = this.NativePath(destination);
            File.Copy(fullSourcePath, fullDestPath, overwrite);            
        }


        public Stream OpenReadFileStream(string path) {
            var file = this.GetFile(path);
            return file.OpenRead();
        }


        public Stream OpenWriteFileStream(string path) {
            var file = this.GetFile(path);
            return file.OpenWrite();
        }

        #endregion
    }
}
