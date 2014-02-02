using System.Reflection;
using Android.App;


[assembly: AssemblyTitle("Acr.MvvmCross.Plugins.CalendarManager.Droid")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

// Add some common permissions, these can be removed if not needed
[assembly: UsesPermission(Android.Manifest.Permission.WriteCalendar)]
[assembly: UsesPermission(Android.Manifest.Permission.ReadCalendar)]
