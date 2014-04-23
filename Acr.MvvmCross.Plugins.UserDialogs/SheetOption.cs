using System;


namespace Acr.MvvmCross.Plugins.UserDialogs {
    
    public class SheetOption {

        public string Text { get; set; }
        public Action Action { get; set; }

        public SheetOption(string text, Action action = null) {
            this.Text = text;
            this.Action = (action ?? (() => {}));
        }
    }
}
