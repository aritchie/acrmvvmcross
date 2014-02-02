using System;
using System.Threading.Tasks;


namespace Acr.MvvmCross.Plugins.CalendarManager.Touch {
    
    public class TouchCalendarManager : ICalendarManager {

        #region ICalendarManager Members

        public Task<bool> RequestAccess() {
            throw new NotImplementedException();
        }

        #endregion
    }
}