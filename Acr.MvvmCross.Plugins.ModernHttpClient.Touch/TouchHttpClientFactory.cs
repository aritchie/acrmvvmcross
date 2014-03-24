using System;
using System.Net.Http;
using ModernHttpClient;


namespace Acr.MvvmCross.Plugins.ModernHttpClient.Touch {
    
    public class TouchHttpClientFactory : IHttpClientFactory {

        public HttpClient Create() {
            return new HttpClient(new NSUrlSessionHandler());
        }
    }
}