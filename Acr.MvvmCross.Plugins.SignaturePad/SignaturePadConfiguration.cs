using System;
using Cirrious.CrossCore.UI;


namespace Acr.MvvmCross.Plugins.SignaturePad {

    public enum ImageFormatType {
        Png,
        Jpg
    }


    public class SignaturePadConfiguration {

		private static SignaturePadConfiguration defaultConfig;
		public static SignaturePadConfiguration Default {
			get {
				defaultConfig = defaultConfig ?? new SignaturePadConfiguration();
				return defaultConfig;
			}
			set {
				if (defaultConfig == null)
					throw new ArgumentException("Default configuration cannot be null");

				defaultConfig = value;
			}
		}


		public SignaturePadConfiguration() {
			this.ImageType = ImageFormatType.Png;
			this.BackgroundColor = MvxColors.White;
			this.CaptionTextColor = MvxColors.Black;
			this.ClearTextColor = MvxColors.Black;
			this.PromptTextColor = MvxColors.White;
			this.StrokeColor = MvxColors.Black;
			this.StrokeWidth = 2f;
			this.SignatureBackgroundColor = MvxColors.White;
			this.SignatureLineColor = MvxColors.Black;

			this.SaveText = "Save";
			this.CancelText = "Cancel";
			this.ClearText = "Clear";
			this.PromptText = "";
			this.CaptionText = "Please Sign Here";
		}


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
