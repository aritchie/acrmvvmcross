using System;
using System.IO;
using System.Linq;
using System.Drawing;
using Android.App;
using Android.OS;
using Android.Widget;
using Android.Views;
using Cirrious.MvvmCross.Plugins.Color.Droid;
using SignaturePad;


namespace Acr.MvvmCross.Plugins.SignaturePad.Droid {

    [Activity]
    public class SignaturePadActivity : Activity {
        private SignaturePadView signatureView;
        private Button btnSave;
        private Button btnCancel;


        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.SignaturePad);

            var rootView = this.FindViewById<RelativeLayout>(Resource.Id.rootView);
            this.signatureView = this.FindViewById<SignaturePadView>(Resource.Id.signatureView);
            this.btnSave = this.FindViewById<Button>(Resource.Id.btnSave);
            this.btnCancel = this.FindViewById<Button>(Resource.Id.btnCancel);

            var cfg = DroidSignatureService.CurrentConfig;

            rootView.SetBackgroundColor(cfg.BackgroundColor.ToAndroidColor());
            this.signatureView.BackgroundColor = cfg.SignatureBackgroundColor.ToAndroidColor();
            this.signatureView.Caption.Text = cfg.CaptionText;
            this.signatureView.Caption.SetTextColor(cfg.CaptionTextColor.ToAndroidColor());
            this.signatureView.ClearLabel.Text = cfg.ClearText;
            this.signatureView.ClearLabel.SetTextColor(cfg.ClearTextColor.ToAndroidColor());
            this.signatureView.SignatureLineColor = cfg.SignatureLineColor.ToAndroidColor(); 
            this.signatureView.SignaturePrompt.Text = cfg.PromptText;
            this.signatureView.SignaturePrompt.SetTextColor(cfg.PromptTextColor.ToAndroidColor());
            this.signatureView.StrokeColor = cfg.StrokeColor.ToAndroidColor();
            this.signatureView.StrokeWidth = cfg.StrokeWidth;

            this.btnSave.Text = cfg.SaveText;
            this.btnCancel.Text = cfg.CancelText;

            if (DroidSignatureService.CurrentPoints != null) {
                this.btnSave.Visibility = ViewStates.Gone;
                this.btnCancel.Visibility = ViewStates.Gone;
//                this.signatureView.Enabled = false;
                this.signatureView.LoadPoints(
                    DroidSignatureService
                        .CurrentPoints
                        .Select(x => new PointF { X = x.X, Y = x.Y })
                        .ToArray()
                );
            }
        }


        protected override void OnResume() {
            base.OnResume();
            this.btnSave.Click += this.OnSave;
            this.btnCancel.Click += this.OnCancel;
        }


        protected override void OnPause() {
            base.OnPause();
            this.btnSave.Click -= this.OnSave;
            this.btnCancel.Click -= this.OnCancel;
        }


        private void OnSave(object sender, EventArgs args) {
            if (this.signatureView.IsBlank)
                return;

            var points = this.signatureView
                .Points
                .Select(x => new DrawPoint(x.X, x.Y));

            using (var image = this.signatureView.GetImage()) {
                 using (var stream = new MemoryStream()) {
                    var format = DroidSignatureService.CurrentConfig.ImageType == ImageFormatType.Png
                        ? Android.Graphics.Bitmap.CompressFormat.Png
                        : Android.Graphics.Bitmap.CompressFormat.Jpeg;
                    image.Compress(format, 100, stream);
                    DroidSignatureService.OnResult(new SignatureResult(false, stream, points));
                    this.Finish();
                }
            }
        }


        private void OnCancel(object sender, EventArgs args) {
            DroidSignatureService.OnResult(new SignatureResult(true, null, null));
            this.Finish();
        }
    }
}

