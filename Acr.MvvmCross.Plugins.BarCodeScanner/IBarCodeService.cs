using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;


namespace Acr.MvvmCross.Plugins.BarCodeScanner {

	public interface IBarCodeService {

		Task<BarCodeResult> Read(BarCodeReadConfiguration config = null, CancellationToken cancelToken = default(CancellationToken));
		Stream Create(BarCodeCreateConfiguration config);
	}
}

