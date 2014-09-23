using System;
using BigTed;
using MonoTouch.UIKit;


namespace Acr.MvvmCross.Plugins.UserDialogs.Touch {
    
    public abstract class AbstractTouchUserDialogService : AbstractUserDialogService {


        public override void Toast(string message, int timeoutSeconds, Action onClick) {
            // TODO: no click callback in showtoast at the moment
            this.Dispatch(() =>  {
                var ms = timeoutSeconds * 1000;
                BTProgressHUD.ShowToast(message, false, ms);
            });
        }


        protected override IProgressDialog CreateDialogInstance() {
            return new TouchProgressDialog();
        }


        protected virtual void Dispatch(Action action) {
            UIApplication.SharedApplication.InvokeOnMainThread(() => action());
        }
    }
}