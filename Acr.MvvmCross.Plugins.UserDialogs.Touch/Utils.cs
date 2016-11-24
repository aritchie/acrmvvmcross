using System;
using System.Linq;
#if __UNIFIED__
using UIKit;
#else
using MonoTouch.UIKit;
#endif


namespace Acr.MvvmCross.Plugins.UserDialogs.Touch {

    public static class Utils {

        public static UIWindow GetTopWindow() {
            return UIApplication.SharedApplication
                .Windows
                .Reverse()
                .FirstOrDefault(x => 
                    x.WindowLevel == UIWindowLevel.Normal && 
                    !x.Hidden
                );
        }


        public static UIView GetTopView() {
            return GetTopWindow().Subviews.Last();
        }


        public static UIViewController GetTopViewController() {
            var root = GetTopWindow().RootViewController;
            var tabs = root as UITabBarController;
            if (tabs != null)
                return (tabs.PresentedViewController != null) ? tabs.PresentedViewController : tabs.SelectedViewController;

            var nav = root as UINavigationController;
            if (nav != null)
                return nav.VisibleViewController;

            if (root.PresentedViewController != null)
                return root.PresentedViewController;

            return root;
        }


        internal static UIKeyboardType GetKeyboardType(InputType inputType) {
            switch (inputType) {
                case InputType.Email  : return UIKeyboardType.EmailAddress;
                case InputType.Number : return UIKeyboardType.NumberPad;
                default               : return UIKeyboardType.Default;
            }
        }
    }
}