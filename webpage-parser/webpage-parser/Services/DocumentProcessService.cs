using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using HtmlAgilityPack;
using webpage_parser.Extensions;

namespace webpage_parser.Services
{
	public class DocumentProcessService : IDocumentProcessService
	{
		public HtmlDocument LoadHtmlDocument(string url)
		{
			if (string.IsNullOrWhiteSpace(url) || !url.IsValidUrl()) return null;

			var web = new HtmlWeb();
			ServicePointManager.SecurityProtocol =
				SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
			HtmlDocument document = web.Load(url);
			document.DocumentNode.Descendants()
				.Where(n => n.Name == "script" || n.Name == "style")
				.ToList()
				.ForEach(n => n.Remove());
			return document;
		}

		public List<string> GetPictures(HtmlDocument document, string url)
		{
			if (document == null) return null;

			try
			{
				var imgSrcs = document.DocumentNode.Descendants("img")
					.Select(e => e.GetAttributeValue("src", null))
					.Where(s => !string.IsNullOrEmpty(s)).ToList();
				var urls = imgSrcs.Select(x => x.IsValidUrl() ? x : x.GetAbsoluteUrl(url)).Distinct().ToList();
				return urls;
			}
			catch
			{
				return null;
			}
		}

		public Dictionary<string, int> GetTopNWordCount(HtmlDocument document)
		{
			if (document == null) return null;

			try
			{
				var words = new List<string>();
				foreach (HtmlNode node in document.DocumentNode.SelectNodes("//text()"))
				{
					if (string.IsNullOrWhiteSpace(node.InnerText)) continue;
					var decodedHtmlText = HttpUtility.HtmlDecode(node.InnerText);
					var trimmedText = decodedHtmlText.Trim();
					var removedNewLineText = trimmedText.Replace(Environment.NewLine, string.Empty);
					words.AddRange(removedNewLineText.GetWordsfromText());
				}

				var topWordCounts = words.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count())
					.OrderByDescending(entry => entry.Value).Take(8);
				return topWordCounts.ToDictionary(pair => pair.Key, pair => pair.Value);
			}
			catch
			{
				return null;
			}
		}

	}
}