using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using CTO_Portal.CustomValidation;


namespace CTO_Portal.Models
{
	[MetadataType(typeof(groupMetaDate))]
	public partial class group
	{
        // add new methods or properties
        public int flag { get; set; }
        public int oldHospitalId { get; set; }
        public int oldDepartmentId { get; set; }
        public int oldDayId { get; set; }
        public int oldShiftId { get; set; }
        public int oldTrainerId { get; set; }


    }
	public class groupMetaDate
	{
        // edit properties


        [Required(ErrorMessage = "Course required")]
        public int courseId { get; set; }


        [Required(ErrorMessage = "Hospital required")]
        public int hospitalId { get; set; }


        [Required(ErrorMessage = "Department required")]
        [IsLocationAvaliable("oldHospitalId", "oldDepartmentId", "oldDayId", "oldShiftId", 
            "hospitalId", "departmentId", "dayId", "shiftId","flag")]
        public int departmentId { get; set; }


        [Required(ErrorMessage = "Day required")]
        public int dayId { get; set; }


        [Required(ErrorMessage = "Shift required")]
        public int shiftId { get; set; }


        [Required(ErrorMessage = "Trainer required")]
        [IsTrainerAvaliable("oldDayId", "oldShiftId", "oldTrainerId", "dayId", "shiftId", "flag")]
        public int trainerId { get; set; }



        [Required(ErrorMessage = "Student ID required")]
        [IsFound(ErrorMessage = " This student Id does not exist")]
        [IsStudentAlreadyInCourseGroup("courseId",
            "oldHospitalId", "oldDepartmentId", "oldDayId", "oldShiftId",
            "hospitalId", "departmentId", "dayId", "shiftId",
            "flag")]
        [IsDuplicated("studentIdTwo", "studentIdThree", "studentIdFour", "studentIdFive", "studentIdSix")]
        [IsStudentAvaliable("hospitalId", "departmentId", "dayId", "shiftId",
            "oldHospitalId", "oldDepartmentId", "oldDayId","oldShiftId", "flag")]
        [Range(0, 999999999, ErrorMessage = "Student Id should be at most 9 positive digits")]
        public long studentIdOne { get; set; }



        [Required(ErrorMessage = "Student ID required")]
        [IsFound(ErrorMessage = " This student Id does not exist")]
        [IsStudentAlreadyInCourseGroup("courseId",
            "oldHospitalId", "oldDepartmentId", "oldDayId", "oldShiftId",
            "hospitalId", "departmentId", "dayId", "shiftId",
            "flag")]
        [IsDuplicated("studentIdOne", "studentIdThree", "studentIdFour", "studentIdFive", "studentIdSix")]
        [IsStudentAvaliable("hospitalId", "departmentId", "dayId", "shiftId",
            "oldHospitalId", "oldDepartmentId", "oldDayId", "oldShiftId", "flag")]
        [Range(0, 999999999, ErrorMessage = "Student Id should be at most 9 positive digits")]
        public long studentIdTwo { get; set; }



        [IsFound(ErrorMessage = " This student Id does not exist")]
        [IsStudentAlreadyInCourseGroup("courseId",
            "oldHospitalId", "oldDepartmentId", "oldDayId", "oldShiftId",
            "hospitalId", "departmentId", "dayId", "shiftId",
            "flag")]
        [IsDuplicated("studentIdOne", "studentIdTwo", "studentIdFour", "studentIdFive", "studentIdSix")]
        [IsStudentAvaliable("hospitalId", "departmentId", "dayId", "shiftId",
            "oldHospitalId", "oldDepartmentId", "oldDayId", "oldShiftId", "flag")]
        [Range(0, 999999999, ErrorMessage = "Student Id should be at most 9 positive digits")]
        public Nullable<long> studentIdThree { get; set; }



        [IsFound(ErrorMessage = " This student Id does not exist")]
        [IsStudentAlreadyInCourseGroup("courseId",
            "oldHospitalId", "oldDepartmentId", "oldDayId", "oldShiftId",
            "hospitalId", "departmentId", "dayId", "shiftId",
            "flag")]
        [IsDuplicated("studentIdOne", "studentIdTwo", "studentIdThree", "studentIdFive", "studentIdSix")]
        [IsStudentAvaliable("hospitalId", "departmentId", "dayId", "shiftId",
            "oldHospitalId", "oldDepartmentId", "oldDayId", "oldShiftId", "flag")]
        [Range(0, 999999999, ErrorMessage = "Student Id should be at most 9 positive digits")]
        public Nullable<long> studentIdFour { get; set; }



        [IsFound(ErrorMessage = " This student Id does not exist")]
        [IsStudentAlreadyInCourseGroup("courseId",
            "oldHospitalId", "oldDepartmentId", "oldDayId", "oldShiftId",
            "hospitalId", "departmentId", "dayId", "shiftId",
            "flag")]
        [IsDuplicated("studentIdOne", "studentIdTwo", "studentIdThree", "studentIdFour", "studentIdSix")]
        [IsStudentAvaliable("hospitalId", "departmentId", "dayId", "shiftId",
            "oldHospitalId", "oldDepartmentId", "oldDayId", "oldShiftId", "flag")]
        [Range(0, 999999999, ErrorMessage = "Student Id should be at most 9 positive digits")]
        public Nullable<long> studentIdFive { get; set; }



        [IsFound(ErrorMessage = " This student Id does not exist")]
        [IsStudentAlreadyInCourseGroup("courseId",
            "oldHospitalId", "oldDepartmentId", "oldDayId", "oldShiftId",
            "hospitalId", "departmentId", "dayId", "shiftId",
            "flag")]
        [IsDuplicated("studentIdOne", "studentIdTwo", "studentIdThree", "studentIdFour", "studentIdFive")]
        [IsStudentAvaliable("hospitalId", "departmentId", "dayId", "shiftId",
            "oldHospitalId", "oldDepartmentId", "oldDayId", "oldShiftId", "flag")]
        [Range(0, 999999999, ErrorMessage = "Student Id should be at most 9 positive digits")]
        public Nullable<long> studentIdSix { get; set; }
    }
}