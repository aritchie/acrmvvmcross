using System;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid.Platform;
using ZXing.Mobile;


namespace Acr.MvvmCross.Plugins.BarCodeScanner.Droid {

    public class DroidBarCodeScanner : IBarCodeScanner {

        public async Task<BarCodeResult> Read(string topText, string bottomText, string flashlightText, string cancelText) {
            var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
            var scanner = new MobileBarcodeScanner(activity) {
                UseCustomOverlay = false,
                TopText = topText,
                BottomText = bottomText,
                FlashButtonText = flashlightText,
                CancelButtonText = cancelText
            };

            var result = await scanner.Scan();
            return (result == null || String.IsNullOrWhiteSpace(result.Text)
                ? BarCodeResult.Fail
                : new BarCodeResult(result.Text, result.BarcodeFormat.ToString())
            );
        }
    }
}
