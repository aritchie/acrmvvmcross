using System;
using System.Collections.Generic;
using System.Globalization;
using Cirrious.CrossCore.Converters;


namespace Acr.MvvmCross.Converters {
    
    public class FileSizeConverter : MvxValueConverter<long> {
        private static readonly List<string> suffixes = new List<string> { "bytes", "KB", "MB", "GB", "TB" };


        // TODO: multilingual on types above?
        protected override object Convert(long fileSize, Type targetType, object parameter, CultureInfo culture) {
            var pow = Math.Floor((fileSize > 0 ? Math.Log(fileSize) : 0) / Math.Log(1024));
            pow = Math.Min(pow, suffixes.Count - 1);
            var value = fileSize / Math.Pow(1024, pow);
            return value.ToString(pow == 0 ? "F0" : "F2") + " " + suffixes[(int)pow];
        }
    }
}
