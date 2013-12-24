using System;
using System.IO;


namespace Acr.MvvmCross.Plugins.Storage {
    
    public interface IStorageService {

        string NativePath(string path);        
        IFileInfo GetFile(string path);
        IDirectoryInfo GetDirectory(string path);

        // file
        bool FileExists(string path);
        void DeleteFile(string file);
        void MoveFile(string source, string destination);
        void CopyFile(string source, string destination, bool overwrite);
        Stream OpenReadFileStream(string path);
        Stream OpenWriteFileStream(string path);
    }
}
