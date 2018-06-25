using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webpage_parser.Models;
using webpage_parser.Services;

namespace webpage_parser.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Result()
		{
			ViewBag.Message = "Your parsed results.";

			return View();
		}


		[HttpPost]
		public ActionResult Parse(ParserModel model)
		{
			if (!ModelState.IsValid) return View("Result", null);

			IParserService parser = new ParserService();
			if (parser.IsValidUrl(model.Url))
			{
				var resultModel = new ParseResultModel
				{
					ImageUrls = parser.GetPictures(model.Url),
					TopWordCounts = parser.GetTopNWordCount(model.Url, 8)
				};
				return View("Result", resultModel);
			}
			return View("Result", null);
		}
	}
}