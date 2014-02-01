using System;
using Android.Content.PM;
using Android.Content.Res;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid;
using B = Android.OS.Build;


namespace Acr.MvvmCross.Plugins.DeviceInfo.Droid {
    
    public class DroidDeviceInfoService : AbstractDeviceInfoService {
        
        public DroidDeviceInfoService() {
            this.Manufacturer = B.Manufacturer;
            this.Model = B.Model;
            this.OperatingSystem = String.Format("{0} - SDK: {1}", B.VERSION.Release, B.VERSION.SdkInt);
            this.IsSimulator = B.Product.Equals("google_sdk");

            var d = Resources.System.DisplayMetrics;
            this.ScreenWidth = (int)(d.WidthPixels / d.Density);
            this.ScreenHeight = (int)(d.HeightPixels / d.Density);
             Mvx.CallbackWhenRegistered<IMvxAndroidGlobals>(x => {
                var m = x.ApplicationContext.PackageManager;
                this.IsRearCameraAvailable = m.HasSystemFeature(PackageManager.FeatureCamera);
                this.IsFrontCameraAvailable = m.HasSystemFeature(PackageManager.FeatureCameraFront);
            });
        }
    }
}