using System;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Sample.Core.ViewModels;


namespace Sample.iOS.Views {
    
    [Register("FileManagerView")]
    public class FileManagerView : MvxTableViewController {

        public override void ViewDidLoad() {
            base.ViewDidLoad();
            var src = new MvxStandardTableViewSource(this.TableView, "TitleText Name; DetailText FileSize(Size)");

            this.NavigationItem.RightBarButtonItem = new UIBarButtonItem("Actions", UIBarButtonItemStyle.Plain, (sender, e) => 
                ((FileManagerViewModel)this.ViewModel).CurrentDirectoryActions.Execute()
            );

            var set = this.CreateBindingSet<FileManagerView, FileManagerViewModel>();
            //set.Bind(this).For(x => x.Title).To(x => x.CurrentDirectory);
            set.Bind(src).To(x => x.Nodes);
            set.Bind(src).For(x => x.SelectionChangedCommand).To(x => x.SelectNode);
            set.Apply();
            
            this.TableView.Source = src;
            this.TableView.ReloadData();
        }
    }
}