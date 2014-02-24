using System;
using System.Collections.Generic;


namespace Acr.MvvmCross.Plugins.Storage {
    
    public interface IDirectoryInfo : IFileSystemEntry {

        IDirectoryInfo Root { get; }
        IDirectoryInfo Parent { get; }

        IEnumerable<IFileInfo> GetFiles(string searchPattern = null, bool recursive = false);
        IEnumerable<IDirectoryInfo> GetSubDirectories(string searchPattern = null, bool recursive = false);
        
        void Refresh();
        void Create();
        void CreateSubDirectory(string name);
    }
}
