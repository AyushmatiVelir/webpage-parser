using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

		private string GetAbsoluteUrl(string websiteUrl, string imgSrc)
		{
			return $"{GetUrlHost(websiteUrl)}/{imgSrc}";
		}

		private string GetUrlHost(string websiteUrl)
		{
			var siteUri = new Uri(websiteUrl);
			return siteUri.GetLeftPart(UriPartial.Authority);
		}

		public IEnumerable<string> GetPictures(string url)
		{
			var web = new HtmlWeb();
			HtmlDocument document = web.Load(url);
			var imgSrcs = document.DocumentNode.Descendants("img")
				.Select(e => e.GetAttributeValue("src", null))
				.Where(s => !string.IsNullOrEmpty(s)).ToList();
			var urls = imgSrcs.Select(x => IsValidUrl(x) ? x : GetAbsoluteUrl(url, x));
			return urls;
		}

		public Dictionary<string, int> GetTopNWordCount(string url, int number)
		{
			var web = new HtmlWeb();
			HtmlDocument document = web.Load(url);
			var words = new List<string>();
			foreach (HtmlNode node in document.DocumentNode.SelectNodes("//text()"))
			{
				if (!string.IsNullOrWhiteSpace(node.InnerText))
				{
					var decodedHtmlText = HttpUtility.HtmlDecode(node.InnerText);
					var trimmedText = decodedHtmlText.Trim();
					var removedNewLineText = trimmedText.Replace(Environment.NewLine, string.Empty);
					words.AddRange(GetWordsfromText(removedNewLineText));
				}
			}
			var topWordCounts = words.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count()).OrderByDescending(entry => entry.Value).Take(8);
			return topWordCounts.ToDictionary(pair => pair.Key, pair => pair.Value);
		}

		private List<string> GetWordsfromText(string text)
		{
			var punctuation = text.Where(char.IsPunctuation).Distinct().ToArray();
			var words = text.Split().Select(x => x.Trim(punctuation)).Where(x => !string.IsNullOrWhiteSpace(x)).Where(x => !(x.Length == 1 && char.IsSymbol(x[0]))).ToList();
			return words;
		}
	}
}