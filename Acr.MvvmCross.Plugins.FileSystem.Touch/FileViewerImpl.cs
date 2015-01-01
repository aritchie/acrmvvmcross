using System;
using Foundation;
using UIKit;


namespace Acr.MvvmCross.Plugins.FileSystem.Touch {

	public class FileViewerImpl : IFileViewer {

		public bool Open(IFile file) {
			var opened = false;

			using (var url = NSUrl.FromFilename(file.FullName)) {
				using (var controller = UIDocumentInteractionController.FromUrl(url)) {
					UIApplication.SharedApplication.InvokeOnMainThread (() => {
						controller.Delegate = new FileViewerInterationDelegate(); 
						opened = controller.PresentPreview(true);
					});
				}
			}
			return opened;
		}
	}


	public class FileViewerInterationDelegate : UIDocumentInteractionControllerDelegate {
		private readonly UIViewController controller;
		private readonly UIView view;


		public FileViewerInterationDelegate() : base() {
			this.controller = Utils.GetTopViewController();
			this.view = Utils.GetTopView();
		}


		public override UIViewController ViewControllerForPreview(UIDocumentInteractionController controller) {
			return this.controller;
		}


		public override UIView ViewForPreview (UIDocumentInteractionController controller) {
			return this.view;
		}
	}
}