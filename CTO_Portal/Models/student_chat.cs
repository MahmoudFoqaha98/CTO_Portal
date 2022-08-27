using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CTO_Portal.Models
{
	public class student_chat
	{
		public student_chat(student s, chat message)
		{
			this.Student = s;
			this.Message = message;
		}
		public student Student { get;set; }
		public chat Message { get; set; }
		public override string ToString()
		{
			return this.Student.Id + " " + this.Student.name + " " + Message;
		}
	}
}