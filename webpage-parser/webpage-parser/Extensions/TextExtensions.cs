using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webpage_parser.Extensions
{
	public static class TextExtensions
	{
		public static IEnumerable<string> GetWordsfromText(this string text)
		{
			var punctuation = text.Where(char.IsPunctuation).Distinct().ToArray();
			var words = text.Split().Select(x => x.Trim(punctuation)).Where(x => !string.IsNullOrWhiteSpace(x)).Where(x => !(x.Length == 1 && char.IsSymbol(x[0])));
			return words;
		}
	}
}