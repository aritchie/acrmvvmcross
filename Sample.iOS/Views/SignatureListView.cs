using System;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using Foundation;
using UIKit;
using Sample.Core.ViewModels;


namespace Sample.iOS.Views {

    [Foundation.Register("SignatureListView")]
    public class SignatureListView : MvxTableViewController {

        public override void ViewDidLoad() {
            base.ViewDidLoad();
            this.Title = "MVX Signatures";

            var btnAdd = new UIBarButtonItem(UIBarButtonSystemItem.Add);
            var btnConfig = new UIBarButtonItem(UIBarButtonSystemItem.Edit);
            this.NavigationItem.RightBarButtonItems = new [] { btnAdd, btnConfig };

            var src = new MvxStandardTableViewSource(this.TableView, "TitleText Name");

            var set = this.CreateBindingSet<SignatureListView, SignatureListViewModel>();
            set.Bind(src).To(x => x.List);
            set.Bind(src).For(x => x.SelectionChangedCommand).To(x => x.View);
            set.Bind(btnAdd).To(x => x.Create);
            set.Bind(btnConfig).To(x => x.Configure);
            set.Apply();

            this.TableView.Source = src;
            this.TableView.ReloadData();
        }
    }
}