using System;
using Acr.MvvmCross.Plugins.TextToSpeech;
using Acr.MvvmCross.Plugins.UserDialogs;
using Acr.MvvmCross.ViewModels;
using Cirrious.MvvmCross.ViewModels;


namespace Sample.Core.ViewModels {
    
    public class TextToSpeechViewModel : ViewModel {
        private readonly ITextToSpeechService speechService;
        private readonly IUserDialogService dialogService;


        public TextToSpeechViewModel(ITextToSpeechService speechService, IUserDialogService dialogService) {
            this.speechService = speechService;
            this.dialogService = dialogService;
        }


        private string text;
        public string Text {
            get { return this.text; }
            set {
                if (this.text == value)
                    return;

                this.text = value;
                this.RaisePropertyChanged("Text");
            }
        }


        public IMvxCommand Speak {
            get {
                return new MvxCommand(async () => {
                    if (String.IsNullOrEmpty(this.Text)) {
                        this.dialogService.Alert("Please enter the text!");
                    }
                    else { 
                        await this.speechService.Speak(this.Text);
                    }
                });
            }
        }
    }
}
