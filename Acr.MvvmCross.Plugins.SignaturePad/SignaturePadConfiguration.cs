using System;
using Cirrious.CrossCore.UI;


namespace Acr.MvvmCross.Plugins.SignaturePad {

    public enum ImageFormatType {
        Png,
        Jpg
    }


    public class SignaturePadConfiguration {

        public ImageFormatType ImageType { get; set; }

        public string SaveText { get; set; }
        public string CancelText { get; set; }

        public MvxColor BackgroundColor { get; set; }
        public MvxColor SignatureBackgroundColor { get; set; }
        public MvxColor SignatureLineColor { get; set; }
        
        public string CaptionText { get; set; }
        public MvxColor CaptionTextColor { get; set; }

        public string PromptText { get; set; }
        public MvxColor PromptTextColor { get; set; }

        public string ClearText { get; set; }
        public MvxColor ClearTextColor { get; set; }

        public float StrokeWidth { get; set; }
        public MvxColor StrokeColor { get; set; }
    }
}
