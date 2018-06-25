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
		private IParserService _service;

		public HomeController(IParserService service)
		{
			_service = service;
		}
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
			
			if (_service.IsValidUrl(model.Url))
			{
				var resultModel = new ParseResultModel
				{
					ImageUrls = _service.GetPictures(model.Url),
					TopWordCounts = _service.GetTopNWordCount(model.Url, 8)
				};
				return View("Result", resultModel);
			}
			return View("Result", null);
		}
	}
}