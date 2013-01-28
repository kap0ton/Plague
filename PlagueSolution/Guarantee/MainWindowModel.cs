using System.ComponentModel;
using System.Runtime.CompilerServices;
using Guarantee.Annotations;

namespace Guarantee
{
	class MainWindowModel : INotifyPropertyChanged
	{
		private string _outputFolder;
		public string OutputFolder
		{
			get { return _outputFolder; }
			set
			{
				_outputFolder = value;
				OnPropertyChanged();
			}
		}

		private string _inputFile;
		public string InputFile
		{
			get { return _inputFile; }
			set
			{
				_inputFile = value;
				OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
				handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
