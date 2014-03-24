using System;
using System.Net.Http;


namespace Acr.MvvmCross.Plugins.ModernHttpClient {

    public interface IHttpClientFactory {

        HttpClient Create();
    }
}
