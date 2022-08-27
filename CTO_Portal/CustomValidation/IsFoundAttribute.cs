using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using CTO_Portal.Models;
namespace CTO_Portal.CustomValidation
{
	
	public class IsFoundAttribute:ValidationAttribute
	{
		public IsFoundAttribute()
		{

		}

		public override bool IsValid(object value)//here the value is a student id 
		{
			if (value != null)
			{
				CTOEntities db = new CTOEntities();
				student s = db.students.Find( Int64.Parse(value.ToString()) );
				if (s == null)
					return false;
			}
			return true;

		}
	}
}