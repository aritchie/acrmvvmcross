using System;
using System.Linq;
using System.Threading.Tasks;
#if WINDOWS_PHONE
using System.Windows;
#elif ANDROID
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid.Platform;
#endif
using ZXing;
using ZXing.Mobile;


namespace Acr.MvvmCross.Plugins.BarCodeScanner {
    
#if __IOS__
    public class TouchBarCodeScanner : IBarCodeScanner {
#elif ANDROID
    public class DroidBarCodeScanner : IBarCodeScanner {
#elif WINDOWS_PHONE
    public class WinPhoneBarCodeScanner : IBarCodeScanner {
#endif

        public async Task<BarCodeResult> Read(string topText, string bottomText, string flashlightText, string cancelText, BarCodeScanningConfig config) {
#if __IOS__
            var scanner = new MobileBarcodeScanner { UseCustomOverlay = false };
#elif ANDROID
            var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
            var scanner = new MobileBarcodeScanner(activity) { UseCustomOverlay = false };
#elif WINDOWS_PHONE
            var scanner = new MobileBarcodeScanner(Deployment.Current.Dispatcher) { UseCustomOverlay = false };
#endif

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
            var result = await scanner.Scan(GetXingConfig(config));
            return (result == null || String.IsNullOrWhiteSpace(result.Text)
                ? BarCodeResult.Fail
                : new BarCodeResult(result.Text, FromXingFormat(result.BarcodeFormat))
            );
        }


        private static BarCodeFormat FromXingFormat(ZXing.BarcodeFormat format) {
            return (BarCodeFormat)Enum.Parse(typeof(BarCodeFormat), format.ToString());
        }


        private static MobileBarcodeScanningOptions GetXingConfig(BarCodeScanningConfig cfg) {
            var def = ZXing.Mobile.MobileBarcodeScanningOptions.Default;
            if (cfg == null)
                return def;

            var config = new MobileBarcodeScanningOptions {
                AutoRotate = cfg.AutoRotate,
                CharacterSet = cfg.CharacterSet ?? def.CharacterSet,
                DelayBetweenAnalyzingFrames = cfg.DelayBetweenAnalyzingFrames ?? def.DelayBetweenAnalyzingFrames,
                InitialDelayBeforeAnalyzingFrames = (cfg.InitialDelayBeforeAnalyzingFrames ?? def.InitialDelayBeforeAnalyzingFrames),
                PureBarcode = cfg.PureBarcode,
                TryHarder = cfg.TryHarder,
                TryInverted = cfg.TryInverted,
                UseFrontCameraIfAvailable = cfg.UseFrontCameraIfAvailable
            };
            if (cfg.Formats != null && cfg.Formats.Count > 0) {
                config.PossibleFormats = cfg.Formats
                    .Select(x => (BarcodeFormat)(int)x)
                    .ToList();
            }
            return config;
        }
    }
}
