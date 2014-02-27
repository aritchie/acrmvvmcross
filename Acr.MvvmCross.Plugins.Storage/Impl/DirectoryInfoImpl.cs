using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Acr.MvvmCross.Plugins.Storage.Impl {
    
    public class DirectoryInfoImpl : IDirectoryInfo {
        private readonly DirectoryInfo directory;
        private readonly Lazy<IDirectoryInfo> parent;
        private readonly Lazy<IDirectoryInfo> root; 


        public DirectoryInfoImpl(DirectoryInfo directory) {
            this.directory = directory;
            this.parent = new Lazy<IDirectoryInfo>(() => new DirectoryInfoImpl(this.directory.Parent));
            this.root = new Lazy<IDirectoryInfo>(() => new DirectoryInfoImpl(this.directory.Root));
        }


        #region IDirectoryInfo Members

        public string Name {
            get { return this.directory.Name; }
        }


        public string FullName {
            get { return this.directory.FullName; }
        }


        public IDirectoryInfo Parent {
            get { return this.parent.Value; }
        }
        

        public IDirectoryInfo Root {
            get { return this.root.Value; }
        }


        public DateTime LastWriteTime {
            get { return this.directory.LastWriteTime; }
        }


        public DateTime LastAccessTime {
            get { return this.directory.LastAccessTime; }
        }


        public bool Exists {
            get { return this.directory.Exists; }
        }


        public IEnumerable<IFileInfo> GetFiles(string searchPattern, bool recursive) {
#if WINDOWS_PHONE
            throw new NotImplementedException(); // TODO
#else
            var search = (recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            return this.directory
                .GetFiles(searchPattern ?? "*.*", search)
                .Select(x => new FileInfoImpl(x));
#endif
        }


        public IEnumerable<IDirectoryInfo> GetSubDirectories(string searchPattern, bool recursive) {
#if WINDOWS_PHONE
            throw new NotImplementedException(); // TODO
#else
            var search = (recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            return this.directory
                .GetDirectories(searchPattern ?? "*.*", search)
                .Select(x => new DirectoryInfoImpl(x));
#endif
        }


        public void CreateSubDirectory(string name) {
            this.directory.CreateSubdirectory(name);
            this.directory.Refresh();
        }


        public void Delete() {
            this.directory.Delete(true);
        }


        public void Create() {
            this.directory.Create();
            this.directory.Refresh();
        }


        public void Rename(string newName) {
            Directory.Move(this.Name, newName);
        }


        public void Refresh() {
            this.directory.Refresh();
        }


        //public void Copy(string destination) {
        //}


        public void Move(string destination) {
            Directory.Move(this.FullName, destination);
        }

        #endregion
    }
}
