using System;
using System.Linq;
using MonoTouch.UIKit;


namespace Acr.MvvmCross.Plugins.FileSystem.Touch {

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
                return tabs.SelectedViewController;

            var nav = root as UINavigationController;
            if (nav != null)
                return nav.VisibleViewController;

            if (root.PresentedViewController != null)
                return root.PresentedViewController;

            return root;
        }
    }
}