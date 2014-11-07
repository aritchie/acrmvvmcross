@echo off
del *.nupkg
nuget push Acr.MvvmCross.Plugins.FileSystem.nuspec
nuget push Acr.MvvmCross.Plugins.DeviceInfo.nuspec
nuget push Acr.MvvmCross.Plugins.Settings.nuspec
nuget push Acr.MvvmCross.Plugins.UserDialogs.nuspec
nuget push Acr.MvvmCross.Plugins.Network.nuspec

rem nuget push Acr.MvvmCross.nuspec
rem nuget push Acr.MvvmCross.Plugins.BarCodeScanner.nuspec
rem nuget push Acr.MvvmCross.Plugins.SignaturePad.nuspec
rem nuget push Acr.MvvmCross.Pack.nuspec
pause