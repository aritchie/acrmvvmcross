using System;
using System.IO;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid.Platform;
using System.Collections.Generic;


namespace Acr.MvvmCross.Plugins.SignaturePad.Droid {
    
    public class DroidSignatureService : AbstractSignatureService {

        internal static IEnumerable<DrawPoint> CurrentPoints { get; private set; }
        internal static SignaturePadConfiguration CurrentConfig { get; private set; }
        internal static Action<SignatureResult> OnResult { get; private set; }


        public override void Request(Action<SignatureResult> onResult) { 
            CurrentPoints = null;
            CurrentConfig = this.Configuration;
            OnResult = onResult;
            var topActivity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
            topActivity.StartActivity(typeof(SignaturePadActivity));
        }


        public override void Load(IEnumerable<DrawPoint> points) {
            CurrentConfig = this.Configuration;
            CurrentPoints = points;
            OnResult = null;
        }
    }
}