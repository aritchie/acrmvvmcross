using System;
using System.Threading.Tasks;
using ZXing.Mobile;


namespace Acr.MvvmCross.Plugins.BarCodeScanner.Touch {

    public class TouchBarCodeScanner : IBarCodeScanner {

        public async Task<BarCodeResult> Read(string topText, string bottomText, string flashlightText, string cancelText) {
            var scanner = new MobileBarcodeScanner { UseCustomOverlay = false };
            if (!String.IsNullOrWhiteSpace(topText)) {
                scanner.TopText = topText;
            }
            if (!String.IsNullOrWhiteSpace(bottomText)) {
                scanner.BottomText = bottomText;
            }
            if (!String.IsNullOrWhiteSpace(flashlightText)) {
                scanner.FlashButtonText = flashlightText;
            }
            if (!String.IsNullOrWhiteSpace(cancelText)) {
                scanner.CancelButtonText = cancelText;
            }

            var result = await scanner.Scan();
            return (result == null || String.IsNullOrWhiteSpace(result.Text)
                ? BarCodeResult.Fail
                : new BarCodeResult(result.Text, result.BarcodeFormat.ToString())
            );
        }
    }
}
