using System.IO;

namespace Guarantee.Lib
{
	public class FileSystemHelper
	{
		public static void MoveDirectory(string sourceFolder, string destFolder)
		{
			CopyFolder(sourceFolder, destFolder);
			Directory.Delete(sourceFolder, true);
		}

		private static void CopyFolder(string sourceFolder, string destFolder)
		{
			if (!Directory.Exists(destFolder))
				Directory.CreateDirectory(destFolder);

			string[] files = Directory.GetFiles(sourceFolder);
			foreach (string file in files)
			{
				string name = Path.GetFileName(file);
				if (!string.IsNullOrEmpty(name))
				{
					string dest = Path.Combine(destFolder, name);
					File.Copy(file, dest, true);
				}
			}
			string[] folders = Directory.GetDirectories(sourceFolder);
			foreach (string folder in folders)
			{
				string name = Path.GetFileName(folder);
				if (!string.IsNullOrEmpty(name))
				{
					string dest = Path.Combine(destFolder, name);
					CopyFolder(folder, dest);
				}
			}
		}
	}
}
