using System;
using Android.Content.Res;
using B = Android.OS.Build;


namespace Acr.MvvmCross.Plugins.DeviceInfo.Droid {
    
    public class DroidDeviceInfoService : IDeviceInfoService {
        
        public DroidDeviceInfoService() {
            this.Manufacturer = B.Manufacturer;
            this.Model = B.Model;
            this.OperatingSystem = String.Format("{0} - SDK: {1}", B.VERSION.Release, B.VERSION.SdkInt);
                
            var d = Resources.System.DisplayMetrics;
            this.ScreenWidth = (int)(d.WidthPixels / d.Density);
            this.ScreenHeight = (int)(d.HeightPixels / d.Density);
        }

        #region IDeviceInfoService Members

        public int ScreenHeight { get; private set; }
        public int ScreenWidth { get; private set; }
        public string OperatingSystem { get; private set; }
        public string Manufacturer { get; private set; }
        public string Model { get; private set; }

        #endregion
    }
}