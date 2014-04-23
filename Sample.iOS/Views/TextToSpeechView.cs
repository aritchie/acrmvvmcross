using System;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Dialog.Touch;
using CrossUI.Touch.Dialog.Elements;
using MonoTouch.Foundation;
using Sample.Core.ViewModels;


namespace Sample.iOS.Views {
    
    [Register("TextToSpeechView")]
    public class TextToSpeechView : MvxDialogViewController {

        public TextToSpeechView() : base(pushing: true) { }


        public override void ViewDidLoad() {
            base.ViewDidLoad();
            var bindings = this.CreateInlineBindingTarget<TextToSpeechViewModel>();
            this.Root = new RootElement("Text-To-Speech") {
                new Section("Main Functionality") {
                    new EntryElement("Text To Say").Bind(bindings, x => x.Value, x => x.Text),
                    new StringElement("Speak").Bind(bindings, x => x.SelectedCommand, x => x.Speak)
                    //new StringElement("Cancel").Bind(bindings, x => x.SelectedCommand, x => x.Cancel)
                },
                new Section("Configuration") {
                    new StringElement("Voice Pitch").Bind(bindings, x => x.Value, x => x.SpeechService.DefaultOptions.VoicePitch),
                    new StringElement("Speech Rate").Bind(bindings, x => x.Value, x => x.SpeechService.DefaultOptions.SpeechRate)
                }
            };
        }
    }
}