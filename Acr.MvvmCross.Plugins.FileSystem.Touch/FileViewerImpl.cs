using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;


namespace Acr.MvvmCross.Plugins.FileSystem.Touch {

	public class FileViewerImpl : IFileViewer {

		public bool Open(IFile file) {
			using (var url = NSUrl.FromFilename(file.FullName)) {
				if (url == null || !url.IsFileUrl)
					return false;

				if (!UIApplication.SharedApplication.CanOpenUrl(url))
					return false;

				UIApplication.SharedApplication.OpenUrl(url);
				return true;

			};
		}
	}
}

