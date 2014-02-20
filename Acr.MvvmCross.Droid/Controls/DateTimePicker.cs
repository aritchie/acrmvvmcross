using System;
using System.Globalization;
using Android.App;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using Orientation = Android.Widget.Orientation;


namespace Acr.MvvmCross.Droid.Controls {
    
    public class DateTimePicker : LinearLayout {
        private Context context;

        private DatePickerDialog datePicker;
        private TimePickerDialog timePicker;
        private TextView dateDisplay;
        private TextView timeDisplay;
        private DateTime selectedDateTime;

        
        public DateTimePicker(Context context) : base(context) {
            this.Init(context, null);
        }


        public DateTimePicker(Context context, IAttributeSet attrs) : base(context, attrs) {
            this.Init(context, attrs);
        }


        private void Init(Context context, IAttributeSet attrs) {
            this.context = context;
            this.Orientation = Orientation.Horizontal;
            this.ShowDate = true;
            this.ShowTime = true;
            if (attrs != null) {
                //var a = context.Theme.ObtainStyledAttributes(attrs, Resource.Styleable.DateTimePicker, 0, 0);
                //try { 
                //    this.DateFormat = a.GetString(Resource.Styleable.DateTimePicker_date_format);
                //    this.TimeFormat = a.GetString(Resource.Styleable.DateTimePicker_time_format);
                //    this.ShowTime = a.GetBoolean(Resource.Styleable.DateTimePicker_show_time, true);
                //    this.ShowDate = a.GetBoolean(Resource.Styleable.DateTimePicker_show_date, true);
                //}
                //finally {
                //    a.Recycle();
                //}
            }

            if (!this.ShowDate && !this.ShowTime)
                throw new ArgumentException("Both date & time are not being shown");
            
            var f = CultureInfo.CurrentCulture.DateTimeFormat;
            this.DateFormat = this.DateFormat ?? "{0:" + f.LongDatePattern + "}";
            this.TimeFormat = this.TimeFormat ?? "{0:" + f.LongTimePattern + "}";

            var now = DateTime.Now;
            this.selectedDateTime = now;

            this.datePicker = new DatePickerDialog(context, this.OnDateSelected, now.Year, now.Month - 1, now.Day);                
            this.dateDisplay = new TextView(context) {
                LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.WrapContent, 1)
            };
            this.dateDisplay.Click += this.OnDateClick;
            this.timePicker = new TimePickerDialog(context, this.OnTimeSelected, now.Hour, now.Minute, false);
            this.timeDisplay = new TextView(this.context) {
                LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.WrapContent, 2)
            };
            this.timeDisplay.Click += this.OnTimeClick;

            if (this.ShowDate) { 
                this.AddView(this.dateDisplay);
            }
            if (this.ShowTime) { 
                this.AddView(this.timeDisplay);
            }
            this.SetLabels();
        }

        #region Internals

        protected virtual void OnDateTimeChanged() {
            if (this.SelectedDateTimeChanged != null) {
                this.SelectedDateTimeChanged(this, this.SelectedDateTime);
            }
        }


        private void OnDateClick(object sender, EventArgs args) {
            this.datePicker.Show();
        }


        private void OnTimeClick(object sender, EventArgs args) {
            this.timePicker.Show();
        }


        private void OnDateSelected(object sender, DatePickerDialog.DateSetEventArgs args) {
            this.SelectedDateTime = new DateTime(
                args.Year, 
                args.MonthOfYear + 1,
                args.DayOfMonth, 
                this.selectedDateTime.Hour, 
                this.selectedDateTime.Minute, 
                0
            );
            this.OnDateTimeChanged();
        }


        private void OnTimeSelected(object sender, TimePickerDialog.TimeSetEventArgs args) {
            this.SelectedDateTime = new DateTime(
                this.selectedDateTime.Year, 
                this.selectedDateTime.Month, 
                this.selectedDateTime.Day, 
                args.HourOfDay,
                args.Minute,
                0
            );
            this.OnDateTimeChanged();
        }


        private void SetLabels() {
            this.dateDisplay.Text = String.Format(this.DateFormat, this.SelectedDateTime);
            this.timeDisplay.Text = String.Format(this.TimeFormat, this.SelectedDateTime);
        }


        protected override void Dispose(bool disposing) {
            if (disposing) {
                this.dateDisplay.Click -= this.OnDateClick;
                this.dateDisplay.Dispose();
                this.datePicker.Dispose();

                this.timeDisplay.Click -= this.OnTimeClick;
                this.timeDisplay.Dispose();
                this.timePicker.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Properties

        public string TimeFormat { get; set; }
        public string DateFormat { get; set; }
        public bool ShowDate { get; set; }
        public bool ShowTime { get; set; }

        public event EventHandler<DateTime> SelectedDateTimeChanged;

        public DateTime SelectedDateTime {
            get { return this.selectedDateTime; }
            set {
                this.selectedDateTime = value;
                this.datePicker.UpdateDate(value);
                this.timePicker.UpdateTime(value.Hour, value.Minute);
                this.SetLabels();
            }
        }

        #endregion
    }
}