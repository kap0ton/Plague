using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using Application = System.Windows.Application;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Guarantee
{
	class MainWindowVM
	{
		private readonly MainWindowModel _model;
		public MainWindowModel Model
		{
			get { return _model; }
		}

		public MainWindowVM()
		{
			_model = new MainWindowModel();
		}

		private RelayCommand _exitCdm;
		public ICommand ExitCmd
		{
			get { return _exitCdm ?? (_exitCdm = new RelayCommand(p => ExitApplication())); }
		}

		private static void ExitApplication()
		{
			Application.Current.Shutdown();
		}

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

		private RelayCommand _proceedCmd;
		public ICommand ProceedCmd
		{
			get
			{
				return _proceedCmd ?? (_proceedCmd = new RelayCommand(p=>ProceedApplication(), p=>OnCanProceedApplication()));
			}
		}

		private void ProceedApplication()
		{
			ConfigHelper.UpdateConfigValues(Model.OutputFolder, Model.InputFile);
			
			//todo: proceed input file and generate tree structure
			var helper = new PortfolioHelper(Model.InputFile, Model.OutputFolder);
			helper.Proceed();
		}

		private bool OnCanProceedApplication()
		{
			return !string.IsNullOrEmpty(Model.InputFile) && !string.IsNullOrEmpty(Model.OutputFolder);
		}

		public void Load()
		{
			Model.OutputFolder = ConfigHelper.OutputFolder;
			Model.InputFile = ConfigHelper.InputFile;
		}
	}
}
