@echo off
del *.nupkg
nuget pack Acr.MvvmCross.nuspec
nuget pack Acr.MvvmCross.Plugins.BarCodeScanner.nuspec
nuget pack Acr.MvvmCross.Plugins.DeviceInfo.nuspec
nuget pack Acr.MvvmCross.Plugins.ExternalApp.nuspec
nuget pack Acr.MvvmCross.Plugins.Settings.nuspec
nuget pack Acr.MvvmCross.Plugins.Storage.nuspec
nuget pack Acr.MvvmCross.Plugins.UserDialogs.nuspec
nuget pack Acr.MvvmCross.Plugins.Network.nuspec
nuget pack Acr.MvvmCross.Plugins.TextToSpeech.nuspec
pause