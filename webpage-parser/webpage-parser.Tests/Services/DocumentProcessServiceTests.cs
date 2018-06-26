using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using NUnit.Framework;
using webpage_parser.Services;

namespace webpage_parser.Tests.Services
{
	[TestFixture]
	public class DocumentProcessServiceTests
	{
		private IDocumentProcessService _processor;

		[SetUp]
		public void SetUp()
		{
			_processor = new DocumentProcessService();
		}

		[Test]
		public void DocumentProcessService_LoadHtmlDocument_NullArgument_ReturnsNull()
		{
			Assert.IsNull(_processor.LoadHtmlDocument(string.Empty));
		}

		[Test]
		public void DocumentProcessService_LoadHtmlDocument_EmptyString_ReturnsNull()
		{
			Assert.IsNull(_processor.LoadHtmlDocument(string.Empty));
		}

		[Test]
		public void DocumentProcessService_LoadHtmlDocument_InvalidUrl_ReturnsNull()
		{
			Assert.IsNull(_processor.LoadHtmlDocument("helloworld"));
		}

		[Test]
		public void DocumentProcessService_GetPictures_NullArguments_ReturnsNull()
		{
			Assert.IsNull(_processor.GetPictures(null,null));
		}

		[Test]
		public void DocumentProcessService_GetPictures_NullDocument_ReturnsNull()
		{
			Assert.IsNull(_processor.GetPictures(null, "abc"));
		}

		[Test]
		public void DocumentProcessService_GetPictures_EmptyDocument_DoesNotThrow()
		{
			var doc= new HtmlDocument();
			Assert.DoesNotThrow(()=>_processor.GetPictures(doc, "abc"));
		}

		[Test]
		public void DocumentProcessService_GetTopNWordCount_NullArgument_ReturnsNull()
		{
			Assert.IsNull(_processor.GetTopNWordCount(null));
		}

		[Test]
		public void DocumentProcessService_GetTopNWordCount_EmptyDocument_DoesNotThrow()
		{
			var doc = new HtmlDocument();
			Assert.DoesNotThrow(()=>_processor.GetTopNWordCount(doc));
		}
	}
}
