using System;


namespace Acr.MvvmCross.Plugins.CalendarManager {

    public enum CalendarRepeat {
        Never,
        EveryDay,
        EveryWeek,
        EveryTwoWeeks,
        EveryMonth,
        EveryYear
    }


    public enum AlertWarning {
        None,
        AtTimeOfEvent,
        FiveMins,
        FifteenMins,
        ThirtyMins,
        Hour,
        TwoHours,
        Day,
        TwoDays,
        Week
    }


    public class CalendarEvent {

        public string Id { get; set; }
        public DateTimeOffset StartDateTime { get; set; }
        public DateTimeOffset EndDateTime { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Url { get; set; }
        public string Notes { get; set; }
    }
}
