using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;


namespace Acr.MvvmCross.Plugins.UserDialogs.WinPhone {
    
    internal static class Helpers {

        internal static Popup CreatePopup(UserControl control) {
            var size = GetFrameSize();

            // overlay entire frame
            return new Popup {
                VerticalOffset = (size.Width - control.ActualHeight) / 2,
                HorizontalOffset = (size.Height - control.ActualWidth) / 2,
                Width = size.Width,
                Height = size.Height,
                Child = control
            };            
        }


        internal static Size GetFrameSize() {
            return Application.Current.RootVisual.RenderSize;
        }
    }
}
