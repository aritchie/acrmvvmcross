﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;


namespace Acr.MvvmCross.ViewModels {
    
    public abstract class ListViewModel<T> : ViewModel {

        protected ListViewModel() {
            this.ItemSelected = new MvxCommand<T>(this.OnItemSelected);
        }


        public override async void Start() {
            base.Start();
            await this.DataBind();
        }


        #region Methods

        protected abstract Task<IList<T>> GetData();
        protected virtual void OnItemSelected(T obj) { }


        protected virtual async Task DataBind() {
            this.IsLoading = true;
            try {
                this.List = await this.GetData();
            }
            catch (Exception ex) {
                Mvx.Error(ex.ToString());
                // TODO: this.NotifyAppError();
            }
            this.IsLoading = false;
        }

        #endregion

        #region Properties

        private bool loading;
        public bool IsLoading {
            get { return this.loading; }
            set {
                if (this.loading == value)
                    return;

                this.loading = value;
                this.RaisePropertyChanged(() => this.IsLoading);
            }
        }


        public MvxCommand<T> ItemSelected { get; private set; }


        private bool noData = true;
        public bool NoData {
            get { return this.noData; }
            private set {
                if (this.noData == value)
                    return;

                this.noData = value;
                this.RaisePropertyChanged("NoData");
            }
        }


        private IList<T> list; 
        public IList<T> List {
            get { return this.list; }
            protected set {
                this.list = value;                
                this.RaisePropertyChanged("List");
                // TODO: this.NoData = value.IsEmpty();
            }
        }

        #endregion
    }
}