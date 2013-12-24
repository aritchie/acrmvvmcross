using System;
using MonoTouch.UIKit;


namespace Acr.MvvmCross.Plugins.DeviceInfo.Touch {
    
    public class TouchDeviceInfoService : IDeviceInfoService {
        
        public TouchDeviceInfoService() {
            this.Manufacturer = "Apple";
            this.Model = UIDevice.CurrentDevice.Model;
            this.OperatingSystem = String.Format("{0} {1}", UIDevice.CurrentDevice.SystemName, UIDevice.CurrentDevice.SystemVersion);

            var screen = UIScreen.MainScreen.Bounds;
            this.ScreenWidth = (int)screen.Width;
            this.ScreenHeight = (int)screen.Height;
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