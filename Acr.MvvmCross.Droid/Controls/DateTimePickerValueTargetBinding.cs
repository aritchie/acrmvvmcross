using System;
using System.Reflection;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Binding;
using Cirrious.MvvmCross.Binding.Bindings.Target;


namespace Acr.MvvmCross.Droid.Controls {

    public class DateTimePickerValueTargetBinding : MvxPropertyInfoTargetBinding<DateTimePicker> {

        public DateTimePickerValueTargetBinding(object target, PropertyInfo targetPropertyInfo) : base(target, targetPropertyInfo) {
            if (this.View == null) {
                MvxBindingTrace.Trace(MvxTraceLevel.Error, "Error - picker is null in DateTimePickerValueTargetBinding");
            }
            else {
                this.View.SelectedDateTimeChanged += this.OnSelectedDateTimeChanged;
            }
        }


        private void OnSelectedDateTimeChanged(object sender, DateTime e) {
            this.FireValueChanged(e);
        }


        public override MvxBindingMode DefaultMode {
            get { return MvxBindingMode.TwoWay; }
        }


        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);

            if (disposing && this.View != null) {
                this.View.SelectedDateTimeChanged -= this.OnSelectedDateTimeChanged;
            }
        }
    }
}