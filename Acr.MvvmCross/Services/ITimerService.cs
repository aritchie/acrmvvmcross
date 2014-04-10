using System;


namespace Acr.MvvmCross.Services {
    
    public interface ITimerService {

        bool IsStarted { get; }
        void Start();
        void Stop();
    }
}
