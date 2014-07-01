using System;
using System.Threading.Tasks;


namespace Acr.MvvmCross.Plugins.BarCodeScanner {

    public interface IBarCodeScanner {

        BarCodeScannerConfiguration Configuration { get; }
        
        void Read(Action<BarCodeResult> onRead);
        Task<BarCodeResult> ReadAsync();
    }
}
