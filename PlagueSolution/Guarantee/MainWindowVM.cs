using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using Guarantee.Annotations;
using Guarantee.Lib;
using log4net;
using Application = System.Windows.Application;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Guarantee
{
	class MainWindowVM : INotifyPropertyChanged
	{
		#region Properties

		private readonly ILog _logger;

		private MainWindowModel _model;
		public MainWindowModel Model
		{
			get { return _model; }
			set
			{
				_model = value;
				OnPropertyChanged("Model");
			}
		}

		#endregion

		#region ctor

		public MainWindowVM(ILog logger)
		{
			_logger = logger;
			Model = new MainWindowModel();
		}

		#endregion

		#region Load

		public void Load()
		{
			Model.OutputFolder = ConfigHelper.OutputFolder;
			Model.InputFile = ConfigHelper.InputFile;
			Model.CompletedFolder = ConfigHelper.CompletedFolder;
		}

		#endregion

		#region Exit

		private RelayCommand _exitCdm;
		public ICommand ExitCmd
		{
			get { return _exitCdm ?? (_exitCdm = new RelayCommand(p => ExitApplication())); }
		}

		private static void ExitApplication()
		{
			Application.Current.Shutdown();
		}

		#endregion

		#region Open Output Folder

		private RelayCommand _openFolderCmd;
		public ICommand OpenFolderCmd
		{
			get { return _openFolderCmd ?? (_openFolderCmd = new RelayCommand(p => OpenFolderDialog())); }
		}

		private void OpenFolderDialog()
		{
			var fbd = new FolderBrowserDialog();

			if (!string.IsNullOrEmpty(Model.OutputFolder) && Directory.Exists(Model.OutputFolder))
				fbd.SelectedPath = Model.OutputFolder;

			if (fbd.ShowDialog() == DialogResult.OK)
			{
				Model.OutputFolder = fbd.SelectedPath;
			}
		}

		#endregion

		#region Open Input File

		private RelayCommand _openFileCmd;
		public ICommand OpenFileCmd
		{
			get { return _openFileCmd ?? (_openFileCmd = new RelayCommand(p => OpenFileDialog())); }
		}

		private void OpenFileDialog()
		{
			var ofd = new OpenFileDialog
				          {
					          Filter = "Excel Worksheet|*.xlsx|All Files|*.*"
				          };

			if (!string.IsNullOrEmpty(Model.InputFile) && File.Exists(Model.InputFile))
				ofd.FileName = Model.InputFile;

			if (ofd.ShowDialog() == true)
			{
				Model.InputFile = ofd.FileName;
			}
		}

		#endregion

		#region Open Completed Folder

		private RelayCommand _completedCmd;
		public ICommand CompletedCmd
		{
			get { return _completedCmd ?? (_completedCmd = new RelayCommand(p => OpenCompletedFolderDialog())); }
		}

		private void OpenCompletedFolderDialog()
		{
			var fbd = new FolderBrowserDialog();

			if (!string.IsNullOrEmpty(Model.CompletedFolder) && Directory.Exists(Model.CompletedFolder))
				fbd.SelectedPath = Model.CompletedFolder;

			if (fbd.ShowDialog() == DialogResult.OK)
			{
				Model.CompletedFolder = fbd.SelectedPath;
			}
		}

		#endregion

		#region Proceed

		private RelayCommand _proceedCmd;

		public ICommand ProceedCmd
		{
			get { return _proceedCmd ?? (_proceedCmd = new RelayCommand(p => ProceedApplication(), p => OnCanProceedApplication())); }
		}

		private void ProceedApplication()
		{
			Model.ProceedStatuses.Clear();
			ConfigHelper.UpdateConfigValues(Model.OutputFolder, Model.InputFile, Model.CompletedFolder);

			var helper = new PortfolioHelper(Model.InputFile, Model.OutputFolder, Model.CompletedFolder);
			var worker = new BackgroundWorker {WorkerReportsProgress = true};
			worker.DoWork += (o, ea) =>
				{
					_logger.Info("Proceed Started");
					Application.Current.Dispatcher.Invoke(new Action(() => Model.ProceedStatuses.Add("Запуск")));
					Model.IsProceedEnabled = false;
					helper.Proceed(worker);
				};
			worker.ProgressChanged += (o, ea) =>
				{
					_logger.Info(ea.UserState.ToString());
					Model.ProceedStatuses.Add(ea.UserState.ToString());
				};
			worker.RunWorkerCompleted += (o, ea) =>
				{
					if (ea.Error != null)
					{
						_logger.Error("error", ea.Error);
						Model.ProceedStatuses.Add("Ошибка");
					}
					else
					{
						_logger.Info("Proceed Ended");
						Model.ProceedStatuses.Add("Завершено");
					}
					Model.IsProceedEnabled = true;
					CommandManager.InvalidateRequerySuggested();
				};
			worker.RunWorkerAsync();
		}

		private bool OnCanProceedApplication()
		{
			return !string.IsNullOrEmpty(Model.InputFile)
			       && !string.IsNullOrEmpty(Model.OutputFolder)
			       && !string.IsNullOrEmpty(Model.CompletedFolder)
			       && Model.IsProceedEnabled;
		}

		#endregion

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}
