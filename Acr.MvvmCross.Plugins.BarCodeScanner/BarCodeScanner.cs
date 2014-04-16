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

        public TouchBarCodeScanner() {
            this.DefaultOptions = new BarCodeScannerOptions();
        }

#elif ANDROID
    public class DroidBarCodeScanner : IBarCodeScanner {

        public DroidBarCodeScanner() {
            this.DefaultOptions = new BarCodeScannerOptions();
        }

#elif WINDOWS_PHONE
    public class WinPhoneBarCodeScanner : IBarCodeScanner {

        public WinPhoneBarCodeScanner() {
            this.DefaultOptions = new BarCodeScannerOptions();
        }
#endif

        public BarCodeScannerOptions DefaultOptions { get; private set; }


        public void Read(Action<BarCodeResult> onRead, Action<Exception> onError, BarCodeScannerOptions options) {
            this.ReadAsync(options)
                .ContinueWith(x => {
                    if (x.Exception == null)
                        onRead(x.Result);
                    else if (onError != null)
                        onError(x.Exception);
                });
        }

        //public Task<BarCodeResult> ReadAsync(string topText, string bottomText, string flashlightText, string cancelText) {
        //    var opts = new BarCodeScannerOptions();

        //    // leave defaults
        //    opts.TopText = topText ?? opts.TopText;
        //    opts.BottomText = bottomText ?? opts.BottomText;
        //    opts.FlashlightText = flashlightText ?? opts.FlashlightText; 
        //    opts.CancelText = cancelText ?? opts.CancelText;

        //    return this.ReadAsync(opts);
        //}


        public async Task<BarCodeResult> ReadAsync(BarCodeScannerOptions options) {
#if __IOS__
            var scanner = new MobileBarcodeScanner { UseCustomOverlay = false };
#elif ANDROID
            var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
            var scanner = new MobileBarcodeScanner(activity) { UseCustomOverlay = false };
#elif WINDOWS_PHONE
            var scanner = new MobileBarcodeScanner(Deployment.Current.Dispatcher) { UseCustomOverlay = false };
#endif
            options = options ?? this.DefaultOptions;
            if (!String.IsNullOrWhiteSpace(options.TopText)) 
                scanner.TopText = options.TopText;
            
            if (!String.IsNullOrWhiteSpace(options.BottomText)) 
                scanner.BottomText = options.BottomText;
            
            if (!String.IsNullOrWhiteSpace(options.FlashlightText)) 
                scanner.FlashButtonText = options.FlashlightText;
            
            if (!String.IsNullOrWhiteSpace(options.CancelText)) 
                scanner.CancelButtonText = options.CancelText;

            var cfg = GetXingConfig(options);
            var result = await scanner.Scan(cfg);
            return (result == null || String.IsNullOrWhiteSpace(result.Text)
                ? BarCodeResult.Fail
                : new BarCodeResult(result.Text, FromXingFormat(result.BarcodeFormat))
            );
        }


        private static BarCodeFormat FromXingFormat(ZXing.BarcodeFormat format) {
            return (BarCodeFormat)Enum.Parse(typeof(BarCodeFormat), format.ToString());
        }


        private static MobileBarcodeScanningOptions GetXingConfig(BarCodeScannerOptions opts) {
            var def = ZXing.Mobile.MobileBarcodeScanningOptions.Default;

            var config = new MobileBarcodeScanningOptions {
                AutoRotate = def.AutoRotate,
                CharacterSet = opts.CharacterSet ?? def.CharacterSet,
                DelayBetweenAnalyzingFrames = opts.DelayBetweenAnalyzingFrames ?? def.DelayBetweenAnalyzingFrames,
                InitialDelayBeforeAnalyzingFrames = (opts.InitialDelayBeforeAnalyzingFrames ?? def.InitialDelayBeforeAnalyzingFrames),
                PureBarcode = opts.PureBarcode,
                TryHarder = opts.TryHarder,
                TryInverted = opts.TryInverted,
                UseFrontCameraIfAvailable = opts.UseFrontCameraIfAvailable
            };
            if (opts.Formats != null && opts.Formats.Count > 0) {
                config.PossibleFormats = opts.Formats
                    .Select(x => (BarcodeFormat)(int)x)
                    .ToList();
            }
            return config;
        }
    }
}
