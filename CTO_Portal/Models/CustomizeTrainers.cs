using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace CTO_Portal.Models
{
	[MetadataType(typeof(trainerMetaData) )]
	public partial class trainer
	{
		//add new methods or properties
	}

	public class trainerMetaData
	{
		[Display(Name ="Trainer ID")]
		[Required(ErrorMessage = "Trainer Id Required")]
		public int Id { get; set; }



		[Display(Name = "Trainer Name")]
		[Required(ErrorMessage = "Trainer Name Required")]
		[RegularExpression("^[ A-Za-zأ-ي]+$", ErrorMessage = "Trainer Name should be characters only")]
		[StringLength(100, ErrorMessage = "Trainer Name should be at most 100 characters")]
		public string name { get; set; }
	}
}