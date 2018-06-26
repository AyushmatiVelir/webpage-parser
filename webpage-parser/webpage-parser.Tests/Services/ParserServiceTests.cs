using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using webpage_parser.Services;

namespace webpage_parser.Tests.Services
{
	[TestFixture]
	public class ParserServiceTests
	{
		private ParserService _parser;
		private IDocumentProcessService _processor;

		[SetUp]
		public void SetUp()
		{
			_processor = Substitute.For<IDocumentProcessService>();
			_parser = new ParserService(_processor);
		}

		[Test]
		public void ParserService_GetParsedResults_EmptyString_ReturnsNull()
		{
			Assert.IsNull(_parser.GetParsedResults(string.Empty));
		}
	}
}
