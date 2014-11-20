using System;
using System.IO;
using System.Threading.Tasks;


namespace Acr.MvvmCross.Plugins.BarCodeScanner {

    public interface IBarCodeScanner {

        BarCodeScannerConfiguration Configuration { get; }

        void Read(Action<BarCodeResult> onRead);
        Task<BarCodeResult> ReadAsync();

        Stream Create(BarCodeFormat format, string content, int height, int width, int margin = 0, bool pureBarCode = false);
    }
}
