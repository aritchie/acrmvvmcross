using System;
using Cirrious.MvvmCross.Plugins.Messenger;


namespace Acr.MvvmCross.Messages {
    
    public class TimerElapsedMessage : MvxMessage {

        public TimerElapsedMessage(object sender) : base(sender) { }
    }
}
