using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Touch.Views.Presenters;


namespace Acr.MvvmCross.Plugins.SignaturePad.Touch {
	
	public class TouchSignatureService : ISignatureService {

		public Task<SignatureResult> Request(SignaturePadConfiguration config = null, CancellationToken cancelToken = default(CancellationToken)) {
			config = config ?? SignaturePadConfiguration.Default;
			var tcs = new TaskCompletionSource<SignatureResult>();
			var controller = new MvxSignatureController(config, x => tcs.TrySetResult(x));

			var presenter = Mvx.Resolve<IMvxTouchViewPresenter>();
			presenter.PresentModalViewController(controller, true);
			cancelToken.Register(() => {
				tcs.TrySetCanceled();
				controller.DismissViewController(true, null);
			});
			return tcs.Task;
		}
	}
}