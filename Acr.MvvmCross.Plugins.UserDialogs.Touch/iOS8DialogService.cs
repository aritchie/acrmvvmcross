using System;
using System.Drawing;
using System.Linq;
#if __UNIFIED__
using UIKit;
#else
using MonoTouch.UIKit;
#endif


namespace Acr.MvvmCross.Plugins.UserDialogs.Touch {

    public class iOS8DialogService : AbstractTouchUserDialogService {

        public override void Alert(AlertConfig config) {
            var alert = UIAlertController.Create(config.Title ?? String.Empty, config.Message, UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, x => {
                if (config.OnOk != null)
                    config.OnOk();
            }));
            this.Present(alert);
        }


        public override void ActionSheet(ActionSheetConfig config) {
            var sheet = UIAlertController.Create(config.Title ?? String.Empty, String.Empty, UIAlertControllerStyle.ActionSheet);
            config.Options.ToList().ForEach(x => 
                sheet.AddAction(UIAlertAction.Create(x.Text, UIAlertActionStyle.Default, y => {
                    if (x.Action != null)
                        x.Action();
                }))
            );
            this.Present(sheet);
        }


        public override void Confirm(ConfirmConfig config) {
            var dlg = UIAlertController.Create(config.Title ?? String.Empty, config.Message, UIAlertControllerStyle.Alert);
            dlg.AddAction(UIAlertAction.Create(config.OkText, UIAlertActionStyle.Default, x => config.OnConfirm(true)));
            dlg.AddAction(UIAlertAction.Create(config.CancelText, UIAlertActionStyle.Default, x => config.OnConfirm(false)));
            this.Present(dlg);
        }


        public override void Login(LoginConfig config) {
            UITextField txtUser = null;
            UITextField txtPass = null;
            var dlg = UIAlertController.Create(config.Title ?? String.Empty, config.Message, UIAlertControllerStyle.Alert);
            dlg.AddAction(UIAlertAction.Create(config.OkText, UIAlertActionStyle.Default, x => config.OnResult(new LoginResult(txtUser.Text, txtPass.Text, true))));
            dlg.AddAction(UIAlertAction.Create(config.CancelText, UIAlertActionStyle.Default, x => config.OnResult(new LoginResult(txtUser.Text, txtPass.Text, false))));
            dlg.AddTextField(x => {
                txtUser = x;
                x.Placeholder = config.LoginPlaceholder;
                x.Text = config.LoginValue ?? String.Empty;
            });
            dlg.AddTextField(x => {
                txtPass = x;
                x.Placeholder = config.PasswordPlaceholder;
                x.SecureTextEntry = true;
            });
            this.Present(dlg);
        }


        public override void Prompt(PromptConfig config) {
            var result = new PromptResult();
            var dlg = UIAlertController.Create(config.Title ?? String.Empty, config.Message, UIAlertControllerStyle.Alert);
            UITextField txt = null;

            dlg.AddAction(UIAlertAction.Create(config.OkText, UIAlertActionStyle.Default, x => {
                result.Ok = true;
                result.Text = txt.Text.Trim();
                config.OnResult(result);
            }));
            dlg.AddAction(UIAlertAction.Create(config.CancelText, UIAlertActionStyle.Default, x => {
                result.Ok = false;
                result.Text = txt.Text.Trim();
                config.OnResult(result);
            }));
            dlg.AddTextField(x => {
                x.SecureTextEntry = config.InputType == InputType.Password;
                x.Placeholder = config.Placeholder ?? String.Empty;
                x.KeyboardType = Utils.GetKeyboardType(config.InputType);
                txt = x;
            });
            this.Present(dlg);
        }


        private void Present(UIAlertController controller) {
            this.Dispatch(() =>  {
                var top = Utils.GetTopViewController();
                var po = controller.PopoverPresentationController;
                if (po != null) {
					po.SourceView = top.View;
					var h = (top.View.Frame.Height / 2) - 400;
					var v = (top.View.Frame.Width / 2) - 300;
					po.SourceRect = new RectangleF((float)v, (float)h, 0, 0);
					po.PermittedArrowDirections = UIPopoverArrowDirection.Any;
                }
                top.PresentViewController(controller, true, null);
            });
        }
    }
}