using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webpage_parser.Models;

namespace webpage_parser.Services
{
	public interface IParserService
	{
		ParseResultModel GetParsedResults(string url);
	}


}