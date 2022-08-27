using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CTO_Portal.Models
{
	[MetadataType(typeof(departmentMetaDate))]
	public partial class department
	{
		// add new properties
	}

	public class departmentMetaDate
	{
		// edit methods or properties

		[Display(Name ="Department Id")]
		[Required]
		public int Id { get; set; }


		[Display(Name = "Department Name")]
		[Required]
		[RegularExpression("^[ A-Za-zأ-ي]+$", ErrorMessage = "Department Name should be characters only")]
		[StringLength(100, ErrorMessage = "Department Name should be at most 100 characters")]
		public string name { get; set; }
	}
}