using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webpage_parser.Models
{
	public class ParseResultModel
	{
		public List<string> ImageUrls { get; set; }
		public Dictionary<string, int> TopWordCounts { get; set; }
	}
}