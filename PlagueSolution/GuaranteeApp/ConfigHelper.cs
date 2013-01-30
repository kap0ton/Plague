using System.Configuration;

namespace GuaranteeApp
{
	class ConfigHelper
	{
		private const string OutputFolderPath = "OutputFolderPath";
		private const string InputFilePath = "InputFilePath";

		public static void UpdateConfigValues(string outputFolder, string inputFile)
		{
			var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

			//output folder path
			if (configuration.AppSettings.Settings[OutputFolderPath] != null)
			{
				configuration.AppSettings.Settings[OutputFolderPath].Value = outputFolder;
			}
			else
			{
				configuration.AppSettings.Settings.Add(OutputFolderPath, outputFolder);
			}

			//input file path
			if (configuration.AppSettings.Settings[InputFilePath] != null)
			{
				configuration.AppSettings.Settings[InputFilePath].Value = inputFile;
			}
			else
			{
				configuration.AppSettings.Settings.Add(InputFilePath, inputFile);
			}

			configuration.Save(ConfigurationSaveMode.Modified);
			ConfigurationManager.RefreshSection("appSettings");
		}

		public static string OutputFolder
		{
			get { return ConfigurationManager.AppSettings[OutputFolderPath]; }
		}

		public static string InputFile
		{
			get { return ConfigurationManager.AppSettings[InputFilePath]; }
		}
	}
}
