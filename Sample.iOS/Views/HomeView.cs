using System;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.Foundation;
using Sample.Core.ViewModels;


namespace Sample.iOS.Views {
    
    [Register("MainView")]
    public class HomeView : MvxTableViewController {

        public override void ViewDidLoad() {
            base.ViewDidLoad();
            this.Title = "ACR MvvmCross Plugins";
            var src = new MvxStandardTableViewSource(this.TableView, "TitleText Title");

            var set = this.CreateBindingSet<HomeView, HomeViewModel>();
            set.Bind(src).To(x => x.Menu);
            set.Bind(src).For(x => x.SelectionChangedCommand).To(x => x.View);
            set.Apply();
            
            this.TableView.Source = src;
            this.TableView.ReloadData();
        }
    }
}