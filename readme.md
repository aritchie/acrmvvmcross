ACR MvvmCross Plugins
=====================

I loved Stuart Lodge's MvvmCross, so I built several services on it that I've used
on a few projects with great success.

* All plugins will be available through nuget
* WinStore & WinPhone platform plugins are in the works



##User Dialogs
Allows for messagebox style dialogs

* Action Sheet (multiple choice menu)
* Alert
* Prompt
* Confirm
* Loading
* Progress
* Toast

1. Droid progress & loading uses AndHUD
2. iOS progress & loading uses BTProgressHUD


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


##Network
I needed something beyond what MvvmCross had out of the box.  I had 
a requirement for detecting network state changes so that we could inform
the user when they were working in an offline state.

* INetworkService subscribes to INotifyPropertyChanged and monitors the device network status
* You can also use MvxMessenger to subscribe to NetworkStatusChangedMessage to watch for changes outside of your view model


##Settings
A simple settings library that works differently than the traditional setting plugins out there.  Instead, my approach was to work
with an observable dictionary.


##Device Info
Allows you to get the information of the device for auditing purposes

* Device Manufacturer
* Operating System and Version
* Front and rear facing cameras
* Screen Resolution