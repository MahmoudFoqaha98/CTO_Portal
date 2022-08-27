using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using CTO_Portal.Models;

namespace CTO_Portal.CustomValidation
{
	public class IsDuplicatedAttribute : ValidationAttribute
	{
		private readonly string studentIdOne;
		private readonly string studentIdTwo;
		private readonly string studentIdThree;
		private readonly string studentIdFour;
		private readonly string studentIdFive;
		public IsDuplicatedAttribute(string studentIdOne, string studentIdTwo, string studentIdThree, string studentIdFour, string studentIdFive)
		{
			this.studentIdOne = studentIdOne;
			this.studentIdTwo = studentIdTwo;
			this.studentIdThree = studentIdThree;
			this.studentIdFour = studentIdFour;
			this.studentIdFive = studentIdFive;
		}


		protected override ValidationResult IsValid(object value, ValidationContext validationContext)//here the value is a student id 
		{

			if (value != null)
			{
				Int64 studentId = Int64.Parse(value.ToString());
				var type = validationContext.ObjectInstance.GetType();
				var studentIdOneProperty = type.GetProperty(studentIdOne);
				var studentIdTwoProperty = type.GetProperty(studentIdTwo);
				var studentIdThreeProperty = type.GetProperty(studentIdThree);
				var studentIdFourProperty = type.GetProperty(studentIdFour);
				var studentIdFiveProperty = type.GetProperty(studentIdFive);

				if (studentIdOneProperty != null && studentIdTwoProperty != null && studentIdThreeProperty != null
					|| studentIdFourProperty != null || studentIdFiveProperty != null)
				{
					var studentIdOnePropertyValue = studentIdOneProperty.GetValue(validationContext.ObjectInstance, null);
					var studentIdTwoPropertyValue = studentIdTwoProperty.GetValue(validationContext.ObjectInstance, null);
					var studentIdThreePropertyValue = studentIdThreeProperty.GetValue(validationContext.ObjectInstance, null);
					var studentIdFourPropertyValue = studentIdFourProperty.GetValue(validationContext.ObjectInstance, null);
					var studentIdFivePropertyValue = studentIdFiveProperty.GetValue(validationContext.ObjectInstance, null);


					bool res1 = check(value.ToString(), studentIdOnePropertyValue);
					bool res2 = check(value.ToString(), studentIdTwoPropertyValue);
					bool res3 = check(value.ToString(), studentIdThreePropertyValue);
					bool res4 = check(value.ToString(), studentIdFourPropertyValue);
					bool res5 = check(value.ToString(), studentIdFivePropertyValue);

					if (res1)
						return new ValidationResult("You can only use this student ID once", new[] { validationContext.MemberName });

					if (res2)
						return new ValidationResult("You can only use this student ID once", new[] { validationContext.MemberName });

					if (res3)
						return new ValidationResult("You can only use this student ID once", new[] { validationContext.MemberName });

					if (res4)
						return new ValidationResult("You can only use this student ID once", new[] { validationContext.MemberName });

					if (res5)
						return new ValidationResult("You can only use this student ID once", new[] { validationContext.MemberName });


					return ValidationResult.Success;
				}
			}
			return ValidationResult.Success;
		}

		private bool check(string id, object id2)
		{
			if (id2 == null)
				return false;

			if (id.Equals(id2.ToString()))
				return true;

			return false;
		}
	}

}