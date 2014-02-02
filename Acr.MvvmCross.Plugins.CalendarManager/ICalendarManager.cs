using System;
using System.Threading.Tasks;


namespace Acr.MvvmCross.Plugins.CalendarManager {
    
    public interface ICalendarManager {

        Task<bool> RequestAccess();
    }
}
