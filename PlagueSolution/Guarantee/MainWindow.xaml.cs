using System.Windows;

namespace Guarantee
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		private MainWindowVM _vm;
		public MainWindow()
		{
			InitializeComponent();
		}

		private void OnWindowLoaded(object sender, RoutedEventArgs e)
		{
			_vm = new MainWindowVM();
			DataContext = _vm;
			_vm.Load();
		}
	}
}
