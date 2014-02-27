using System;
using System.Collections.Generic;


namespace Acr.MvvmCross.Plugins.BarCodeScanner {
    
    public class BarCodeScanningConfig {

        public bool? AutoRotate { get; set; }
        public string CharacterSet { get; set; }
        public int? DelayBetweenAnalyzingFrames { get; set; }
        public bool? PureBarcode { get; set; }
        public int? InitialDelayBeforeAnalyzingFrames { get; set; }
        public bool? TryHarder { get; set; }
        public bool? TryInverted { get; set; }
        public bool? UseFrontCameraIfAvailable { get; set; }

        public List<BarCodeFormat> Formats { get; set; }


        public BarCodeScanningConfig AddFormat(BarCodeFormat format) {
            this.Formats = this.Formats ?? new List<BarCodeFormat>();
            this.Formats.Add(format);
            return this;
        }
    }
}