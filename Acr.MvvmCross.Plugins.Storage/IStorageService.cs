using System;
using System.Collections.Generic;
using System.IO;


namespace Acr.MvvmCross.Plugins.Storage {
    
    public interface IStorageService {

        string NativePath(string path);
        IFileSystemEntry GetFileSystemEntry(string path);
        IEnumerable<IFileSystemEntry> GetFileSystemEntries(string path, string searchPattern = null);

        IFileInfo GetFile(string path);
        bool FileExists(string source);
        void CopyFile(string source, string destination, bool overwrite);
        void MoveFile(string source, string destination);
        Stream OpenWriteFileStream(string path);
        Stream OpenReadFileStream(string path);

        IDirectoryInfo GetDirectory(string path);
        bool DirectoryExists(string source);
        void MoveDirectory(string source, string destination);
        //void CopyDirectory(string source, string destination, Action<IFileSystemEntry, decimal> fileCopying);
    }
}
