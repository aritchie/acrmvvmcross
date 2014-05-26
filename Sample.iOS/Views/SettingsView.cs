using System;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Sample.Core.ViewModels;


namespace Sample.iOS.Views {

    [Register("SettingsView")]
    public class SettingsView : MvxTableViewController {

        public override void ViewDidLoad() {
            base.ViewDidLoad();
            this.Title = "Settings";
            var src = new MvxStandardTableViewSource(this.TableView, "TitleText Key; DetailText Value");

            var btnAction = new UIBarButtonItem(UIBarButtonSystemItem.Add);
            this.NavigationItem.RightBarButtonItem = btnAction;

            var set = this.CreateBindingSet<SettingsView, SettingsViewModel>();
            set.Bind(src).To(x => x.Settings);
            set.Bind(src).For(x => x.SelectionChangedCommand).To(x => x.Select);
            set.Bind(btnAction).To(x => x.Actions);
            set.Apply();

            this.TableView.Source = src;
            this.TableView.ReloadData();
        }
    }
}