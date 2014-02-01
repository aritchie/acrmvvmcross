using System;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;


namespace Acr.MvvmCross.Plugins.DeviceInfo.Touch {
    
    public class TouchDeviceInfoService : AbstractDeviceInfoService {
        
        public TouchDeviceInfoService() {
            this.Manufacturer = "Apple";
            this.Model = UIDevice.CurrentDevice.Model;
            this.OperatingSystem = String.Format("{0} {1}", UIDevice.CurrentDevice.SystemName, UIDevice.CurrentDevice.SystemVersion);

            var screen = UIScreen.MainScreen.Bounds;
            this.ScreenWidth = (int)screen.Width;
            this.ScreenHeight = (int)screen.Height;
            this.IsFrontCameraAvailable = UIImagePickerController.IsCameraDeviceAvailable(UIImagePickerControllerCameraDevice.Front);
            this.IsRearCameraAvailable = UIImagePickerController.IsCameraDeviceAvailable(UIImagePickerControllerCameraDevice.Rear);
            this.IsSimulator = (Runtime.Arch == Arch.SIMULATOR);
        }
    }
}