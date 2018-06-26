using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webpage_parser.Models
{
	public class ParserModel
	{
		[Required(ErrorMessage = "Required Field")]
		[Url(ErrorMessage = "Please enter a valid url beginning with http or https.")]
		public string Url { get; set; }
	}
}