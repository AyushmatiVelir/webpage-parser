using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webpage_parser.Models
{
	public class ParserModel
	{
		[Required]
		public string Url { get; set; }
	}
}