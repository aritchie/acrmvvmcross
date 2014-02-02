using System.Reflection;
using Android.App;


[assembly: AssemblyTitle("Acr.MvvmCross.Plugins.ContactManager.Droid")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

// Add some common permissions, these can be removed if not needed
[assembly: UsesPermission(Android.Manifest.Permission.WriteContacts)]
[assembly: UsesPermission(Android.Manifest.Permission.ReadContacts)]