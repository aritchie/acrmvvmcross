using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;


namespace Acr.MvvmCross.Plugins.BarCodeScanner.WinPhone {

    public class Plugin : IMvxPlugin {

        public void Load() {
            Mvx.RegisterSingleton<IBarCodeService>(new BarCodeService());
        }
    }

    public class BarCodeService : IBarCodeService
    {
        public Task<BarCodeResult> Read(BarCodeReadConfiguration config = null, CancellationToken cancelToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Stream Create(BarCodeCreateConfiguration config)
        {
            throw new NotImplementedException();
        }
    }
}