using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using CTO_Portal.CustomValidation;

namespace CTO_Portal.Models
{
	[MetadataType(typeof(Temp_requestMetaData))]
	public partial class temp_requests
	{
		// add methods or properties

	}
	public class Temp_requestMetaData
	{
		
		// Edit properites
		[Required]
		[Display(Name ="Student No 1")]
		[IsFound(ErrorMessage = " This student Id does not exist")]
		[IsVerified("courseId")]
		[IsDuplicated("studentIdTwo", "studentIdThree", "studentIdFour", "studendIdFive", "studentIdSix")]
		[Range(0, 999999999, ErrorMessage = "Student Id should be at most 9 positive digits")]
		public long stidentIdOne { get; set; }


		[Required]
		[Display(Name = "Student No 2")]
		[IsFound(ErrorMessage = " This student Id does not exist")]
		[IsVerified("courseId")]
		[IsDuplicated("stidentIdOne", "studentIdThree", "studentIdFour", "studendIdFive", "studentIdSix")]
		[Range(0,999999999,ErrorMessage = "Student Id should be at most 9 positive digits")]
		public long studentIdTwo { get; set; }


	
		[Display(Name = "Student No 3")]
		[IsFound(ErrorMessage = " This student Id does not exist")]
		[IsVerified("courseId")]
		[IsDuplicated("stidentIdOne", "studentIdTwo", "studentIdFour", "studendIdFive", "studentIdSix")]
		[Range(0, 999999999, ErrorMessage = "Student Id should be at most 9 positive digits")]
		public Nullable<long> studentIdThree { get; set; }


		
		[Display(Name = "Student No 4")]
		[IsFound(ErrorMessage = " This student Id does not exist")]
		[IsVerified("courseId")]
		[IsDuplicated("stidentIdOne", "studentIdTwo", "studentIdThree", "studendIdFive", "studentIdSix")]
		[Range(0, 999999999, ErrorMessage = "Student Id should be at most 9 positive digits")]
		public Nullable<long> studentIdFour { get; set; }


		
		[Display(Name = "Student No 5")]
		[IsFound(ErrorMessage = " This student Id does not exist")]
		[IsVerified("courseId")]
		[IsDuplicated("stidentIdOne", "studentIdTwo", "studentIdThree", "studentIdFour", "studentIdSix")]
		[Range(0, 999999999, ErrorMessage = "Student Id should be at most 9 positive digits")]
		public Nullable<long> studendIdFive { get; set; }


		
		[Display(Name = "Student No 6")]
		[IsFound(ErrorMessage = " This student Id does not exist")]
		[IsVerified("courseId")]
		[IsDuplicated("stidentIdOne", "studentIdTwo", "studentIdThree", "studentIdFour", "studendIdFive")]
		[Range(0, 999999999, ErrorMessage = "Student Id should be at most 9 positive digits")]
		
		public Nullable<long> studentIdSix { get; set; }


		[Required]
		[Display(Name = "Course")]
		public int courseId { get; set; }


		
		[Display(Name = "Note")]
		[MaxLength(300,ErrorMessage ="You can't enter more than 300 characters")]
		public string note { get; set; }
	}

}