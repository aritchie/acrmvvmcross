using System;


namespace Acr.MvvmCross.Plugins.DeviceInfo.WindowsStore {

    public class WinStoreDeviceInfoService : IDeviceInfoService {

        //public WinStoreDeviceInfoService() {
        //    this.ScreenHeight = Convert.ToInt32(Window.Current.Bounds.Height);
        //    this.ScreenWidth = Convert.ToInt32(Window.Current.Bounds.Width);

        //    //this.Manufacturer = DeviceStatus.DeviceManufacturer; 
        //    //this.Model = DeviceStatus.DeviceName;
        //    //this.OperatingSystem = Environment.OSVersion.ToString();

        //    var cameras = DeviceInformation
        //        .FindAllAsync(DeviceClass.VideoCapture)
        //        .GetResults();
            
        //    this.IsRearCameraAvailable = cameras.Any();
        //    this.IsFrontCameraAvailable = (cameras.Count > 1);

        //    this.DeviceId = GetHardwareId();
        //    //new MediaCapture().VideoDeviceController.
        //}


        //private static string GetHardwareId() {
        //    var token = HardwareIdentification.GetPackageSpecificToken(null);
        //    var hardwareId = token.Id;
        //    var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(hardwareId);

        //    var bytes = new byte[hardwareId.Length];
        //    dataReader.ReadBytes(bytes);

        //    return BitConverter.ToString(bytes);
        //}
        #region IDeviceInfoService Members

        public int ScreenHeight {
            get { throw new NotImplementedException(); }
        }

        public int ScreenWidth {
            get { throw new NotImplementedException(); }
        }

        public string DeviceId {
            get { throw new NotImplementedException(); }
        }

        public string Manufacturer {
            get { throw new NotImplementedException(); }
        }

        public string Model {
            get { throw new NotImplementedException(); }
        }

        public string OperatingSystem {
            get { throw new NotImplementedException(); }
        }

        public bool IsFrontCameraAvailable {
            get { throw new NotImplementedException(); }
        }

        public bool IsRearCameraAvailable {
            get { throw new NotImplementedException(); }
        }

        public bool IsSimulator {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
