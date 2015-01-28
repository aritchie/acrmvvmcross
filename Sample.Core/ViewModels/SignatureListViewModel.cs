using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Cirrious.MvvmCross.ViewModels;
using Acr.IO;
using Acr.MvvmCross.Plugins.SignaturePad;
using Acr.UserDialogs;


namespace Sample.Core.ViewModels {

    public class SignatureListViewModel : MvxViewModel {

        private const string FILE_FORMAT = "{0:dd-MM-yyyy_hh-mm-ss_tt}.jpg";
        private readonly IFileSystem fileSystem;
		private readonly IFileViewer fileViewer;
        private readonly ISignatureService signatureService;
        private readonly IUserDialogs dialogService;


        public SignatureListViewModel(IFileSystem fileSystem, 
								      IFileViewer fileViewer,
                                      IUserDialogs dialogService,
                                      ISignatureService signatureService){
			this.fileSystem = fileSystem;
			this.fileViewer = fileViewer;
            this.dialogService = dialogService;
            this.signatureService = signatureService;

            this.Configure = new MvxCommand(() => this.ShowViewModel<SignatureConfigurationViewModel>());
			this.Create = new MvxCommand(async () => await this.OnCreate());
            this.View = new MvxCommand<IFile>(this.OnView);
			this.List = new ObservableCollection<IFile>();
        }


        public override void Start() {
            base.Start();
			var files = this.fileSystem
				.Public
				.Files
                .ToList();

            foreach (var file in files)
                this.List.Add(file);
        }


        public ObservableCollection<IFile> List { get; private set; }
        public IMvxCommand Configure { get; private set; }
        public IMvxCommand Create { get; private set; }
        public MvxCommand<IFile> View { get; private set; }
        public MvxCommand<IFile> Delete { get; private set; }


		private async Task OnCreate() {
			var result = await this.signatureService.Request();
			if (result.Cancelled)
				return;

            var fileName = String.Format(FILE_FORMAT, DateTime.Now);
			var file = this.fileSystem.Public.CreateFile(fileName);

			using (var fs = file.Create())
				using (var stream = result.GetStream())
					stream.CopyTo(fs);
            
            this.List.Add(file);
        }


        private void OnView(IFile signature) {
            this.dialogService.ActionSheet(new ActionSheetConfig()
				.Add("View", () => {
					if (!this.fileViewer.Open(signature))
						this.dialogService.Alert("Cannot open file");
				})
                .Add("Delete", async () => {
                    var r = await this.dialogService.ConfirmAsync("Are you sure you want to delete " + signature.Name);
					if (r) {
						signature.Delete();
						this.List.Remove(signature);
					}
                })
                .Add("Cancel")
            );
        }
    }
}

