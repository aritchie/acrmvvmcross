using System;


namespace Acr.MvvmCross.Plugins.UserDialogs {
    
    public interface ISafeUserDialogService : IUserDialogService {

        // TODO: kill all current popups?
        // TODO: keep loading up but hand off to next guy waiting?
        bool IsPopupShowing { get; }
    }
}
