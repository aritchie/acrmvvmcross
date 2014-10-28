using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Plugins.File;
using Acr.MvvmCross.Plugins.UserDialogs;
using Acr.MvvmCross.Plugins.SignaturePad;
using Sample.Core.Models;


namespace Sample.Core.ViewModels {

    public class SignatureListViewModel : MvxViewModel {

        private const string FILE_FORMAT = "{0:dd-MM-yyyy_hh-mm-ss_tt}.jpg";
        private readonly IMvxFileStore store;
        private readonly ISignatureService signatureService;
        private readonly IUserDialogService dialogService;


        public SignatureListViewModel(IMvxFileStore store, 
                                      IUserDialogService dialogService,
                                      ISignatureService signatureService) {
            this.store = store;
            this.dialogService = dialogService;
            this.signatureService = signatureService;
            this.Configure = new MvxCommand(() => this.ShowViewModel<SignatureConfigurationViewModel>());
            this.Create = new MvxCommand(this.OnCreate);
            this.Delete = new MvxCommand<Signature>(this.OnDelete);
            this.View = new MvxCommand<Signature>(this.OnView);
            this.List = new ObservableCollection<Signature>();
        }


        public override void Start() {
            base.Start();
            var files = this.store
                .GetFilesIn(".")
                .Select(x => new Signature {
                    FileName = Path.GetFileName(x),
                    FilePath = x
                })
                .ToList();

            foreach (var file in files)
                this.List.Add(file);
        }


        public ObservableCollection<Signature> List { get; private set; }
        public IMvxCommand Configure { get; private set; }
        public IMvxCommand Create { get; private set; }
        public MvxCommand<Signature> View { get; private set; }
        public MvxCommand<Signature> Delete { get; private set; }


        private void OnCreate() {
            this.signatureService.Request(result => {
                var fileName = String.Format(FILE_FORMAT, DateTime.Now);
                var path = this.store.NativePath(fileName);

                using (var ms = new MemoryStream()) {
                    result.Stream.CopyTo(ms);
                    var bytes = ms.ToArray();
                    this.store.WriteFile(path, bytes);
                }
                this.List.Add(new Signature {
                    FilePath = path,
                    FileName = fileName
                });
            });
        }


        private void OnView(Signature signature) {
            this.dialogService.ActionSheet(new ActionSheetConfig()
                .Add("View", () => {
                })
                .Add("Delete", async () => {
                    var r = await this.dialogService.ConfirmAsync("Are you sure you want to delete " + signature.FileName);
                    if (r)
                        this.store.DeleteFile(signature.FilePath);
                })
                .Add("Cancel")
            );
        }


        private void OnDelete(Signature signature) {
            this.store.DeleteFile(signature.FilePath);
            this.List.Remove(signature);
        }
    }
}

