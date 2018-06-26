using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webpage_parser.Extensions
{
	public static class UrlExtensions
	{
		public static string GetAbsoluteUrl(this string imgSrc, string websiteUrl)
		{
			return IsValidUrl(websiteUrl) ? $"{GetUrlHost(websiteUrl)}/{imgSrc}" : imgSrc;
		}
		public static bool IsValidUrl(this string url)
		{
			Uri uriResult;
			return Uri.TryCreate(url, UriKind.Absolute, out uriResult)
				   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
		}
		private static string GetUrlHost(string websiteUrl)
		{
			var siteUri = new Uri(websiteUrl);
			return siteUri.GetLeftPart(UriPartial.Authority);
		}
	}
}