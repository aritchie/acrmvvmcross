using System;
using System.Threading.Tasks;


namespace Acr.MvvmCross.Plugins.CalendarManager.WinPhone {

    public class WinPhoneCalendarManager : ICalendarManager {

        #region ICalendarManager Members

        public Task<bool> RequestAccess() {
            throw new NotImplementedException();
        }

        #endregion
    }
}