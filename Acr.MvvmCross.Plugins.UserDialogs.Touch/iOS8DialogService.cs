using System;
using System.Linq;
using MonoTouch.UIKit;


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
            var alert = UIAlertController.Create(config.Title ?? String.Empty, String.Empty, UIAlertControllerStyle.ActionSheet);
            config.Options.ToList().ForEach(x => 
                sheet.AddAction(UIAlertAction.Create(x.Text, UIAlertActionStyle.Default, y => {
                    if (x.Action != null)
                        x.Action();
                }))
            );
            this.Present(alert);
        }


        public override void Confirm(ConfirmConfig config) {
            var dlg = new UIAlertController {
                Title = config.Title ?? String.Empty,
                Message = config.Message
            };
            dlg.AddAction(UIAlertAction.Create(config.OkText, UIAlertActionStyle.Default, x => config.OnConfirm(true)));
            dlg.AddAction(UIAlertAction.Create(config.OkText, UIAlertActionStyle.Default, x => config.OnConfirm(false)));
            this.Present(dlg);
        }


        public override void Prompt(PromptConfig config) {
            var dlg = new UIAlertController {
                Title = config.Title ?? String.Empty,
                Message = config.Message
            };

            UITextField txt = null;
            dlg.AddTextField(x => {
                x.SecureTextEntry = config.IsSecure;
                x.Placeholder = config.Placeholder;
                txt = x;
            });
            dlg.AddAction(UIAlertAction.Create(config.OkText, UIAlertActionStyle.Default, x => {
                result.Ok = true;
                result.Text = txt.Text.Trim();
                config.OnResult(result);
            }));
            dlg.AddAction(UIAlertAction.Create(config.OkText, UIAlertActionStyle.Default, x => {
                result.Ok = false;
                result.Text = txt.Text.Trim();
                config.OnResult(result);
            }));
            this.Present(dlg);
        }


        private void Present(UIAlertController controller) {
            this.Dispatch(() =>  {
                var top = Utils.GetTopViewController();
                top.PresentViewController(controller, true, null);
            });
        }
    }
}