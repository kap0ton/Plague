using System.Collections.Generic;

namespace Guarantee.Lib
{
	internal class Borrower
	{
		public string Name { get; set; }
		public List<string> ZalogTypes { get; set; }

		public Borrower()
		{
			ZalogTypes = new List<string>();
		}
	}

	class Zalog
	{
		public string Name { get; set; }
		public string ZalogType { get; set; }
	}
}
