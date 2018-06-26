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

		[SetUp]
		public void SetUp()
		{
			_parser = new ParserService();
		}

		[Test]
		public void ParserService_IsValidUrl_EmptyString_ReturnsFalse()
		{
			Assert.IsFalse(_parser.IsValidUrl(string.Empty));
		}

		[Test]
		public void ParserService_IsValidUrl_InvalidUrl_ReturnsFalse()
		{
			Assert.IsFalse(_parser.IsValidUrl("xyz12!!@548"));
		}

		[Test]
		public void ParserService_IsValidUrl_WithoutProtocolOrDomain_ReturnsFalse()
		{
			Assert.IsFalse(_parser.IsValidUrl("abc.com"));
		}

		[Test]
		public void ParserService_IsValidUrl_WithoutProtocol_ReturnsFalse()
		{
			Assert.IsFalse(_parser.IsValidUrl("www.google.com"));
		}

		[Test]
		public void ParserService_IsValidUrl_ValidUrl_ReturnsTrue()
		{
			Assert.IsTrue(_parser.IsValidUrl("http://www.google.com"));
		}

		[Test]
		public void ParserService_GetParsedResults_EmptyString_ReturnsNull()
		{
			Assert.IsNull(_parser.GetParsedResults(string.Empty));
		}
	}
}
