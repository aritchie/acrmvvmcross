using System;
using System.Linq;
using Android.App;
using Android.Widget;
using AndroidHUD;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Core;
using Cirrious.CrossCore.Droid.Platform;


namespace Acr.MvvmCross.Plugins.UserDialogs.Droid {
    
    public class DroidUserDialogService : AbstractUserDialogService<DroidProgressDialog> {

        public override void Alert(string message, string title, string okText, Action onOk) {
            this.Dispatch(activity => 
                new AlertDialog
                    .Builder(activity)
                    .SetMessage(message)
                    .SetTitle(title)
                    .SetPositiveButton(okText, (o, e) => {
                        if (onOk != null) {
                            onOk();
                        }
                    })
                    .Show()
            );
        }


        public override void ActionSheet(ActionSheetOptions options) {
            var array = options
                .Options
                .Select(x => x.Text)
                .ToArray();

            this.Dispatch(activity => 
                new AlertDialog
                    .Builder(activity)
                    .SetTitle(title)
                    .SetItems(array, (sender, args) => options.Options[args.Which].Action())
                    .Show()
            );
        }


        public override void Confirm(string message, Action<bool> onConfirm, string title, string okText, string cancelText) {
            this.Dispatch(activity => 
                new AlertDialog
                    .Builder(activity)
                    .SetMessage(message)
                        .SetTitle(title)
                        .SetPositiveButton(okText, (o, e) => onConfirm(true))
                        .SetNegativeButton(cancelText, (o, e) => onConfirm(false))
                        .Show()
            );
        }


        public override void Prompt(string message, Action<PromptResult> promptResult, string title, string okText, string cancelText, string hint) {
            this.Dispatch(activity => {
                var txt = new EditText(activity) {
                    Hint = hint
                };

                new AlertDialog
                    .Builder(activity)
                    .SetMessage(message)
                    .SetTitle(title)
                    .SetView(txt)
                    .SetPositiveButton(okText, (o, e) =>
                        promptResult(new PromptResult {
                            Ok = true, 
                            Text = txt.Text
                        })
                    )
                    .SetNegativeButton(cancelText, (o, e) => 
                        promptResult(new PromptResult {
                            Ok = false, 
                            Text = txt.Text
                        })
                    )
                    .Show();
            });
        }


        public override void Toast(string message, int timeoutSeconds, Action onClick) {
            this.Dispatch(activity => {
                onClick = onClick ?? (() => {});

                AndHUD.Shared.ShowToast(
                    activity, 
                    message, 
                    MaskType.Clear,
                    TimeSpan.FromSeconds(timeoutSeconds),
                    false,
                    onClick
                );
            });
        }


        protected override DroidProgressDialog CreateProgressDialogInstance() {
            var activity = GetTopActivity();
            return new DroidProgressDialog(activity);
        }


        protected virtual void Dispatch(Action action) {
            Mvx.Resolve<IMvxMainThreadDispatcher>().RequestMainThreadAction(action);
        }


        protected virtual void Dispatch(Action<Activity> action) {
            this.Dispatch(() => {
                var activity = this.GetTopActivity();
                action(activity);
            });
        }


        protected virtual Activity GetTopActivity() {
            return Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
        }
    }
}