using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
#if __ANDROID__
using Android.Graphics;
#elif WINDOWS_PHONE
using System.Windows.Media.Imaging;
#endif
using Cirrious.CrossCore;
using ZXing;
using ZXing.Common;
using ZXing.Mobile;


namespace Acr.MvvmCross.Plugins.BarCodeScanner {

    public class BarCodeScanner : IBarCodeScanner {

        public BarCodeScannerConfiguration Configuration { get; private set; }


        public BarCodeScanner() {
            var def = ZXing.Mobile.MobileBarcodeScanningOptions.Default;

            this.Configuration = new BarCodeScannerConfiguration {
                AutoRotate = def.AutoRotate,
                CharacterSet = def.CharacterSet,
                DelayBetweenAnalyzingFrames = def.DelayBetweenAnalyzingFrames,
                InitialDelayBeforeAnalyzingFrames = def.InitialDelayBeforeAnalyzingFrames,
                PureBarcode = def.PureBarcode,
                TryHarder = def.TryHarder,
                TryInverted = def.TryInverted,
                UseFrontCameraIfAvailable = def.UseFrontCameraIfAvailable
            };
        }


        public void Read(Action<BarCodeResult> onRead) {
            this.ReadAsync().ContinueWith(x => onRead(x.Result));
        }


        public Stream CreateBarCode(BarCodeFormat format, string content, int height, int width, int margin, bool pureBarCode) {
            // code 128, aztec,  - codeset B, height, width, margin, pure barcode
            // qr - charset, disableECI, 
            var writer = new BarcodeWriter {
                Format = (BarcodeFormat)Enum.Parse(typeof(BarcodeFormat), format.ToString()),
                Encoder = new MultiFormatWriter(),
                Options = new EncodingOptions {
                    Height = height,
                    Margin = margin,
                    Width = width,
                    PureBarcode = pureBarCode
                }
            };
#if __IOS__
            return writer.Write(content).AsPNG().AsStream();
#elif __ANDROID__
            var ms = new MemoryStream();
            using (var bitmap = writer.Write(content))
                bitmap.Compress(Bitmap.CompressFormat.Png, 100, ms);

            return ms;
#elif WINDOWS_PHONE
            return new MemoryStream(writer.Write(content).ToByteArray());
#endif
        }


        public async Task<BarCodeResult> ReadAsync() {
#if __IOS__
            var scanner = new MobileBarcodeScanner { UseCustomOverlay = false };
#elif __ANDROID__
            var topActivity = Mvx.Resolve<Cirrious.CrossCore.Droid.Platform.IMvxAndroidCurrentTopActivity>().Activity;
            var scanner = new MobileBarcodeScanner(topActivity) { UseCustomOverlay = false };
#elif WINDOWS_PHONE
            var scanner = new MobileBarcodeScanner(System.Windows.Deployment.Current.Dispatcher) { UseCustomOverlay = false };
#endif
            var result = await scanner.Scan(this.GetXingConfig());
            return (result == null || String.IsNullOrWhiteSpace(result.Text)
                ? BarCodeResult.Fail
                : new BarCodeResult(result.Text, FromXingFormat(result.BarcodeFormat))
            );
        }


        private static BarCodeFormat FromXingFormat(ZXing.BarcodeFormat format) {
            return (BarCodeFormat)Enum.Parse(typeof(BarCodeFormat), format.ToString());
        }


        private MobileBarcodeScanningOptions GetXingConfig() {
            var opts = new MobileBarcodeScanningOptions {
                AutoRotate = this.Configuration.AutoRotate,
                CharacterSet = this.Configuration.CharacterSet,
                DelayBetweenAnalyzingFrames = this.Configuration.DelayBetweenAnalyzingFrames,
                InitialDelayBeforeAnalyzingFrames = this.Configuration.InitialDelayBeforeAnalyzingFrames,
                PureBarcode = this.Configuration.PureBarcode,
                TryHarder = this.Configuration.TryHarder,
                TryInverted = this.Configuration.TryInverted,
                UseFrontCameraIfAvailable = this.Configuration.UseFrontCameraIfAvailable
            };

            if (this.Configuration.Formats != null && this.Configuration.Formats.Count > 0) {
                opts.PossibleFormats = this.Configuration.Formats
                    .Select(x => (BarcodeFormat)(int)x)
                    .ToList();
            }
            return opts;
        }
    }
}
