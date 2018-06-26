using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace webpage_parser.Services
{
	public interface IDocumentProcessService
	{
		HtmlDocument LoadHtmlDocument(string url);
		List<string> GetPictures(HtmlDocument document, string url);
		Dictionary<string, int> GetTopNWordCount(HtmlDocument document);
	}
}
