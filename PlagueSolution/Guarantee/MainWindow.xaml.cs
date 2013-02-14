using System.Windows;
using log4net;
using log4net.Config;

namespace Guarantee
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		private ILog _logger;
		private MainWindowVM _vm;
		public MainWindow()
		{
			InitializeComponent();
		}

		private void OnWindowLoaded(object sender, RoutedEventArgs e)
		{
			XmlConfigurator.Configure();
			_logger = LogManager.GetLogger(typeof(MainWindow));
			_logger.Info("Start Application");

			_vm = new MainWindowVM(_logger);
			DataContext = _vm;
			_vm.Load();
		}

		private void OnWindowClosed(object sender, System.EventArgs e)
		{
			_logger.Info("Exit Application");
		}
	}
}
