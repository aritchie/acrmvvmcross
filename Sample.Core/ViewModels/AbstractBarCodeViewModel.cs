using System;
using System.Linq;
using System.Collections.Generic;
using Cirrious.MvvmCross.ViewModels;
using Acr.BarCodes;
using Acr.UserDialogs;


namespace Sample.Core {

	public abstract class AbstractBarCodeViewModel : MvxViewModel {

		protected IBarCodes Scanner { get; private set; }
		protected IUserDialogs Dialogs { get; private set; }


		protected AbstractBarCodeViewModel(IBarCodes scanner, IUserDialogs dialogs) {
			this.Scanner = scanner;
			this.Dialogs = dialogs;

			var list = Enum
				.GetNames(typeof(BarCodeFormat))
				.ToList();
			list.Insert(0, "Any");
			this.Formats = list;
			this.SelectedFormat = "Any";
		}


		private IMvxCommand pickFormatCmd;
		public IMvxCommand PickFormat {
			get {
				this.pickFormatCmd = this.pickFormatCmd ?? new MvxCommand(() => {
					var cfg = new ActionSheetConfig();
					foreach (var format in this.Formats)
						cfg.Add(format, () => this.SelectedFormat = format);

					this.Dialogs.ActionSheet(cfg);
				});
				return this.pickFormatCmd;
			}
		}


		public IList<string> Formats { get; private set; }


		private string selectedFormat;
		public string SelectedFormat {
			get { return this.selectedFormat; }
			set { this.SetProperty(ref this.selectedFormat, value); }
		}
	}
}

