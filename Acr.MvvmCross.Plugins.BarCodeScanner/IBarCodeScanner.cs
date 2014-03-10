using System;
using System.Threading.Tasks;


namespace Acr.MvvmCross.Plugins.BarCodeScanner {

    public interface IBarCodeScanner {

        BarCodeScannerOptions DefaultOptions { get; }

        // TODO: none async?
        //void Read(Action<BarCodeResult> onRead, BarCodeScannerOptions options = null);
        //Task<BarCodeResult> ReadAsync(BarCodeScannerOptions options = null);
        Task<BarCodeResult> Read(BarCodeScannerOptions options = null);
    }
}
