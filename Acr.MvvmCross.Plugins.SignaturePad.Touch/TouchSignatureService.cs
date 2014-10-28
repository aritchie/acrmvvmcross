using System;
using System.Collections.Generic;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Touch.Views.Presenters;


namespace Acr.MvvmCross.Plugins.SignaturePad.Touch {
	
	public class TouchSignatureService : AbstractSignatureService {

		public override void Request(Action<SignatureResult> onResult) {
			var controller = new MvxSignatureController(this.Configuration, onResult);
			this.Show(controller);
		}


		public override void Load(IEnumerable<DrawPoint> points) {
			var controller = new MvxSignatureController(this.Configuration, points);
			this.Show(controller);
		}


		private void Show(MvxSignatureController controller) {
			var presenter = Mvx.Resolve<IMvxTouchViewPresenter>();
			presenter.PresentModalViewController(controller, true);
		}
	}
}