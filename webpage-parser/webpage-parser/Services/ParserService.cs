using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using HtmlAgilityPack;
using webpage_parser.Extensions;
using webpage_parser.Models;

namespace webpage_parser.Services
{
	public class ParserService : IParserService
	{
		private readonly IDocumentProcessService _documentProcessor;

		public ParserService(IDocumentProcessService documentProcessor)
		{
			_documentProcessor = documentProcessor;
		}

		public ParseResultModel GetParsedResults(string url)
		{
			try
			{
				var document = _documentProcessor.LoadHtmlDocument(url);
				var result = new ParseResultModel
				{
					ImageUrls = _documentProcessor.GetPictures(document, url),
					TopWordCounts = _documentProcessor.GetTopNWordCount(document)
				};
				return result;
			}
			catch (Exception ex)
			{
				//Log
			}
			return null;
		}
	}
}