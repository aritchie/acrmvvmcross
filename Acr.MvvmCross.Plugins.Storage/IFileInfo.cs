using System;
using System.IO;


namespace Acr.MvvmCross.Plugins.Storage {
    
    public interface IFileInfo {

        string Name { get; }
        string Extension { get; }
        string FullName { get; }
        string MimeType { get; }
        bool Exists { get; }
        long Length { get; }
        IDirectoryInfo Directory { get; }
        DateTime LastWriteTime { get; }
        DateTime LastWriteTimeUtc { get; }
        DateTime LastAccessTime { get; }
        DateTime LastAccessTimeUtc { get; }
        
        void Delete();
        Stream Create();
        Stream OpenRead();
        Stream OpenWrite();
    }
}
