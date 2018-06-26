using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using webpage_parser.Extensions;

namespace webpage_parser.Tests.Extensions
{
	[TestFixture]
	public class UrlExtensionsTests
	{
		[Test]
		public void GetAbsoluteUrl_EmptyString_DoesNotThrow()
		{
			string url = "test";
			Assert.DoesNotThrow(() => url.GetAbsoluteUrl(string.Empty));
		}

		[Test]
		public void GetAbsoluteUrl_EmptyString_ReturnsRelativeUrl()
		{
			string url = "test";
			Assert.AreEqual(url.GetAbsoluteUrl(string.Empty), "test");
		}

		[Test]
		public void GetAbsoluteUrl_RelativeUrl_ReturnsAbsoluteUrl()
		{
			string url = "/test/test.jpg";
			Assert.AreEqual(url.GetAbsoluteUrl("http://www.test.com/abtesting"), "http://www.test.com//test/test.jpg");
		}

		[Test]
		public void IsValidUrl_EmptyString_ReturnsFalse()
		{
			string url = string.Empty;
			Assert.IsFalse(url.IsValidUrl());
		}

		[Test]
		public void IsValidUrl_IncorrectUrlFormat_ReturnsFalse()
		{
			string url = "/test";
			Assert.IsFalse(url.IsValidUrl());
		}

		[Test]
		public void IsValidUrl_IsCorrect_ReturnsTrue()
		{
			string url = "http://www.google.com";
			Assert.IsTrue(url.IsValidUrl());
		}

	}
}
