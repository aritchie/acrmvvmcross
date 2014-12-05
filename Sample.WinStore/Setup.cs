using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.WindowsCommon.Platform;
using Windows.UI.Xaml.Controls;


namespace Sample.WinStore {

    public class Setup : MvxWindowsSetup {
        
        public Setup(Frame rootFrame) : base(rootFrame) {}


        protected override IMvxApplication CreateApp() {
            return new Core.App();
        }
    }
}