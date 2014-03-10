using System;
using Cirrious.MvvmCross.ViewModels;


namespace Sample.Core.ViewModels {
    
    public class MenuItemViewModel : MvxViewModel {
        
        public MenuItemViewModel(string title, Action command) {
            this.title = title;
            this.command = command;
        }


        private string title;
        public string Title {
            get { return this.title; }
            set {
                if (this.title == value)
                    return;

                this.title = value;
                this.RaisePropertyChanged(() => this.Title);
            }
        }


        private readonly Action command;
        public IMvxCommand Command {
            get {
                return new MvxCommand(this.command);
            }
        }
    }
}
