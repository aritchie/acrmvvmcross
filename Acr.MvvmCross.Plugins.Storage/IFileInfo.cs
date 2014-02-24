using System;
using System.IO;


namespace Acr.MvvmCross.Plugins.Storage {
    
    public interface IFileInfo : IFileSystemEntry {

        string Extension { get; }
        string MimeType { get; }
        long Length { get; }
        IDirectoryInfo Directory { get; }

        Stream Create();
        Stream OpenRead();
        Stream OpenWrite();
    }
}
