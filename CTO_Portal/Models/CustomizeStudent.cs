using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CTO_Portal.Models
{
	[MetadataType(typeof(studentMetaData))]
	public partial class student
	{
		//add methods or add new properties
	}
	// specifies the metadata class to associate with a data model class
	public class studentMetaData
	{
		// Edit properties
		[Display(Name ="Student Id")]
		[Required]
		public long Id { get; set; }


		[Display(Name = "Student Name")]
		[Required]
		public string name { get; set; }


		[Display(Name = "Student Email")]
		[Required]
		[DataType(DataType.EmailAddress)]
		public string email { get; set; }

		[Display(Name = "Student Password")]
		[Required]
		public string password { get; set; }
	}
}