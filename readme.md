ACR MvvmCross Plugins
=====================

I loved Stuart Lodge's MvvmCross, so I built several services on it that I've used
on a few projects with great success.

* All plugins will be available through nuget
* WinStore & WinPhone platform plugins are in the works


##Bar Code Scanner
This is based on Redth's ZXing.Net.Mobile.

    new MvxCommand(async () => {
        var scan = Mvx.Resolve<IBarCodeScanner>();
        var r = await scan.Read(flashlightText: "Turn on flashlight", cancelText: "Cancel");

        Result = (r.Success 
            ? String.Format("Barcode Result - Format: {0} - Code: {1}", r.Format, r.Code)
            : "Cancelled barcode scan"
        );
    });


##Cache
TODO


##Device Info
Allows you to get the information of the device for auditing purposes


##External App
This allows you to open documents in other applications.  Useful if you have a document
repository style app

    if (ExternalAppService.Open("document.pdf")) {
        // app selection is already open
    }   
    else {
        throw new Exception("BOOM");
    }


##Network
I needed something beyond what MvvmCross had out of the box.  I had
a requirement for detecting network state changes so that we could inform
the user when they were working in an offline state.

    public MyViewModel(INetworkService networkService) {
        MvxSubscriptionToken networkSubscriptionToken = networkService.Subscribe(e => OnNetworkChange(e.Status));
        OnNetworkChange(networkService.CurrentStatus);
    }

    private void OnNetworkChange(MvxNetworkStatus e) {
        if (!e.IsConnected) {
            NetworkInfo = "Not Connected";
        }
        else if (e.IsMobile) {
            NetworkInfo = "Mobile Connection";
        }
        else {
            NetworkInfo = "WiFi Connection";
        }
    }


##Settings
Just a simple setting access platform service


##Storage
Allows for PCL use of 

* DirectoryInfo
* FileInfo
* and File Streams


##User Dialogs
Allows for messagebox style dialogs

* Alert
* Prompt
* Confirm
* Loading
* Progress
* Toast

1. Droid progress & loading uses AndHUD
2. iOS progress & loading uses BTProgressHUD
