using System;
using System.Net.Http;
using ModernHttpClient;


namespace Acr.MvvmCross.Plugins.ModernHttpClient.Droid {
    
    public class DroidHttpClientFactory : IHttpClientFactory {

        #region IHttpClientFactory Members

        public HttpClient Create() {
            //var handler = new HttpClientHandler();
            //if (handler.SupportsAutomaticDecompression) {
            //    handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            //}
            return new HttpClient(new OkHttpNetworkHandler());
        }

        #endregion
    }
}