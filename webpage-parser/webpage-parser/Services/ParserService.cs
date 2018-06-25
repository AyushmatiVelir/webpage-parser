using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;

namespace webpage_parser.Services
{
	public class ParserService : IParserService
	{
		public bool IsValidUrl(string url)
		{
			Uri uriResult;
			return Uri.TryCreate(url, UriKind.Absolute, out uriResult)
				   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
		}

		public IEnumerable<string> GetPictures(string url)
		{
			var web = new HtmlWeb();
			HtmlDocument document = web.Load(url);
			var urls = document.DocumentNode.Descendants("img")
				.Select(e => e.GetAttributeValue("src", null))
				.Where(s => !string.IsNullOrEmpty(s));
			return urls;
		}

		public Dictionary<string, int> GetTopNWordCount(string url, int number)
		{
			throw new NotImplementedException();
		}
	}
}