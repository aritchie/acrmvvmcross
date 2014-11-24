using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid.Platform;


namespace Acr.MvvmCross.Plugins.SignaturePad.Droid {
    
	public class DroidSignatureService : ISignatureService {
		internal SignaturePadConfiguration CurrentConfig { get; private set; }
		private TaskCompletionSource<SignatureResult> tcs;


		internal void Complete(SignatureResult result) {
			this.tcs.TrySetResult(result);
		}


		internal void Cancel() {
			this.tcs.TrySetResult(new SignatureResult(true, null, null));
		}


		public virtual Task<SignatureResult> Request(SignaturePadConfiguration config, CancellationToken cancelToken) {
			CurrentConfig = config ?? SignaturePadConfiguration.Default;

			this.tcs = new TaskCompletionSource<SignatureResult>();
			cancelToken.Register(this.Cancel);
			var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
			activity.StartActivity(typeof(SignaturePadActivity));

			return this.tcs.Task;
		}
    }
}