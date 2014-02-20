using System;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Views;
using Android.Widget;
using AndroidHUD;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid.Platform;


namespace Acr.MvvmCross.Plugins.UserDialogs.Droid {
    
    public class DroidUserDialogService : MvxAndroidTask, IUserDialogService {

        public virtual void Toast(string message, int timeoutSeconds) {
            var activity = GetTopActivity();
            AndHUD.Shared.ShowToast(
                activity, 
                message, 
                MaskType.Black, 
                TimeSpan.FromSeconds(timeoutSeconds)
            );
        }


        public virtual Task Alert(string message, string title, string okText) {            
            var tcs = new TaskCompletionSource<object>();
            var act = GetTopActivity();

            this.Dispatcher.RequestMainThreadAction(() => 
                new AlertDialog.Builder(act)
                    .SetMessage(message)
                    .SetTitle(title)
                    .SetPositiveButton(okText, (o, e) => tcs.SetResult(null))
                    .Show()
            );

            return tcs.Task;
        }


        public virtual void ActionSheet(string title, string cancelText, params SheetOption[] options) {
            this.DoOnActivity(activity => {
                var popup = new PopupMenu(activity, null);
                
                for (var i = 0; i < options.Length; i++) {
                    popup.Menu.Add(0, i, i, options[i].Text);
                }

                popup.MenuItemClick += (sender, args) => {
                    options[args.Item.ItemId].Action();
                    args.Handled = true;
                    popup.Dismiss();
                };
                popup.Show();
            });
        }


        public virtual Task<bool> Confirm(string message, string title, string okText, string cancelText) {
            var tcs = new TaskCompletionSource<bool>();
            var act = GetTopActivity();

            this.Dispatcher.RequestMainThreadAction(() => 
                new AlertDialog.Builder(act)
                    .SetMessage(message)
                        .SetTitle(title)
                        .SetPositiveButton(okText, (o, e) => tcs.SetResult(true))
                        .SetNegativeButton(cancelText, (o, e) => tcs.SetResult(false))
                        .Show()
            );

            return tcs.Task;
        }


        public virtual Task<PromptResult> Prompt(string message, string title, string okText, string cancelText, string hint) {
            var tcs = new TaskCompletionSource<PromptResult>();
            var act = GetTopActivity();

            this.Dispatcher.RequestMainThreadAction(() => {
                var txt = new EditText(act) {
                    Hint = hint
                };

                new AlertDialog.Builder(act)
                    .SetMessage(message)
                    .SetTitle(title)
                    .SetView(txt)
                    .SetPositiveButton(okText, (o, e) => 
                        tcs.SetResult(new PromptResult {
                            Ok = true, 
                            Text = txt.Text
                        })
                    )
                    .SetNegativeButton(cancelText, (o, e) => 
                        tcs.SetResult(new PromptResult {
                            Ok = false, 
                            Text = txt.Text
                        })
                    )
                    .Show();
            });
            return tcs.Task;
        }


        public virtual IProgressDialog Progress(string title, int max, Action onCancel, string cancelText) {
            var activity = GetTopActivity();

            var dlg = new DroidProgressDialog(activity) {
                Title = title,
                IsDeterministic = true,
                Max = max,
                Progress = 0
            };

            if (onCancel != null) {
                dlg.SetCancel(onCancel, cancelText);
            }

            return dlg;
        }


        public virtual IProgressDialog Loading(string title, int max, Action onCancel, string cancelText) {
            var activity = GetTopActivity();

            var dlg = new DroidProgressDialog(activity) {
                Title = title,
                IsDeterministic = false,
                Max = max,
                Progress = 0
            };

            if (onCancel != null) {
                dlg.SetCancel(onCancel, cancelText);
            }

            return dlg;
        }


        protected static Activity GetTopActivity() {
            return Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
        }
    }
}