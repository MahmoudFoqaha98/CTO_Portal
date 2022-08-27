using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using CTO_Portal.Models;
using System.Collections;

namespace CTO_Portal.CustomValidation
{
	public class IsStudentAlreadyInCourseGroupAttribute :ValidationAttribute
	{
		private readonly string courseId;

		private readonly string hospitalId;
		private readonly string departmentId;
		private readonly string dayId;
		private readonly string shiftId;

		private readonly string oldHospitalId;
		private readonly string oldDepartmentId;
		private readonly string oldDayId;
		private readonly string oldShiftId;


		private readonly string flag;


		public IsStudentAlreadyInCourseGroupAttribute(
			string courseId,
			string oldHospitalId, string oldDepartmentId, string oldDayId, string oldShiftId,
			string hospitalId,string departmentId, string dayId, string shiftId,
			string flag)
		{
			this.courseId = courseId;

			this.oldHospitalId = oldHospitalId;
			this.oldDepartmentId = oldDepartmentId;
			this.oldDayId = oldDayId;
			this.oldShiftId = oldShiftId;


			this.hospitalId = hospitalId;
			this.departmentId = departmentId;
			this.dayId = dayId;
			this.shiftId = shiftId;


			this.flag = flag;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)//here the value is a student id 
		{

			if (value != null)
			{
				Int64 studentId = Int64.Parse(value.ToString());

				var type = validationContext.ObjectInstance.GetType();

				var coursrIdProperty = type.GetProperty(courseId);

				var oldHospitalIdProperty = type.GetProperty(oldHospitalId);
				var oldDepartmentIdProperty = type.GetProperty(oldDepartmentId);
				var oldDayIdProperty = type.GetProperty(oldDayId);
				var oldShiftIdProperty = type.GetProperty(oldShiftId);



				var hospitalIdProperty = type.GetProperty(hospitalId);
				var departmentIdProperty = type.GetProperty(departmentId);
				var dayIdProperty = type.GetProperty(dayId);
				var shiftIdProperty = type.GetProperty(shiftId);


				var flagProperty = type.GetProperty(flag);



				if (coursrIdProperty != null 
					&& oldHospitalIdProperty != null
					&& oldDepartmentIdProperty != null
					&& oldDayIdProperty != null
					&& oldShiftIdProperty != null

					&& hospitalIdProperty != null 
					&& departmentIdProperty != null
					&& dayIdProperty != null 
					&& shiftIdProperty != null 

					&& flagProperty != null)
				{
					var coursrIdPropertyValue = coursrIdProperty.GetValue(validationContext.ObjectInstance, null);


					var oldHospitalIdPropertyValue = oldHospitalIdProperty.GetValue(validationContext.ObjectInstance, null);
					var oldDepartmentIdPropertyValue = oldDepartmentIdProperty.GetValue(validationContext.ObjectInstance, null);
					var oldDayIdPropertyValue = oldDayIdProperty.GetValue(validationContext.ObjectInstance, null);
					var oldShiftIdPropertyValue = oldShiftIdProperty.GetValue(validationContext.ObjectInstance, null);



					var hospitalIdPropertyValue = hospitalIdProperty.GetValue(validationContext.ObjectInstance, null);
					var departmentIdPropertyValue = departmentIdProperty.GetValue(validationContext.ObjectInstance, null);
					var dayIdPropertyValue = dayIdProperty.GetValue(validationContext.ObjectInstance, null);
					var shiftIdPropertyValue = shiftIdProperty.GetValue(validationContext.ObjectInstance, null);


					var flagPropertyValue = flagProperty.GetValue(validationContext.ObjectInstance, null);

					Int32 cId = Int32.Parse(coursrIdPropertyValue.ToString());


					Int32 oldHosId = Int32.Parse(oldHospitalIdPropertyValue.ToString());
					Int32 oldDeptId = Int32.Parse(oldDepartmentIdPropertyValue.ToString());
					Int32 oldDId = Int32.Parse(oldDayIdPropertyValue.ToString());
					Int32 oldShId = Int32.Parse(oldShiftIdPropertyValue.ToString());


					Int32 hosId = Int32.Parse(hospitalIdPropertyValue.ToString());
					Int32 deptId = Int32.Parse(departmentIdPropertyValue.ToString());
					Int32 dId = Int32.Parse(dayIdPropertyValue.ToString());
					Int32 shId = Int32.Parse(shiftIdPropertyValue.ToString());


					Int32 flag_value = Int32.Parse(flagPropertyValue.ToString());

					CTOEntities db = new CTOEntities();

					group myGroup = null;

					if (flag_value == 1)
					{
						myGroup = db.groups.Where(a => a.courseId == cId)
							.Where(a => a.studentIdOne == studentId ||
										 a.studentIdTwo == studentId ||
										 a.studentIdThree == studentId ||
										 a.studentIdFour == studentId ||
										 a.studentIdFive == studentId ||
										 a.studentIdSix == studentId).FirstOrDefault();

						if (myGroup == null)
							return ValidationResult.Success;

						return new ValidationResult("This student is already in a group for this course", new[] { validationContext.MemberName });
					}
					else
					{
						IEnumerable<group> allGroups = db.groups.Where(a => a.hospitalId != oldHosId ||
								   a.departmentId != oldDeptId || a.dayId != oldDId || a.shiftId != oldShId).ToList();





						myGroup = allGroups.Where(a => a.courseId == cId)
												.Where(a => a.studentIdOne == studentId ||
															 a.studentIdTwo == studentId ||
															 a.studentIdThree == studentId ||
															 a.studentIdFour == studentId ||
															 a.studentIdFive == studentId ||
															 a.studentIdSix == studentId).FirstOrDefault();

						if (myGroup == null)
							return ValidationResult.Success;

						return new ValidationResult("This student is already in a group for this course", new[] { validationContext.MemberName });
					}
				}
			}
			return ValidationResult.Success;
		}
	}
}