using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
namespace CTO_Portal.Models
{
	[MetadataType(typeof(courseMetaData))]
	public partial class cours
	{
		//add methods or add new properties
	}
	// specifies the metadata class to associate with a data model class
	public class courseMetaData
	{
		// Edit properties
		[Display(Name = "Course Id")]
		[Required]
		public int Id { get; set; }


		[Display(Name="Course Name")]
		[Required]
		[RegularExpression("^[ A-Za-zأ-ي]+$", ErrorMessage ="Course Name should be characters only")]
		[StringLength(100,ErrorMessage = "Course Name should be at most 100 characters")]
		public string name { get; set; }
	}
}