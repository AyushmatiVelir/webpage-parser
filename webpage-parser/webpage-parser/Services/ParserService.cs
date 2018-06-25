using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using HtmlAgilityPack;
using webpage_parser.Models;

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
		public ParseResultModel GetParsedResults(string url)
		{
			try
			{
				var document = LoadHtmlDocument(url);
				var result = new ParseResultModel
				{
					ImageUrls = GetPictures(document, url),
					TopWordCounts = GetTopNWordCount(document)
				};
				return result;
			}
			catch (Exception ex)
			{
				//Log
			}
			return null;
		}
		private List<string> GetPictures(HtmlDocument document, string url)
		{
			var imgSrcs = document.DocumentNode.Descendants("img")
				.Select(e => e.GetAttributeValue("src", null))
				.Where(s => !string.IsNullOrEmpty(s)).ToList();
			var urls = imgSrcs.Select(x => IsValidUrl(x) ? x : GetAbsoluteUrl(url, x)).ToList();
			return urls;
		}
		private static Dictionary<string, int> GetTopNWordCount(HtmlDocument document)
		{
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

		private static HtmlDocument LoadHtmlDocument(string url)
		{
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
		private static string GetAbsoluteUrl(string websiteUrl, string imgSrc)
		{
			return $"{GetUrlHost(websiteUrl)}/{imgSrc}";
		}
		private static string GetUrlHost(string websiteUrl)
		{
			var siteUri = new Uri(websiteUrl);
			return siteUri.GetLeftPart(UriPartial.Authority);
		}
		private static IEnumerable<string> GetWordsfromText(string text)
		{
			var punctuation = text.Where(char.IsPunctuation).Distinct().ToArray();
			var words = text.Split().Select(x => x.Trim(punctuation)).Where(x => !string.IsNullOrWhiteSpace(x)).Where(x => !(x.Length == 1 && char.IsSymbol(x[0])));
			return words;
		}

	}
}