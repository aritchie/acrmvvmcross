using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Plugins.File;
using Acr.MvvmCross.Plugins.UserDialogs;
using Acr.MvvmCross.Plugins.SignaturePad;
using Acr.MvvmCross.Plugins.FileSystem;
using Sample.Core.Models;


namespace Sample.Core.ViewModels {

    public class SignatureListViewModel : MvxViewModel {

        private const string FILE_FORMAT = "{0:dd-MM-yyyy_hh-mm-ss_tt}.jpg";
        private readonly IFileSystem fileSystem;
        private readonly ISignatureService signatureService;
        private readonly IUserDialogService dialogService;


        public SignatureListViewModel(IFileSystem fileSystem, 
                                      IUserDialogService dialogService,
                                      ISignatureService signatureService) {
			this.fileSystem = fileSystem;
            this.dialogService = dialogService;
            this.signatureService = signatureService;
            this.Configure = new MvxCommand(() => this.ShowViewModel<SignatureConfigurationViewModel>());
			this.Create = new MvxCommand(async () => await this.OnCreate());
            this.View = new MvxCommand<Signature>(this.OnView);
            this.List = new ObservableCollection<Signature>();
        }


        public override void Start() {
            base.Start();
			var files = this.fileSystem
				.Public
				.Files
                .Select(x => new Signature {
					FileName = x.Name,
					FilePath = x.FullName
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


		private async Task OnCreate() {
			var result = await this.signatureService.Request();
			if (result.Cancelled)
				return;

            var fileName = String.Format(FILE_FORMAT, DateTime.Now);
			var file = this.fileSystem.Public.CreateFile(fileName);

			using (var fs = file.Create())
				using (var stream = result.GetStream())
					stream.CopyTo(fs);
            
            this.List.Add(new Signature {
				FilePath = file.FullName,
                FileName = fileName
            });
        }


        private void OnView(Signature signature) {
            this.dialogService.ActionSheet(new ActionSheetConfig()
                .Add("View", () => {
                })
                .Add("Delete", async () => {
                    var r = await this.dialogService.ConfirmAsync("Are you sure you want to delete " + signature.FileName);
					if (r) {
						this.fileSystem.GetFile(signature.FilePath).Delete();
						this.List.Remove(signature);
					}
                })
                .Add("Cancel")
            );
        }
    }
}

