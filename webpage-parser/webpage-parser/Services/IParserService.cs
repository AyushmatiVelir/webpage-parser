using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webpage_parser.Services
{
	public interface IParserService
	{
		bool IsValidUrl(string url);

		IEnumerable<string> GetPictures(string url);

		Dictionary<string, int> GetTopNWordCount(string url, int number);

	}


}