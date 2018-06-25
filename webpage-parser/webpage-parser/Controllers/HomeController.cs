using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webpage_parser.Models;

namespace webpage_parser.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		
		[HttpPost]
		public ActionResult Parse(ParserModel model)
		{
			if (ModelState.IsValid)
			{
				//TODO: SubscribeUser(model.Email);
			}

			//return View("Index", model);
			return View("Contact");
		}
	}
}