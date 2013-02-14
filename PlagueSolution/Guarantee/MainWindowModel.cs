﻿using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Guarantee
{
	public class MainWindowModel : INotifyPropertyChanged
	{
		private string _outputFolder;
		public string OutputFolder
		{
			get { return _outputFolder; }
			set
			{
				_outputFolder = value;
				OnPropertyChanged("OutputFolder");
			}
		}

		private string _inputFile;
		public string InputFile
		{
			get { return _inputFile; }
			set
			{
				_inputFile = value;
				OnPropertyChanged("InputFile");
			}
		}

		private bool _isProceedEnabled;
		public bool IsProceedEnabled
		{
			get { return _isProceedEnabled; }
			set
			{
				_isProceedEnabled = value;
				OnPropertyChanged("IsProceedEnabled");
			}
		}

		private ObservableCollection<string> _proceedStatuses;
		public ObservableCollection<string> ProceedStatuses
		{
			get { return _proceedStatuses ?? (_proceedStatuses = new ObservableCollection<string>()); }
		}

		public MainWindowModel()
		{
			IsProceedEnabled = true;
		}

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler != null)
				handler(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}
