using System;
using Acr.MvvmCross.ViewModels;
using Cirrious.MvvmCross.ViewModels;


namespace Acr.MvvmCross {
    
    public static class Extensions {

        public static void TryDestroy(this IMvxViewModel viewModel) {
            var lc = viewModel as IViewModelLifecycle;
            if (lc != null) {
                lc.OnDestroy();
            }
        }


        public static void TryResume(this IMvxViewModel viewModel) {
            var lc = viewModel as IViewModelLifecycle;
            if (lc != null) {
                lc.OnResume();
            }
        }


        public static void TryPause(this IMvxViewModel viewModel) {
            var lc = viewModel as IViewModelLifecycle;
            if (lc != null) {
                lc.OnPause();
            }
        }
    }
}