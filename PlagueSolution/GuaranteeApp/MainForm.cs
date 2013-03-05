using System;
using System.Windows.Forms;
using Guarantee.Lib;
using log4net;
using log4net.Config;

namespace GuaranteeApp
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private string OutputFolderPath { get; set; }
		private string InputFilePath { get; set; }

		private void btnExit_OnClick(object sender, EventArgs e)
		{
			_logger.Info("Exit Application");
			Application.Exit();
		}

		private void btnOutputFolder_OnClick(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(OutputFolderPath))
				fbdOutputFolder.SelectedPath = OutputFolderPath;

			if (fbdOutputFolder.ShowDialog() == DialogResult.OK)
			{
				OutputFolderPath = fbdOutputFolder.SelectedPath;
				txtOutputFolder.Text = OutputFolderPath;
			}
		}

		private void btnInputFile_OnClick(object sender, EventArgs e)
		{
			ofdInputFile.Filter = "Excel Worksheet|*.xlsx|All Files|*.*";

			if (!string.IsNullOrEmpty(InputFilePath))
				ofdInputFile.FileName = InputFilePath;

			if (ofdInputFile.ShowDialog() == DialogResult.OK)
			{
				InputFilePath = ofdInputFile.FileName;
				txtInputFile.Text = InputFilePath;
			}
		}

		private void btnProceed_OnClick(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(OutputFolderPath) || string.IsNullOrEmpty(InputFilePath))
			{
				MessageBox.Show("Выберите папку и файл!");
				return;
			}

			ConfigHelper.UpdateConfigValues(OutputFolderPath, InputFilePath);

			try
			{
				//todo: proceed input file and generate tree structure
				var helper = new PortfolioHelper(InputFilePath, OutputFolderPath, string.Empty);
				helper.Proceed();
			}
			catch (Exception ex)
			{
				_logger.Error("Proceed failed", ex);
				throw;
			}
		}

		private ILog _logger;

		private void MainForm_OnLoad(object sender, EventArgs e)
		{
			XmlConfigurator.Configure();
			_logger = LogManager.GetLogger(typeof(MainForm));
			_logger.Info("Application started");

			OutputFolderPath = ConfigHelper.OutputFolder;
			InputFilePath = ConfigHelper.InputFile;

			txtOutputFolder.Text = OutputFolderPath;
			txtInputFile.Text = InputFilePath;
		}
	}
}
