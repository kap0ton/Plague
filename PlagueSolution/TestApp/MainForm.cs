using System;
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
	}
}
