using System;


namespace Acr.MvvmCross.Plugins.UserDialogs {
    
    public class DateTimePromptResult {

        public bool Success {
            get { return (this.SelectedDateTime != null); }
        }

        public DateTime? SelectedDateTime { get; set; }



        public DateTimePromptResult(DateTime? selectedDateTime) {
            this.SelectedDateTime = selectedDateTime;
        }
    }
}
