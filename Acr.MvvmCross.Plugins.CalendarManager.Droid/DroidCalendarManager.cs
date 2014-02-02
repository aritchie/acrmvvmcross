using System;
using System.Threading.Tasks;


namespace Acr.MvvmCross.Plugins.CalendarManager.Droid {

    public class DroidCalendarManager : ICalendarManager {

        #region ICalendarManager Members

        public Task<bool> RequestAccess() {
            throw new NotImplementedException();
        }

        #endregion
    }
}