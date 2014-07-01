using System;
using System.Linq;
using Android.App;
using Android.Text.Method;
using Android.Views.InputMethods;
using Android.Widget;
using AndroidHUD;


namespace Acr.MvvmCross.Plugins.UserDialogs.Droid {
    
    public class DroidUserDialogService : AbstractUserDialogService {

        public override void Alert(AlertConfig config) {
            Utils.RequestMainThread(() => 
                new AlertDialog
                    .Builder(Utils.GetActivityContext())
                    .SetMessage(config.Message)
                    .SetTitle(config.Title)
                    .SetPositiveButton(config.OkText, (o, e) => {
                        if (config.OnOk != null) 
                            config.OnOk();
                    })
                    .Show()
            );
        }


        public override void ActionSheet(ActionSheetConfig config) {
            var array = config
                .Options
                .Select(x => x.Text)
                .ToArray();

            Utils.RequestMainThread(() => 
                new AlertDialog
                    .Builder(Utils.GetActivityContext())
                    .SetTitle(config.Title)
                    .SetItems(array, (sender, args) => config.Options[args.Which].Action())
                    .Show()
            );
        }


        public override void Confirm(ConfirmConfig config) {
            Utils.RequestMainThread(() => 
                new AlertDialog
                    .Builder(Utils.GetActivityContext())
                    .SetMessage(config.Message)
                    .SetTitle(config.Title)
                    .SetPositiveButton(config.OkText, (o, e) => config.OnConfirm(true))
                    .SetNegativeButton(config.CancelText, (o, e) => config.OnConfirm(false))
                    .Show()
            );
        }


        public override void DateTimePrompt(DateTimePromptConfig config) {
            // TODO
            throw new NotImplementedException();
        }


        public override void DurationPrompt(DurationPromptConfig config) {
            // TODO
            throw new NotImplementedException();
        }


        public override void Prompt(PromptConfig config) {
            Utils.RequestMainThread(() => {
                var txt = new EditText(Utils.GetActivityContext()) {
                    Hint = config.Placeholder
                };
                switch (config.Type) {

                    case PromptType.Secure:
                        //txt.InputType = InputTypes.ClassText | InputTypes.TextVariationPassword;
                        txt.TransformationMethod = PasswordTransformationMethod.Instance;
                        break;

                    case PromptType.MultiLine:
                        txt.SetLines(3);
                        txt.SetSingleLine(false);
                        txt.ImeOptions = ImeAction.Next;
                        break;
                }

                new AlertDialog
                    .Builder(Utils.GetActivityContext())
                    .SetMessage(config.Message)
                    .SetTitle(config.Title)
                    .SetView(txt)
                    .SetPositiveButton(config.OkText, (o, e) =>
                        config.OnResult(new PromptResult {
                            Ok = true, 
                            Text = txt.Text
                        })
                    )
                    .SetNegativeButton(config.CancelText, (o, e) => 
                        config.OnResult(new PromptResult {
                            Ok = false, 
                            Text = txt.Text
                        })
                    )
                    .Show();
            });
        }


        public override void Toast(string message, int timeoutSeconds, Action onClick) {
            Utils.RequestMainThread(() => {
                onClick = onClick ?? (() => {});

                AndHUD.Shared.ShowToast(
                    Utils.GetActivityContext(), 
                    message, 
                    MaskType.Clear,
                    TimeSpan.FromSeconds(timeoutSeconds),
                    false,
                    onClick
                );
            });
        }


        protected override IProgressDialog CreateDialogInstance() {
            return new ProgressDialog();
        }
    }
}