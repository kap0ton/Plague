using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using OfficeOpenXml;

namespace Guarantee.Lib
{
	public class PortfolioHelper
	{
		private const string Solutions = "Решения КК и договора";
		private const string Documents = "Документы по залогу";
		private const string Conclusions = "Заключения";

		private readonly string _filePath;
		private readonly string _folderPath;
		private readonly string _completedPath;

		public PortfolioHelper(string file, string folder, string completedFolder)
		{
			_filePath = file;
			_folderPath = folder;
			_completedPath = completedFolder;
		}

		public void Proceed()
		{
			var items = ParseFile();
			var b = CollectBorrowers(items);
			var e = CollectExisting();
			CreateFolderTree(b);
			MoveCompleted(b, e);
		}

		public void Proceed(BackgroundWorker worker)
		{
			worker.ReportProgress(0, "Обработка файла");
			var items = ParseFile();
			var b = CollectBorrowers(items);
			var e = CollectExisting();
			worker.ReportProgress(1, "Создание структуры папок");
			CreateFolderTree(b);
			worker.ReportProgress(2, "Перемещение завершенных заявок");
			MoveCompleted(b, e);
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
				for (var row = 2; row < totalRows; row++)
				{
					var name = worksheet.Cells[row, 1].Value;
					if (name != null)
					{
						var zalogType = worksheet.Cells[row, 14].Value;
						items.Add(new Zalog { Name = name.ToString(), ZalogType = zalogType.ToString() });
					}
				}
			}
			return items;
		}

		private void CreateFolderTree(IEnumerable<Borrower> items)
		{
			foreach (var borrower in items)
			{
				var dirName = borrower.Name;
				var rootFolder = Path.Combine(_folderPath, dirName);
				Directory.CreateDirectory(rootFolder);

				var solFolder = Path.Combine(rootFolder, Solutions);
				Directory.CreateDirectory(solFolder);

				var docFolder = Path.Combine(rootFolder, Documents);
				Directory.CreateDirectory(docFolder);

				var conFolder = Path.Combine(rootFolder, Conclusions);
				Directory.CreateDirectory(conFolder);

				foreach (var zalogType in borrower.ZalogTypes)
				{
					Directory.CreateDirectory(Path.Combine(docFolder, zalogType));
				}
			}
		}

		private void MoveCompleted(List<Borrower> items, List<Borrower> existing)
		{
			if (!Directory.Exists(_completedPath))
				Directory.CreateDirectory(_completedPath);

			Debug.WriteLine(items.Count());
			Debug.WriteLine(existing.Count());

			foreach (var e in existing)
			{
				var a = items.Where(x => x.Name == e.Name).ToList();
				if (!a.Any())
				{
					Debug.WriteLine(e.Name);
					var p = Path.Combine(_folderPath, e.Name);
					var n = Path.Combine(_completedPath, e.Name);
					try
					{
						FileSystemHelper.MoveDirectory(p, n);
					}
					catch (Exception ex)
					{
						Debug.WriteLine(ex.Message);
					}
				}
			}
		}

		private static List<Borrower> CollectBorrowers(IEnumerable<Zalog> items)
		{
			var res = (from t in items
			           group t by t.Name
			           into gt
			           select new Borrower
				           {
					           Name = gt.Key.Replace('"', ' ').Replace('.', ' ').Trim(),
					           ZalogTypes = gt.Select(x => x.ZalogType).ToList()
				           }).ToList();
			return res;
		}

		private List<Borrower> CollectExisting()
		{
			var r = new List<Borrower>();
			foreach (var directory in Directory.GetDirectories(_folderPath))
			{
				var b = new Borrower {Name = new DirectoryInfo(directory).Name};
				var p = Path.Combine(directory, Documents);
				if (Directory.Exists(p))
				{
					b.ZalogTypes = Directory.GetDirectories(p).Select(x => new DirectoryInfo(x).Name).ToList();
				}
				r.Add(b);
			}
			return r;
		}
	}
}
