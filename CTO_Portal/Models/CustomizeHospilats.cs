using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CTO_Portal.Models
{
	[MetadataType(typeof(hospitalMetaData))]
	public partial class hospital
	{
		//add new mothods or properties
	}

	public class hospitalMetaData
	{
		// Edit properties
		[Display(Name ="Hospital ID")]
		[Required()]
		public int Id { get; set; }



		[Display(Name = "Hospital Name")]
		[Required(ErrorMessage = "Hospital Name is Required")]
		[RegularExpression("^[ A-Za-zأ-ي]+$", ErrorMessage = "Hospital Name should be characters only")]
		[StringLength(100, ErrorMessage = "Hospital Name should be at most 100 characters")]
		public string name { get; set; }
	}


}