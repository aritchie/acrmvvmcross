using System;
using System.Collections.Generic;
using Cirrious.CrossCore.UI;


namespace Acr.MvvmCross.Plugins.SignaturePad {

    public abstract class AbstractSignatureService : ISignatureService {

        protected AbstractSignatureService() {
            this.Configuration = new SignaturePadConfiguration {
                ImageType = ImageFormatType.Png,
                BackgroundColor = MvxColors.White,
                CaptionTextColor = MvxColors.Black,
                ClearTextColor = MvxColors.Black,
                PromptTextColor = MvxColors.White,
                StrokeColor = MvxColors.Black,
                StrokeWidth = 2f,
                SignatureBackgroundColor = MvxColors.White,
                SignatureLineColor = MvxColors.Black,

                SaveText = "Save",
                CancelText = "Cancel",
                ClearText = "Clear",
                PromptText = "",
                CaptionText = "Please Sign Here"
            };
        }

        public abstract void Load(IEnumerable<DrawPoint> points);
        public abstract void Request(Action<SignatureResult> onResult);
        
        
        public SignaturePadConfiguration Configuration { get; private set; }
    }
}

