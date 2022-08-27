using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using CTO_Portal.Models;

namespace CTO_Portal.CustomValidation
{
	public class IsTrainerAvaliableAttribute : ValidationAttribute
	{
		private readonly string dayId;
		private readonly string shiftId;

		private readonly string oldDayId;
		private readonly string oldShiftId;
		private readonly string oldTrainerId;

		private readonly string flag;

		public IsTrainerAvaliableAttribute(string oldDayId, string oldShiftId, string oldTrainerId,
			string dayId, string shiftId, string flag)
		{
			this.oldDayId = oldDayId;
			this.oldShiftId = oldShiftId;
			this.oldTrainerId = oldTrainerId;

			this.dayId = dayId;
			this.shiftId = shiftId;


			this.flag = flag;
		}


		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{

			if (value != null)
			{

				var type = validationContext.ObjectInstance.GetType();


				var oldDayIdProperty = type.GetProperty(oldDayId);
				var oldShiftIdProperty = type.GetProperty(oldShiftId);
				var oldTrainerIdProperty = type.GetProperty(oldTrainerId);


				var dayIdProperty = type.GetProperty(dayId);
				var shiftIdProperty = type.GetProperty(shiftId);

				var flagProperty = type.GetProperty(flag);



				if (oldDayIdProperty != null&& oldShiftIdProperty != null && oldTrainerIdProperty  != null 
					&& shiftIdProperty != null && dayIdProperty != null && flagProperty != null)
				{

					var oldDayIdPropertyValue = oldDayIdProperty.GetValue(validationContext.ObjectInstance, null);
					var oldShiftIdPropertyValue = oldShiftIdProperty.GetValue(validationContext.ObjectInstance, null);
					var oldTrainerIdPropertyValue = oldTrainerIdProperty.GetValue(validationContext.ObjectInstance, null);


					var dayIdPropertyValue = dayIdProperty.GetValue(validationContext.ObjectInstance, null);
					var shiftIdPropertyValue = shiftIdProperty.GetValue(validationContext.ObjectInstance, null);


					var flagPropertyValue = flagProperty.GetValue(validationContext.ObjectInstance, null);


					Int32 oldDId = Int32.Parse(oldDayIdPropertyValue.ToString());
					Int32 oldSid = Int32.Parse(oldShiftIdPropertyValue.ToString());
					Int32 oldTrId = Int32.Parse(oldTrainerIdPropertyValue.ToString());


					Int32 dId = Int32.Parse(dayIdPropertyValue.ToString());
					Int32 sid = Int32.Parse(shiftIdPropertyValue.ToString());


					Int32 flag_value = Int32.Parse(flagPropertyValue.ToString());


					CTOEntities db = new CTOEntities();

					group myGroup = null;

					Int32 trainerID = Int32.Parse(value.ToString());
					if (flag_value == 1)
					{
						myGroup = db.groups.Where(a => a.dayId == dId)
										   .Where(a => a.shiftId == sid)
										   .Where(a=>a.trainerId==trainerID).FirstOrDefault();

						if (myGroup == null)
							return ValidationResult.Success;

						return new ValidationResult("This Trainer is busy at this time", new[] { validationContext.MemberName });
					}

					else
					{
						IEnumerable<group> allGroups = db.groups.Where(a=>a.dayId != oldDId 
																		|| a.shiftId != oldSid
																		||a.trainerId != oldTrId).ToList();


						myGroup = allGroups.Where(a => a.dayId == dId)
										   .Where(a => a.shiftId == sid)
										   .Where(a=>a.trainerId == trainerID).FirstOrDefault();

						if (myGroup == null)
							return ValidationResult.Success;

						return new ValidationResult("This Trainer is busy at this time", new[] { validationContext.MemberName });
					}
				}
			}
			return ValidationResult.Success;
		}
	}
}