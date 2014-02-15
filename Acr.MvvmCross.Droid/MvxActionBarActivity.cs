/* Code courtesy of http://motzcod.es/ */
using Android.Content;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Droid.Views;

namespace Acr.MvvmCross.Droid {
    
    public abstract class MvxActionBarActivity : MvxActionBarEventSourceActivity, IMvxAndroidView {

        protected MvxActionBarActivity() {
            BindingContext = new MvxAndroidBindingContext(this, this);
            this.AddEventListeners();
        }


        public object DataContext {
            get { return BindingContext.DataContext; }
            set { BindingContext.DataContext = value; }
        }


        public IMvxViewModel ViewModel {
            get { return DataContext as IMvxViewModel; }
            set {
                DataContext = value;
                OnViewModelSet();
            }
        }


        protected override void OnResume() {
            base.OnResume();
            this.ViewModel.TryResume();
        }


        protected override void OnPause() {
            base.OnPause();
            this.ViewModel.TryPause();
        }


        protected override void OnDestroy() {
            base.OnDestroy();
            this.ViewModel.TryDestroy();
        }

        
        public void MvxInternalStartActivityForResult(Intent intent, int requestCode) {
            base.StartActivityForResult(intent, requestCode);
        }

        
        public IMvxBindingContext BindingContext { get; set; }

        
        public override void SetContentView(int layoutResId) {
            var view = this.BindingInflate(layoutResId, null);
            SetContentView(view);
        }


        protected virtual void OnViewModelSet() {
        }
    }
}