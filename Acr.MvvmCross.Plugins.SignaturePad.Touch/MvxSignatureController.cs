using System;
using System.IO;
using System.Linq;
using System.Drawing;
using UIKit;
using Foundation;
using Cirrious.MvvmCross.Plugins.Color;
using Cirrious.MvvmCross.Plugins.Color.Touch;


namespace Acr.MvvmCross.Plugins.SignaturePad.Touch {

    public class MvxSignatureController : UIViewController {

        private MvxSignatureView view;
        private readonly Action<SignatureResult> onResult;
        private readonly SignaturePadConfiguration config;


        public MvxSignatureController(SignaturePadConfiguration config, Action<SignatureResult> onResult) {
            this.config = config;
            this.onResult = onResult;
        }


        public override void LoadView() {
            base.LoadView();

            this.view = new MvxSignatureView();
            this.View = this.view;
        }


        public override void ViewDidLoad() {
            base.ViewDidLoad();

            this.view.BackgroundColor = this.config.BackgroundColor.ToNativeColor();
            this.view.Signature.BackgroundColor = this.config.SignatureBackgroundColor.ToNativeColor();
            this.view.Signature.Caption.TextColor = this.config.CaptionTextColor.ToNativeColor();
            this.view.Signature.Caption.Text = this.config.CaptionText;
            this.view.Signature.ClearLabel.SetTitle(this.config.ClearText, UIControlState.Normal);
            this.view.Signature.ClearLabel.SetTitleColor(this.config.ClearTextColor.ToNativeColor(), UIControlState.Normal);
            this.view.Signature.SignatureLineColor = this.config.SignatureLineColor.ToNativeColor();
            this.view.Signature.SignaturePrompt.Text = this.config.PromptText;
            this.view.Signature.SignaturePrompt.TextColor = this.config.PromptTextColor.ToNativeColor();
            this.view.Signature.StrokeColor = this.config.StrokeColor.ToNativeColor();
            this.view.Signature.StrokeWidth = this.config.StrokeWidth;
            this.view.Signature.Layer.ShadowOffset = new SizeF(0, 0);
            this.view.Signature.Layer.ShadowOpacity = 1f;

            this.view.SaveButton.SetTitle(this.config.SaveText, UIControlState.Normal);
            this.view.SaveButton.TouchUpInside += (sender, args) => {
                if (this.view.Signature.IsBlank)
                    return;

                var points = this.view
                    .Signature
                    .Points
                    .Select(x => new DrawPoint((float)x.X, (float)x.Y));

				var tempPath = GetTempFilePath();
                using (var image = this.view.Signature.GetImage()) 
                    using (var stream = GetImageStream(image, this.config.ImageType))
						using (var fs = new FileStream(tempPath, FileMode.Create)) 
							stream.CopyTo(fs);

				this.DismissViewController(true, null);
				this.onResult(new SignatureResult(false, () => new FileStream(tempPath, FileMode.Open, FileAccess.Read, FileShare.Read), points));
            };

            this.view.CancelButton.SetTitle(this.config.CancelText, UIControlState.Normal);
            this.view.CancelButton.TouchUpInside += (sender, args) => {
                this.DismissViewController(true, null);
                this.onResult(new SignatureResult(true, null, null));
            };
        }


		private static string GetTempFilePath() {
			var documents = UIDevice.CurrentDevice.CheckSystemVersion(8, 0)
				? NSFileManager.DefaultManager.GetUrls (NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User)[0].Path
				: Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

			var tempPath = Path.Combine(documents, "..", "tmp");
			return Path.Combine(tempPath, "Signature.tmp");
		}


        private static Stream GetImageStream(UIImage image, ImageFormatType formatType) {
            if (formatType == ImageFormatType.Jpg)
                return image.AsJPEG().AsStream();

            return image.AsPNG().AsStream();
        }
    }
}


//            FROM XAMARIN SAMPLES
//            this.view.Signature.Caption.Font = UIFont.FromName ("Marker Felt", 16f);
//            this.view.Signature.SignaturePrompt.Font = UIFont.FromName ("Helvetica", 32f);
//            this.view.Signature.BackgroundColor = UIColor.FromRGB (255, 255, 200); // a light yellow.
//            this.view.Signature.BackgroundImageView.Image = UIImage.FromBundle ("logo-galaxy-black-64.png");
//            this.view.Signature.BackgroundImageView.Alpha = 0.0625f;
//            this.view.Signature.BackgroundImageView.ContentMode = UIViewContentMode.ScaleToFill;
//            this.view.Signature.BackgroundImageView.Frame = new System.Drawing.RectangleF(20, 20, 256, 256);