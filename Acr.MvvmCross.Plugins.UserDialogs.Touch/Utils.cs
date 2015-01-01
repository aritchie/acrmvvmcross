using System;
using System.Linq;
using UIKit;


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

            //return 
            //    UIApplication.SharedApplication.KeyWindow
            //    ?? UIApplication.SharedApplication.Windows.Last()
            //    ?? UIApplication.SharedApplication.Delegate.Window;
        }


        public static UIView GetTopView() {
            return GetTopWindow().Subviews.Last();
        }


        public static UIViewController GetTopViewController() {
            var root = GetTopWindow().RootViewController;
            var tabs = root as UITabBarController;
            if (tabs != null)
                return tabs.SelectedViewController;

            var nav = root as UINavigationController;
            if (nav != null)
                return nav.VisibleViewController;

            if (root.PresentedViewController != null)
                return root.PresentedViewController;

            return root;
        }


        internal static UIKeyboardType GetKeyboardType(InputType inputType) {
            switch (inputType) {
                case InputType.Email:
                    return UIKeyboardType.EmailAddress;
                case InputType.Number:
                    return UIKeyboardType.NumberPad;
                default:
                    return UIKeyboardType.Default;
            }
        }
    }
}