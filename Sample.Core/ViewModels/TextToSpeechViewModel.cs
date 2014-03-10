using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Acr.MvvmCross.Plugins.TextToSpeech;
using Acr.MvvmCross.Plugins.UserDialogs;
using Acr.MvvmCross.ViewModels;
using Cirrious.MvvmCross.ViewModels;


namespace Sample.Core.ViewModels {
    
    public class TextToSpeechViewModel : ViewModel {

        public ITextToSpeechService SpeechService { get; private set; }
        private readonly IUserDialogService dialogService;
        private CancellationTokenSource cancelSource;


        public TextToSpeechViewModel(ITextToSpeechService speechService, IUserDialogService dialogService) {
            this.SpeechService = speechService;
            this.dialogService = dialogService;

            var list = this.SpeechService.GetVoices().ToList();
            this.Voices = list;
        }



        public IList<VoiceDescriptor> Voices { get; private set; }


        private VoiceDescriptor selectedVoice;
        public VoiceDescriptor SelectedVoice {
            get { return this.SpeechService.DefaultOptions.Voice; }
            set {
                if (this.SpeechService.DefaultOptions.Voice == value)
                    return;

                this.SpeechService.DefaultOptions.Voice = value;
                this.RaisePropertyChanged(() => this.SelectedVoice);
            }
        }


        private string text;
        public string Text {
            get { return this.text; }
            set {
                if (this.text == value)
                    return;

                this.text = value;
                this.RaisePropertyChanged(() => this.Text);
            }
        }


        // TODO: this is only necessary until I get loading cancel working on WP8
        public IMvxCommand Cancel {
            get {
                return new MvxCommand(() => {
                    if (this.cancelSource == null)
                        this.dialogService.Alert("Nothing to cancel");
                    else {
                        this.cancelSource.Cancel();
                        this.dialogService.Alert("Cancelled");
                    }
                });
            }
        }


        public IMvxCommand Speak {
            get {
                return new MvxCommand(async () => {
                    
                    // TODO: show load with cancel when speaking
                    if (String.IsNullOrEmpty(this.Text)) 
                        this.dialogService.Alert("Please enter the text!");
                    else {
                        using (this.cancelSource = new CancellationTokenSource()) {
                            //using (this.dialogService.Loading("Speaking", () => this.cancelSource.Cancel(false))) { 
                                await this.SpeechService.Speak(this.Text, cancelToken: this.cancelSource.Token);
                            //}
                        } 
                        this.cancelSource = null;
                    }
                });
            }
        }
    }
}
