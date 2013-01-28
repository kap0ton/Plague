using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using OfficeOpenXml;

namespace Guarantee
{
	class PortfolioHelper
	{
		private readonly string _filePath;
		private readonly string _folderPath;

		public PortfolioHelper(string file, string folder)
		{
			_filePath = file;
			_folderPath = folder;
		}

		public void Proceed()
		{
			try
			{
				var items = ParseFile();
				CreateFolderTree(items);
				MessageBox.Show("done!");
			}
			catch (Exception ex)
			{
				MessageBox.Show("error " + ex.Message);
			}
		}

		private IEnumerable<Zalog> ParseFile()
		{
			var items = new List<Zalog>();

			if (string.IsNullOrEmpty(_filePath) || !File.Exists(_filePath))
				return items;

			var existingFile = new FileInfo(_filePath);
			using (var package = new ExcelPackage(existingFile))
			{
				ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

				var totalRows = worksheet.Dimension.End.Row;

				for (int row = 2; row < totalRows; row++)
				{
					var name = worksheet.Cells[row, 1].Value;
					if (name != null)
					{
						var zalogType = worksheet.Cells[row, 14].Value;
						items.Add(new Zalog {Name = name.ToString(), ZalogTypt = zalogType.ToString()});
					}
				}

				Console.WriteLine(items.Count);
			}
			return items;
		}

		private void CreateFolderTree(IEnumerable<Zalog> items)
		{
			var groups = (from t in items
			              group t by t.Name
			              into gt
			              select new
				                     {
					                     Name = gt.Key,
					                     Zalogs = gt.Select(x => x.ZalogTypt).ToList()
				                     }).ToList();
			Console.WriteLine(groups.Count());

			//var dics = Directory.GetDirectories(_folderPath);
			//Console.WriteLine("dirs: " + dics.Count());
			//foreach (var dic in dics)
			//{
			//	Directory.Delete(dic, true);
			//}

			foreach (var ggg in groups)
			{
				var dirName = ggg.Name.Replace('"', ' ');
				var rootFolder = Path.Combine(_folderPath, dirName);
				Directory.CreateDirectory(rootFolder);
				Directory.CreateDirectory(Path.Combine(rootFolder, "Решения КК и договора"));
				var docRoot = Path.Combine(rootFolder, "Документы по залогу");
				Directory.CreateDirectory(docRoot);
				Directory.CreateDirectory(Path.Combine(rootFolder, "Заключения"));
				
				foreach (var zalogType in ggg.Zalogs)
				{
					Directory.CreateDirectory(Path.Combine(docRoot, zalogType));
				}
			}
		}
	}

	class Zalog
	{
		public string Name { get; set; }
		public string ZalogTypt { get; set; }
	}
}
