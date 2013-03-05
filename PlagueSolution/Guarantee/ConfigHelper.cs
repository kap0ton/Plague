using System.Configuration;

namespace Guarantee
{
	class ConfigHelper
	{
		private const string OutputFolderPath = "OutputFolderPath";
		private const string InputFilePath = "InputFilePath";
		private const string CompletedFolderPath = "CompletedFolderPath";

		#region helper methods

		public static void UpdateConfigValues(string outputFolder, string inputFile, string completedFolder)
		{
			var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			UpdateConfigSection(configuration, OutputFolderPath, outputFolder);
			UpdateConfigSection(configuration, InputFilePath, inputFile);
			UpdateConfigSection(configuration, CompletedFolderPath, completedFolder);
			configuration.Save(ConfigurationSaveMode.Modified);
			ConfigurationManager.RefreshSection("appSettings");
		}

		private static void UpdateConfigSection(Configuration configuration, string key, string value)
		{
			if (configuration.AppSettings.Settings[key] != null)
			{
				configuration.AppSettings.Settings[key].Value = value;
			}
			else
			{
				configuration.AppSettings.Settings.Add(key, value);
			}
		}

		#endregion

		public static string OutputFolder
		{
			get { return ConfigurationManager.AppSettings[OutputFolderPath]; }
		}

		public static string InputFile
		{
			get { return ConfigurationManager.AppSettings[InputFilePath]; }
		}

		public static string CompletedFolder
		{
			get { return ConfigurationManager.AppSettings[CompletedFolderPath]; }
		}
	}
}
