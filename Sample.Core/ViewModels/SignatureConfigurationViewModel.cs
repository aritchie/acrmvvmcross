using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Acr.MvvmCross.Plugins.SignaturePad;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.CrossCore.UI;


namespace Sample.Core.ViewModels {

    public class ColorDefinition {
        public string Name { get; set; }
        public MvxColor Color { get; set; }
    }


    public class SignatureConfigurationViewModel : MvxViewModel {
        private readonly ISignatureService signatureService;


        public SignatureConfigurationViewModel(ISignatureService signatureService) {
            this.signatureService = signatureService;
            this.Colors = typeof(MvxColors)
                .GetTypeInfo()
                .DeclaredFields
                .Select(x => new ColorDefinition {
                    Name = x.Name,
                    Color = (MvxColor)x.GetValue(null)
                })
                .ToList();

			var cfg = SignaturePadConfiguration.Default;
            this.saveText = cfg.SaveText;
            this.cancelText = cfg.CancelText;
            this.promptText = cfg.PromptText;
            this.captionText = cfg.CaptionText;

            this.bgColor = this.GetColorDefinition(cfg.BackgroundColor);
            this.promptTextColor = this.GetColorDefinition(cfg.PromptTextColor);
            this.captionTextColor = this.GetColorDefinition(cfg.CaptionTextColor);
            this.signatureLineColor = this.GetColorDefinition(cfg.SignatureLineColor);
            this.strokeColor = this.GetColorDefinition(cfg.StrokeColor);
        }


        private ColorDefinition GetColorDefinition(MvxColor color) {
            return this.Colors.FirstOrDefault(x => x.Color.ARGB == color.ARGB);
        }

        #region Binding Properties

        public IList<ColorDefinition> Colors { get; private set; }


        private string cancelText;
        public string CancelText {
            get { return this.cancelText; }
            set { 
                if (this.SetProperty(ref this.cancelText, value))
					SignaturePadConfiguration.Default.CancelText = value;
            }
        }


        private string saveText;
        public string SaveText {
            get { return this.saveText; }
            set { 
                if (this.SetProperty(ref this.saveText, value))
					SignaturePadConfiguration.Default.SaveText = value;
            }
        }


        private string promptText;
        public string PromptText {
            get { return this.promptText; }
            set {
                if (this.SetProperty(ref this.promptText, value))
					SignaturePadConfiguration.Default.PromptText = value;
            }
        }


        private string captionText;
        public string CaptionText {
            get { return this.captionText; }
            set {
                if (this.SetProperty(ref this.captionText, value))
					SignaturePadConfiguration.Default.CaptionText = value;
            }
        }


        private float strokeWidth;
        public float StrokeWidth {
            get { return this.strokeWidth; }
            set {
                if (this.SetProperty(ref this.strokeWidth, value))
					SignaturePadConfiguration.Default.StrokeWidth = value;
            }
        }

        public ColorDefinition signatureLineColor;
        public ColorDefinition SignatureLineColor {
            get { return this.signatureLineColor; }
            set {
                if (this.SetProperty(ref this.signatureLineColor, value))
					SignaturePadConfiguration.Default.SignatureLineColor = value.Color;
            }
        }


        private ColorDefinition strokeColor;
        public ColorDefinition StrokeColor {
            get { return this.strokeColor; }
            set {
                if (this.SetProperty(ref this.strokeColor, value))
					SignaturePadConfiguration.Default.StrokeColor = value.Color;
            }
        }


        private ColorDefinition captionTextColor;
        public ColorDefinition CaptionTextColor {
            get { return this.captionTextColor; }
            set {
                if (this.SetProperty(ref this.captionTextColor, value))
					SignaturePadConfiguration.Default.CaptionTextColor = value.Color;
            }
        }


        private ColorDefinition bgColor;
        public ColorDefinition BgColor {
            get { return this.bgColor; }
            set {
                if (this.SetProperty(ref this.bgColor, value))
					SignaturePadConfiguration.Default.BackgroundColor = value.Color;
            }
        }


        private ColorDefinition promptTextColor;
        public ColorDefinition PromptTextColor {
            get { return this.promptTextColor; }
            set {
                if (this.SetProperty(ref this.promptTextColor, value))
					SignaturePadConfiguration.Default.PromptTextColor = value.Color;
            }
        }

        #endregion
    }
}
