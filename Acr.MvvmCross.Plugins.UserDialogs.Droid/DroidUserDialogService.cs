using System;
using System.Linq;
using Android.App;
using Android.Widget;
using AndroidHUD;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Core;
using Cirrious.CrossCore.Droid.Platform;


namespace Acr.MvvmCross.Plugins.UserDialogs.Droid {
    
    public class DroidUserDialogService : AbstractUserDialogService {

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


        public override void ActionSheet(string title,  params SheetOption[] options) {
            var array = options
                .Select(x => x.Text)
                .ToArray();

            this.Dispatch(activity => 
                new AlertDialog
                    .Builder(activity)
                    .SetTitle(title)
                    .SetItems(array, (sender, args) => options[args.Which].Action())
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
            onClick = onClick ?? (() => {});
            var activity = GetTopActivity();

            AndHUD.Shared.ShowToast(
                activity, 
                message, 
                MaskType.Clear,
                TimeSpan.FromSeconds(timeoutSeconds),
                false,
                onClick
            );
        }


        public override IProgressDialog Progress(string title, Action onCancel, string cancelText, bool show) {
            var activity = GetTopActivity();

            var dlg = new DroidProgressDialog(activity) {
                Title = title,
                IsDeterministic = true
            };

            if (onCancel != null) {
                dlg.SetCancel(onCancel, cancelText);
            }

            if (show) {
                dlg.Show();
            }
            return dlg;
        }


        public override IProgressDialog Loading(string title, Action onCancel, string cancelText, bool show) {
            var activity = GetTopActivity();

            var dlg = new DroidProgressDialog(activity) {
                Title = title,
                IsDeterministic = false
            };

            if (onCancel != null) {
                dlg.SetCancel(onCancel, cancelText);
            }

            if (show) {
                dlg.Show();
            }
            return dlg;
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