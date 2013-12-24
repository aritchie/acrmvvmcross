using System;
using System.Collections.Generic;


namespace Acr.MvvmCross.Plugins.Storage {
    
    public interface IDirectoryInfo {

        string Name { get; }
        string FullName { get; }
        bool Exists { get; }
        DateTime LastWriteTime { get; }
        DateTime LastWriteTimeUtc { get; }
        DateTime LastAccessTime { get; }
        DateTime LastAccessTimeUtc { get; }
        IDirectoryInfo Root { get; }
        IDirectoryInfo Parent { get; }

        IEnumerable<IFileInfo> GetFiles(string searchPattern = null, bool recursive = false);
        IEnumerable<IDirectoryInfo> GetSubDirectories(string searchPattern = null, bool recursive = false);
        
        void Create();
        void CreateSubDirectory(string name);
        void Delete();
    }
}
