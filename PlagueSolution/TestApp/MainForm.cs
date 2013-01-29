using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using log4net;
using log4net.Config;

namespace TestApp
{
	public partial class MainForm : Form
	{
		private ILog _logger;

		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_OnLoad(object sender, EventArgs e)
		{
			XmlConfigurator.Configure();
			_logger = LogManager.GetLogger(typeof (MainForm));
			//BasicConfigurator.Configure();

			_logger.Info("Application started");
		}

		private void MainForm_OnFormClosed(object sender, FormClosedEventArgs e)
		{
			_logger.Info("Application closed");
		}

		private string _dirPath = string.Empty;

		private void btnCreate_Click(object sender, EventArgs e)
		{
			try
			{
				_logger.Info("get assembly path");
				var path = Assembly.GetAssembly(typeof (MainForm)).Location;
				_logger.Info("get assembly directory");
				var dir = Path.GetDirectoryName(path);
				_logger.Info("combine new directory");
				_dirPath = Path.Combine(dir, "Folder");
				_logger.Info("create new directory");
				Directory.CreateDirectory(_dirPath);
				_logger.Info("directory created");

				btnDelete.Enabled = true;
				btnCreate.Enabled = false;
			}
			catch (Exception ex)
			{
				_logger.Error("directory creation failed", ex);
			}
			finally
			{
				btnCreate.Enabled = false;
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				_logger.Info("delete directory " + _dirPath);
				if (string.IsNullOrEmpty(_dirPath))
					throw new InvalidOperationException("directory path is empty");

				Directory.Delete(_dirPath, true);
				_logger.Info("directory deleted successfully");
				btnCreate.Enabled = true;
			}
			catch (Exception ex)
			{
				_logger.Error("directory deletion failed", ex);
			}
			finally
			{
				btnDelete.Enabled = false;
			}
		}
	}
}
