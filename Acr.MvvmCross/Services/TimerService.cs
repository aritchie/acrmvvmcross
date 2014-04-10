using System;
using System.Threading.Tasks;
using Acr.MvvmCross.Messages;
using Cirrious.MvvmCross.Plugins.Messenger;


namespace Acr.MvvmCross.Services {
    
    public class TimerService : ITimerService {
        private readonly IMvxMessenger messenger;


        public TimerService(IMvxMessenger messenger) {
            this.messenger = messenger;
        }


        private void DoWork() {
            Task
                .Delay(TimeSpan.FromSeconds(10))
                .ContinueWith(x => {
                    this.messenger.Publish(new TimerElapsedMessage(this));
                    if (this.IsStarted)
                        this.DoWork();
                });
        }


        public bool IsStarted { get; private set; }


        public void Start() {
            if (this.IsStarted)
                return;

            this.IsStarted = true;
            this.DoWork();
        }



        public void Stop() {
            this.IsStarted = false;
        }
    }
}
