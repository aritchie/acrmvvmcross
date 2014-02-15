using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Core;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;
using Cirrious.MvvmCross.ViewModels;
using Newtonsoft.Json;


namespace Acr.MvvmCross.Droid.Fragments {

    public abstract class MvxTabFragment : MvxFragment, TabHost.IOnTabChangeListener {
        private const string SavedTabIndexStateKey = "__savedTabIndex";

        private readonly Dictionary<string, TabInfo> _lookup = new Dictionary<string, TabInfo>();

        private readonly int _layoutId;
        private TabHost _tabHost;
        private TabInfo _currentTab;
        private readonly int _tabContentId;


        protected MvxTabFragment(int layoutId, int tabContentId) {
            _layoutId = layoutId;
            _tabContentId = tabContentId;
        }

        protected class TabInfo {
            public string Tag { get; private set; }
            public Type FragmentType { get; private set; }
            public Bundle Bundle { get; private set; }
            public IMvxViewModel ViewModel { get; private set; }

            public Fragment CachedFragment { get; set; }

            public TabInfo(string tag, Type fragmentType, Bundle bundle, IMvxViewModel viewModel) {
                Tag = tag;
                FragmentType = fragmentType;
                Bundle = bundle;
                ViewModel = viewModel;
            }
        }

        private class TabFactory
            : Java.Lang.Object
              , TabHost.ITabContentFactory {
            private readonly Context _context;

            public TabFactory(Context context) {
                _context = context;
            }

            public View CreateTabContent(String tag) {
                var v = new View(_context);
                v.SetMinimumWidth(0);
                v.SetMinimumHeight(0);
                return v;
            }
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(this._layoutId, null);

            if (savedInstanceState != null) {
                this.OnViewStateRestored(savedInstanceState);
            }
            this.InitializeTabHost(savedInstanceState, view);

            if (savedInstanceState != null) {
                this._tabHost.SetCurrentTabByTag(savedInstanceState.GetString(SavedTabIndexStateKey));
            }
            return view;
        }


        public override void OnSaveInstanceState(Bundle outState) {
            var state = this.ViewModel.SaveStateBundle();
            outState.PutObject("ViewModelBundle", state);
            outState.PutType("ViewModelType", this.ViewModel.GetType());
            outState.PutString(SavedTabIndexStateKey, _tabHost.CurrentTabTag);
            base.OnSaveInstanceState(outState);
        }


        public override void OnViewStateRestored(Bundle savedInstanceState) {
            base.OnViewStateRestored(savedInstanceState);
            var viewModelType = savedInstanceState.GetType("ViewModelType");
            var state = savedInstanceState.GetObject<MvxBundle>("ViewModelBundle");
            var request = MvxViewModelRequest.GetDefaultRequest(viewModelType);
            this.ViewModel = Mvx.Resolve<IMvxViewModelLoader>().LoadViewModel(request, state);
        }


        private void InitializeTabHost(Bundle args, View fragmentView) {
            _tabHost = (TabHost)fragmentView.FindViewById(Android.Resource.Id.TabHost);
            _tabHost.Setup();

            AddTabs(args);

            if (_lookup.Any())
                OnTabChanged(_lookup.First().Key);

            _tabHost.SetOnTabChangedListener(this);
        }

        protected abstract void AddTabs(Bundle args);

        protected void AddTab<TFragment>(string tagAndSpecName, string tabName, Bundle args,
                                         IMvxViewModel viewModel) {
            var tabSpec = this._tabHost.NewTabSpec(tagAndSpecName).SetIndicator(tabName);
            AddTab<TFragment>(args, viewModel, tabSpec);
        }

        protected void AddTab<TFragment>(Bundle args, IMvxViewModel viewModel, TabHost.TabSpec tabSpec) {
            var tabInfo = new TabInfo(tabSpec.Tag, typeof(TFragment), args, viewModel);
            AddTab(_tabHost, tabSpec, tabInfo);
            _lookup.Add(tabInfo.Tag, tabInfo);
        }

        private void AddTab(TabHost tabHost,
                            TabHost.TabSpec tabSpec,
                            TabInfo tabInfo) {
            // Attach a Tab view factory to the spec
            tabSpec.SetContent(new TabFactory(this.Activity));
            String tag = tabSpec.Tag;

            // Check to see if we already have a CachedFragment for this tab, probably
            // from a previously saved state.  If so, deactivate it, because our
            // initial state is that a tab isn't shown.
            tabInfo.CachedFragment = this.ChildFragmentManager.FindFragmentByTag(tag);
            if (tabInfo.CachedFragment != null && !tabInfo.CachedFragment.IsDetached) {
                var ft = this.ChildFragmentManager.BeginTransaction();
                ft.Detach(tabInfo.CachedFragment);
                ft.Commit();
                this.ChildFragmentManager.ExecutePendingTransactions();
            }

            tabHost.AddTab(tabSpec);
        }

        public virtual void OnTabChanged(string tag) {
            var newTab = this._lookup[tag];
            if (_currentTab != newTab) {
                var ft = this.ChildFragmentManager.BeginTransaction();
                if (_currentTab != null) {
                    if (_currentTab.CachedFragment != null) {
                        ft.Detach(_currentTab.CachedFragment);
                    }
                }
                if (newTab != null) {
                    if (newTab.CachedFragment == null) {
                        newTab.CachedFragment = Fragment.Instantiate(this.Activity,
                                                                     FragmentJavaName(newTab.FragmentType),
                                                                     newTab.Bundle);
                        FixupDataContext(newTab);
                        ft.Add(_tabContentId, newTab.CachedFragment, newTab.Tag);
                    }
                    else {
                        FixupDataContext(newTab);
                        ft.Attach(newTab.CachedFragment);
                    }
                }

                _currentTab = newTab;
                ft.Commit();
                this.ChildFragmentManager.ExecutePendingTransactions();
            }
        }

        protected virtual void FixupDataContext(TabInfo newTab) {
            var consumer = newTab.CachedFragment as IMvxDataConsumer;
            if (consumer == null)
                return;

            if (consumer.DataContext != newTab.ViewModel)
                consumer.DataContext = newTab.ViewModel;
        }

        protected virtual string FragmentJavaName(Type fragmentType) {
            var namespaceText = fragmentType.Namespace ?? "";
            if (namespaceText.Length > 0)
                namespaceText = namespaceText.ToLowerInvariant() + ".";
            return namespaceText + fragmentType.Name;
        }
    }
}