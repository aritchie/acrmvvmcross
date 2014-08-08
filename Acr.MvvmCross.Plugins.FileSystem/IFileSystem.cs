using System;


namespace Acr.MvvmCross.Plugins.FileSystem {
    
    public interface IFileSystem {

        IDirectory AppData { get; }
        //IDirectory Roaming { get; }

        IDirectory GetDirectory(string path);
        IFile GetFile(string path);
    }
}
