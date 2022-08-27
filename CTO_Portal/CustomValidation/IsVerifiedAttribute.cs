using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CTO_Portal.Models;

namespace CTO_Portal.CustomValidation
{
	public class IsVerifiedAttribute:ValidationAttribute
	{
		private readonly string courseId;
		public IsVerifiedAttribute( string courseId)
		{
			this.courseId = courseId;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)//here the value is a student id 
		{

			if (value != null)
			{
				Int64 studentId = Int64.Parse( value.ToString() );
				var type = validationContext.ObjectInstance.GetType();
				var coursrIdProperty = type.GetProperty(courseId);
				if (coursrIdProperty != null)
				{
					var coursrIdPropertyValue = coursrIdProperty.GetValue(validationContext.ObjectInstance, null);

					Int32 cId = Int32.Parse(coursrIdPropertyValue.ToString());
					CTOEntities db = new CTOEntities();

					verified_requests request = db.verified_requests.Where(a => a.courseId == cId)
						.Where(a => a.studentIdOne == studentId ||
									 a.studentIdTwo == studentId ||
									 a.studentIdThree == studentId ||
									 a.studentIdFour == studentId ||
									 a.studentIdFive == studentId ||
									 a.studentIdSix == studentId).FirstOrDefault();

					if (request == null)
						return ValidationResult.Success;

					return new ValidationResult("This student has already agreed on another group request for this course,please contact the office for more information",new[] { validationContext.MemberName});
				}
			}
			return ValidationResult.Success;
		}
	}
}