using System;
using Android.OS;
using Android.Views;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;
using Cirrious.MvvmCross.ViewModels;
using Newtonsoft.Json;


namespace Acr.MvvmCross.Droid.Fragments {
    
    public class Mvx2Fragment : MvxFragment {

        private readonly int layoutId;

        protected Mvx2Fragment(int layoutId) : base() {
            this.layoutId = layoutId;
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(this.layoutId, null);

            if (savedInstanceState != null) {
                var viewModelType = savedInstanceState.GetType("ViewModelType");
                var state = savedInstanceState.GetObject<MvxBundle>("ViewModelBundle");;
                var request = MvxViewModelRequest.GetDefaultRequest(viewModelType);
                this.ViewModel = Mvx.Resolve<IMvxViewModelLoader>().LoadViewModel(request, state);
            }
            return view;
        }


        public override void OnSaveInstanceState(Bundle outState) {
            var state = this.ViewModel.SaveStateBundle();
            outState.PutObject("ViewModelBundle", state);
            outState.PutType("ViewModelType", this.ViewModel.GetType());
            base.OnSaveInstanceState(outState);
        }


        public override void OnResume() {
            base.OnResume();
            this.ViewModel.TryResume();
        }


        public override void OnPause() {
            base.OnPause();
            this.ViewModel.TryPause();
        }


        public override void OnDestroy() {
            base.OnDestroy();
            this.ViewModel.TryDestroy();
        }
    }
}