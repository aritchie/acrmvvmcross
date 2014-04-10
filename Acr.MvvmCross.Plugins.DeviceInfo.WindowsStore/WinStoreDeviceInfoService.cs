using System;
using System.Linq;
using Windows.Devices.Enumeration;
using Windows.System.Profile;
using Windows.UI.Xaml;


namespace Acr.MvvmCross.Plugins.DeviceInfo.WindowsStore {

    public class WinStoreDeviceInfoService : AbstractDeviceInfoService {

        public WinStoreDeviceInfoService() {
            this.ScreenHeight = Convert.ToInt32(Window.Current.Bounds.Height);
            this.ScreenWidth = Convert.ToInt32(Window.Current.Bounds.Width);

            //this.Manufacturer = DeviceStatus.DeviceManufacturer; 
            //this.Model = DeviceStatus.DeviceName;
            //this.OperatingSystem = Environment.OSVersion.ToString();

            var cameras = DeviceInformation
                .FindAllAsync(DeviceClass.VideoCapture)
                .GetResults();
            
            this.IsRearCameraAvailable = cameras.Any();
            this.IsFrontCameraAvailable = (cameras.Count > 1);

            this.DeviceId = GetHardwareId();
            //new MediaCapture().VideoDeviceController.
        }


        private static string GetHardwareId() {
            var token = HardwareIdentification.GetPackageSpecificToken(null);
            var hardwareId = token.Id;
            var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(hardwareId);

            var bytes = new byte[hardwareId.Length];
            dataReader.ReadBytes(bytes);

            return BitConverter.ToString(bytes);
        }
    }
}
