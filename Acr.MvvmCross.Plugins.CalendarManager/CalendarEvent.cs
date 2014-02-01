using System;


namespace Acr.MvvmCross.Plugins.CalendarManager {
    
    public class CalendarEvent {

        public string Id { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public string Details { get; set; }
    }
}
