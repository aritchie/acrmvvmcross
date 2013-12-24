using System;
using System.Threading.Tasks;


namespace Acr.MvvmCross.Plugins.BarCodeScanner {

    public interface IBarCodeScanner {

        Task<BarCodeResult> Read(
            string topText = "Hold the camera up to the barcode\nAbout 6 inches away", 
            string bottomText = "Wait for the barcode to automatically scan", 
            string flashlightText = null,
            string cancelText = null
        );
    }
}
