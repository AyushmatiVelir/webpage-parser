using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using webpage_parser.Extensions;

namespace webpage_parser.Tests.Extensions
{
	[TestFixture]
	public class TextExtensionsTest
	{
		[Test]
		public void GetWordsfromText_EmptyString_DoesNotThrow()
		{
			string text = string.Empty;
			Assert.DoesNotThrow(() => text.GetWordsfromText());
		}

		[Test]
		public void GetWordsfromText_EmptyString_ReturnsEmptyEnumerable()
		{
			string text = string.Empty;
			Assert.AreEqual(text.GetWordsfromText(),Enumerable.Empty<string>());
		}

		[Test]
		public void GetWordsfromText_TextHasPunctuations_RemovePunctuations()
		{
			string text = "hello, we want to check. Will this work.";
			Assert.AreEqual(text.GetWordsfromText().ToList(), new List<string> { "hello","we","want","to","check","Will","this","work" });
		}

		[Test]
		public void GetWordsfromText_TextHasSymbols_RemoveSymbols()
		{
			string text = "This is @ test & this is = test";
			Assert.AreEqual(text.GetWordsfromText(), new List<string> {"This","is","test","this","is","test"});
		}

		[Test]
		public void GetWordsfromText_TextHasOneWord_ReturnsOneWord()
		{
			string text = "test";
			Assert.AreEqual(text.GetWordsfromText(), new List<string> {"test"});
		}

	}
}
