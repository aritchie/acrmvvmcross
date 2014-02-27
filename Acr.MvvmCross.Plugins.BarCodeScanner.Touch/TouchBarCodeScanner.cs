using System;
using System.Linq;
using System.Threading.Tasks;
using ZXing;
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

        
        public void SetConfiguration(BarCodeScanningConfig cfg) {
            var def = ZXing.Mobile.MobileBarcodeScanningOptions.Default;
            def.AutoRotate = cfg.AutoRotate;
            if (!String.IsNullOrWhiteSpace(cfg.CharacterSet)) {
                def.CharacterSet = cfg.CharacterSet;
            }
            def.DelayBetweenAnalyzingFrames = cfg.DelayBetweenAnalyzingFrames ?? def.DelayBetweenAnalyzingFrames;
            if (cfg.Formats != null && cfg.Formats.Count > 0) {
                def.PossibleFormats = cfg.Formats
                    .Select(x => (BarcodeFormat)(int)x)
                    .ToList();
            }
            def.PureBarcode = cfg.PureBarcode;
            def.InitialDelayBeforeAnalyzingFrames = (cfg.InitialDelayBeforeAnalyzingFrames ?? def.InitialDelayBeforeAnalyzingFrames);
            def.TryHarder = cfg.TryHarder;
            def.TryInverted = cfg.TryInverted;
            def.UseFrontCameraIfAvailable = cfg.UseFrontCameraIfAvailable;
        }
    }
}
