using System;
using System.Threading.Tasks;
using Acr.MvvmCross.Messages;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;


namespace Acr.MvvmCross.Services {
    
    public class TimerService : ITimerService {

        public bool Enabled { get; set; }

        public TimerService() {
            
            Task.Factory.StartNew(() => {
                while (true) {
                    Task
                        .Delay(TimeSpan.FromSeconds(10))
                        .ContinueWith(x => {
                            if (this.Enabled)
                                return;

                            Mvx
                                .Resolve<IMvxMessenger>()
                                .Publish(new TimerElapsedMessage(this));
                        })
                        .Wait();
                } 
            });
        }
    }
}
