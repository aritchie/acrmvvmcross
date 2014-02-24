using System;
using Acr.MvvmCross.Droid;
using Android.App;
using Cirrious.MvvmCross.Binding.BindingContext;
using Sample.Core.ViewModels;


namespace Sample.Droid.Views {
    
    [Activity]
    public class FileManagerActivity : Mvx2Activity {

        public new FileManagerViewModel ViewModel {
            get { return (FileManagerViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnViewModelSet() {
            base.OnViewModelSet();
            this.SetContentView(Resource.Layout.FileManager);

            //var set = this.CreateBindingSet<FileManagerActivity, FileManagerViewModel>();
            //set.Bind(this.ActionBar).For(x => x.Title).To(x => x.CurrentDirectory);
            //set.Apply();
        }
    }
}