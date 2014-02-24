using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Acr.MvvmCross.Plugins.ExternalApp;
using Acr.MvvmCross.Plugins.Storage;
using Acr.MvvmCross.Plugins.UserDialogs;
using Cirrious.MvvmCross.ViewModels;


namespace Sample.Core.ViewModels {

    public class FileManagerViewModel : MvxViewModel {

        private readonly IUserDialogService dialogService;
        private readonly IExternalAppService externalApp;
        private readonly IStorageService store;

        private bool clipboardCut = false;
        private IFileSystemEntry clipboardNode;


        public FileManagerViewModel(IStorageService storageService, 
                                IExternalAppService externalApp,
                                IUserDialogService dialogService) {
            this.store = storageService;
            this.externalApp = externalApp;
            this.dialogService = dialogService;
            //this.rootAssembly = Assembly.Load(new AssemblyName("Sample.Core"));
        }


        public override void Start() {
            using (this.dialogService.Loading()) {
                var dir = this.store.GetDirectory(".");
                this.BindDirectory(dir.FullName);

                var test = this.store.GetDirectory("Test");
                if (test.Exists) {
                    test.Create();
                }
            }
        }

        #region Properties

        public MvxCommand<IFileSystemEntry> SelectNode {
            get {
                return new MvxCommand<IFileSystemEntry>(node => {
                    var isDir = (node is IDirectoryInfo);
                    var title = (isDir ? "Directory - " : "File - ");
                    this.dialogService.ActionSheet(
                        title + node.Name,
                        new SheetOption("Open", () => {
                            if (isDir) {
                                this.BindDirectory(node.FullName);
                            }
                            else if (!this.externalApp.Open(node.FullName)) {
                                this.dialogService.Alert("Could not open file - " + node.Name);
                            }
                        }),
                        new SheetOption("Delete", () => {
                            node.Delete();
                            this.BindDirectory(Path.GetDirectoryName(node.FullName));
                        }),
                        new SheetOption("Rename", () => {
                        }),
                        new SheetOption("Copy", () => {
                            this.clipboardCut = false;
                            this.clipboardNode = node;
                        }),
                        new SheetOption("Cut", () => {
                            this.clipboardCut = true;
                            this.clipboardNode = node;
                        }),
                        new SheetOption("Paste", () => {
                            if (this.clipboardNode == null) {
                                
                            }
                            else { 
                                this.clipboardNode = null;
                            }
                        })
                    );
                });
            }
        }

        // need back command
        public IMvxCommand CurrentDirectoryActions {
            get {
                return new MvxCommand(() => 
                    this.dialogService.ActionSheet(
                        "Actions",
                        
                        new SheetOption("Create Directory", () => 
                            this.dialogService.Prompt(
                                "New Directory Name",
                                r => {
                                    if (r.Ok) {
                                        if (String.IsNullOrWhiteSpace(r.Text)) {
                                            
                                        }
                                        // TODO: validate text - Path.GetInvalidPathChars()
                                        this.CurrentDirectory.CreateSubDirectory(r.Text);
                                    }
                                }
                            )
                        ),
                        new SheetOption("Create File", () => {
                            this.dialogService.Alert("TODO");
                        })
                    )
                );
            }
        }

        private IDirectoryInfo currentDirectory; 
        public IDirectoryInfo CurrentDirectory {
            get { return this.currentDirectory; }
            private set {
                this.currentDirectory = value;
                this.RaisePropertyChanged(() => this.CurrentDirectory);
            }
        }


        private IList<IFileSystemEntry> nodes;
        public IList<IFileSystemEntry> Nodes {
            get { return this.nodes; }
            private set {
                this.nodes = value;
                this.RaisePropertyChanged(() => this.Nodes);
                this.NoData = value.Any();
            }
        }


        private bool noData = true;
        public bool NoData {
            get { return this.noData; }
            private set {
                this.noData = value;
                this.RaisePropertyChanged(() => this.NoData);
            }
        }

        #endregion

        #region Methods

        private void BindDirectory(string path) {
            var dir = this.store.GetDirectory(path);
            this.BindDirectory(dir);
        }


        private void BindDirectory(IDirectoryInfo dir) {
            this.CurrentDirectory = dir;
            this.Nodes = this.store.GetFileSystemEntries(dir.FullName).ToList();
        }

        #endregion
    }
}
