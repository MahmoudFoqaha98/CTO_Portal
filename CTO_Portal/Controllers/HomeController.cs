using CTO_Portal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Data.Linq.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Remoting.Contexts;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace CTO_Portal.Controllers
{
	public class HomeController : Controller
	{
		private CTOEntities db = new CTOEntities();

		// Main Page ( Start Page)
		[HttpGet]
		public ActionResult MainPage()
		{
			return View();
		}



		// GET: Login Page 
		[HttpGet]
		public ActionResult Login()
		{
			return View();
		}


		// POST: Login Page 
		[HttpPost]
		public ActionResult Login( string username , string password)
		{
			if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
			{
				Int64 user;
				
				bool res = Int64.TryParse(username,out user);
				if (res == false)
				{
					ViewBag.Error = "Invalid Username/Password";
					return View();
				}

				student s = db.students.Where(a=>a.Id == user && a.password == password).FirstOrDefault();

				if (s != null)
				{
					// save student information if username and password are true
					Session["id"] = s.Id;
					Session["name"] = s.name;
					Session["type"] = "1"; // flag to difference between users 
					return RedirectToAction("Index");
				}
				else
				{
					admin ad = db.admins.Where(a=>a.id == user && a.password == password).FirstOrDefault();
					if (ad != null)
					{
						// save admin information if username and password are true
						Session["id"] = ad.id;
						Session["name"] = ad.name;
						Session["type"] = "2";
						return RedirectToAction("AdminDashboard");
					}
				}
				ViewBag.Error = "Invalid Username/Password";
			}
			else
				ViewBag.Error = "Username/Password Required";
			return View();
		}


		// GET: Acess Denied Page 
		[HttpGet]
		public ActionResult acessdenied()
		{
			return View();
		}


		// GET: student dashboard that shown when student loged in
		[HttpGet]
		public ActionResult Index()
		{
			if(Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("1"))// It checks if the person who requested the page is a student or not
				return RedirectToAction("acessdenied");

			return View();
		}



		// GET: temp_requests/Create Request
		public ActionResult CreateRequest()
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("1"))// It checks if the person who requested the page is a student or not
				return RedirectToAction("acessdenied");

			ViewBag.RequestPeriod = db.request_period.Find(1).status;
			ViewBag.courseId = new SelectList(db.courses, "Id", "name");
			ViewBag.stidentIdOne = new SelectList(db.students, "Id", "name");
			ViewBag.studentIdTwo = new SelectList(db.students, "Id", "name");
			ViewBag.studentIdThree = new SelectList(db.students, "Id", "name");
			ViewBag.studentIdFour = new SelectList(db.students, "Id", "name");
			ViewBag.studendIdFive = new SelectList(db.students, "Id", "name");
			ViewBag.studentIdSix = new SelectList(db.students, "Id", "name");
			return View();
		}






		// POST: temp_requests/Create Request
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateRequest([Bind(Include = "id,stidentIdOne,studentIdTwo,studentIdThree,studentIdFour,studendIdFive,studentIdSix,courseId,note")] temp_requests temp_requests)
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("1"))// It checks if the person who requested the page is a student or not
				return RedirectToAction("acessdenied");

			if (ModelState.IsValid)
			{
				try
				{

					if (!(temp_requests.stidentIdOne.ToString()).Equals(Session["id"].ToString()))
					{
						ModelState.AddModelError("stidentIdOne", "This is not your ID, don't play with code");
						ViewBag.courseId = new SelectList(db.courses, "Id", "name", temp_requests.courseId);
						return View(temp_requests);
					}

					db.temp_requests.Add(temp_requests);
					db.SaveChanges();


					// insert request id , student id , status, activationCode to email status
					email_status email = new email_status();

					email.requestId = temp_requests.id;
					email.studentId = temp_requests.stidentIdOne;
					email.status = false;
					email.activationCode = Guid.NewGuid();
					var act = email.activationCode;
					db.email_status.Add(email);
					// End----------------------------

					// insert request id , student id , status, activationCode to email status
					email_status email2 = new email_status();

					email2.requestId = temp_requests.id;
					email2.studentId = temp_requests.studentIdTwo;
					email2.status = false;
					email2.activationCode = Guid.NewGuid();
					var act2 = email2.activationCode;
					db.email_status.Add(email2);
					// End----------------------------

					string Emailst3 = null, Emailst4 = null, Emailst5 = null, Emailst6 = null;
					Guid act3 = Guid.Empty, act4 = Guid.Empty, act5 = Guid.Empty, act6 = Guid.Empty;
					// insert request id , student id , status, activationCode to email status
					if (temp_requests.studentIdThree != null)
					{
						email_status email3 = new email_status();

						email3.requestId = temp_requests.id;
						email3.studentId = (long)temp_requests.studentIdThree;
						email3.status = false;
						email3.activationCode = Guid.NewGuid();
						act3 = email3.activationCode;
						db.email_status.Add(email3);
						Emailst3 = db.students.Find(temp_requests.studentIdThree).email;
					}
					// End----------------------------


					// insert request id , student id , status, activationCode to email status
					if (temp_requests.studentIdFour != null)
					{
						email_status email4 = new email_status();

						email4.requestId = temp_requests.id;
						email4.studentId = (long)temp_requests.studentIdFour;
						email4.status = false;
						email4.activationCode = Guid.NewGuid();
						act4 = email4.activationCode;
						db.email_status.Add(email4);
						Emailst4 = db.students.Find(temp_requests.studentIdFour).email;
					}
					// End----------------------------


					// insert request id , student id , status, activationCode to email status
					if (temp_requests.studendIdFive != null)
					{
						email_status email5 = new email_status();

						email5.requestId = temp_requests.id;
						email5.studentId = (long)temp_requests.studendIdFive;
						email5.status = false;
						email5.activationCode = Guid.NewGuid();
						act5 = email5.activationCode;
						db.email_status.Add(email5);
						Emailst5 = db.students.Find(temp_requests.studendIdFive).email;
					}
					// End----------------------------


					// insert request id , student id , status, activationCode to email status
					if (temp_requests.studentIdSix != null)
					{
						email_status email6 = new email_status();

						email6.requestId = temp_requests.id;
						email6.studentId = (long)temp_requests.studentIdSix;
						email6.status = false;
						email6.activationCode = Guid.NewGuid();
						act6 = email6.activationCode;
						db.email_status.Add(email6);
						Emailst6 = db.students.Find(temp_requests.studentIdSix).email;
					}
					// End----------------------------

					db.SaveChanges();

					var Emailst1 = db.students.Find(temp_requests.stidentIdOne).email;
					var Emailst2 = db.students.Find(temp_requests.studentIdTwo).email;
					SendVerificationEmail(Emailst1, act.ToString(), temp_requests);
					SendVerificationEmail(Emailst2, act2.ToString(), temp_requests);

					if (Emailst3 != null)
						SendVerificationEmail(Emailst3, act3.ToString(), temp_requests);

					if (Emailst4 != null)
						SendVerificationEmail(Emailst4, act4.ToString(), temp_requests);


					if (Emailst5 != null)
						SendVerificationEmail(Emailst5, act5.ToString(), temp_requests);

					if (Emailst6 != null)
						SendVerificationEmail(Emailst6, act6.ToString(), temp_requests);
				}
				catch (Exception ex)
				{

				}

				return RedirectToAction("Index");
			}// End Model state

			ViewBag.RequestPeriod = db.request_period.Find(1).status;
			ViewBag.courseId = new SelectList(db.courses, "Id", "name", temp_requests.courseId);
			ViewBag.stidentIdOne = new SelectList(db.students, "Id", "name", temp_requests.stidentIdOne);
			ViewBag.studentIdTwo = new SelectList(db.students, "Id", "name", temp_requests.studentIdTwo);
			ViewBag.studentIdThree = new SelectList(db.students, "Id", "name", temp_requests.studentIdThree);
			ViewBag.studentIdFour = new SelectList(db.students, "Id", "name", temp_requests.studentIdFour);
			ViewBag.studendIdFive = new SelectList(db.students, "Id", "name", temp_requests.studendIdFive);
			ViewBag.studentIdSix = new SelectList(db.students, "Id", "name", temp_requests.studentIdSix);


			return View(temp_requests);
		}






		[NonAction]
		public void SendVerificationEmail(string emailId, string activationCode, temp_requests temp_requests)
		{

			var verifyUrl = "/Home/VerifyAccount/" + activationCode;
			string link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
			var fromEmail = new MailAddress("ctoaaup@gmail.com", " CTO AAUP");
			var fromEmailPassword = "Fa2468359";
			var toEmail = new MailAddress(emailId);

			string subject = "CTO group request";

			string body = string.Empty;
			using (StreamReader reader = new StreamReader(Server.MapPath("~/Views/Home/emailTemplate.html")))
			{
				body = reader.ReadToEnd();
			}

			body = body.Replace("{link}", link);

			student s = db.students.Find(temp_requests.stidentIdOne);
			body = body.Replace("{st1}", s.name);
			body = body.Replace("{st1_id}", temp_requests.stidentIdOne.ToString());



			s = db.students.Find(temp_requests.studentIdTwo);
			body = body.Replace("{st2}", s.name);
			body = body.Replace("{st2_id}", temp_requests.studentIdTwo.ToString());

			if (temp_requests.studentIdThree != null)
			{
				s = db.students.Find(temp_requests.studentIdThree);
				body = body.Replace("{st3}", s.name);
				//body = body.Replace("{st3}", (s.name != null) ? s.name : "");
				body = body.Replace("{st3_id}", temp_requests.studentIdThree.ToString());
			}
			else
			{
				body = body.Replace("{st3}", "");
				body = body.Replace("{st3_id}", "");
			}

			if (temp_requests.studentIdFour != null)
			{
				s = db.students.Find(temp_requests.studentIdFour);
				body = body.Replace("{st4}", s.name);
				body = body.Replace("{st4_id}", temp_requests.studentIdFour.ToString());
			}
			else
			{
				body = body.Replace("{st4}", "");
				body = body.Replace("{st4_id}", "");
			}

			if (temp_requests.studendIdFive != null)
			{
				s = db.students.Find(temp_requests.studendIdFive);
				body = body.Replace("{st5}", s.name);
				body = body.Replace("{st5_id}", temp_requests.studendIdFive.ToString());
			}
			else
			{
				body = body.Replace("{st5}", "");
				body = body.Replace("{st5_id}", "");
			}

			if (temp_requests.studentIdSix != null)
			{
				s = db.students.Find(temp_requests.studentIdSix);
				body = body.Replace("{st6}", s.name);
				body = body.Replace("{st6_id}", temp_requests.studentIdSix.ToString());
			}
			else
			{
				body = body.Replace("{st6}", "");
				body = body.Replace("{st6_id}", "");
			}

			cours c = db.courses.Find(temp_requests.courseId);
			body = body.Replace("{course}", c.name);

			var smtp = new SmtpClient
			{
				Host = "smtp.gmail.com",
				Port = 587,
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)

			};

			using (var message = new MailMessage(fromEmail, toEmail)
			{
				Subject = subject,
				Body = body,
				IsBodyHtml = true
			})
				smtp.Send(message);
		}




		[HttpGet]
		public ActionResult VerifyAccount(string id)// here id is the activation code
		{
			try
			{
				bool Status = false;
				db.Configuration.ValidateOnSaveEnabled = false;
				var v = db.email_status.Where(a => a.activationCode == new Guid(id)).FirstOrDefault();
				if (v != null)
				{
					if (!isVerVerified(id))
					{
						v.status = true;
						db.SaveChanges();
						Status = true;
					}
				}
				ViewBag.Status = Status;


				email_status e = db.email_status.Where(a => a.activationCode == new Guid(id)).FirstOrDefault();
				var request_id = e.requestId;

				var emails = db.email_status.Where(a => a.requestId == request_id).ToList();

				foreach (email_status item in emails) // check if all students are agreed of request or not
					if (item.status == false)
						return View();

				temp_requests temp = db.temp_requests.Find(request_id);

				verified_requests verifiedTransfare = new verified_requests();

				verifiedTransfare.id = temp.id;
				verifiedTransfare.studentIdOne = temp.stidentIdOne;
				verifiedTransfare.studentIdTwo = temp.studentIdTwo;
				verifiedTransfare.studentIdThree = temp.studentIdThree;
				verifiedTransfare.studentIdFour = temp.studendIdFive;
				verifiedTransfare.studentIdFive = temp.studendIdFive;
				verifiedTransfare.studentIdSix = temp.studentIdSix;
				verifiedTransfare.courseId = temp.courseId;
				verifiedTransfare.note = temp.note;
				verifiedTransfare.approved = false;
				db.verified_requests.Add(verifiedTransfare);
				db.SaveChanges();
			}
			catch ( Exception ex)
			{

			}
			return View();
		}



		// check if all students for this course in this request with this student are already verified or not
		private bool isVerVerified(string id)// id is student id that will agree the request
		{
			email_status v = db.email_status.Where(a => a.activationCode == new Guid(id)).FirstOrDefault();

			int requestId = v.requestId;

			Int64 studentId = v.studentId;

			temp_requests temp = db.temp_requests.Find(requestId);
			Int32 courseId = temp.courseId;

			if (isFoundInVerifiedRequests(courseId, temp.stidentIdOne)
				 || isFoundInVerifiedRequests(courseId, temp.studentIdTwo)
				 || isFoundInVerifiedRequests(courseId, temp.studentIdThree)
				 || isFoundInVerifiedRequests(courseId, temp.studentIdFour)
				 || isFoundInVerifiedRequests(courseId, temp.studendIdFive)
				 || isFoundInVerifiedRequests(courseId, temp.studentIdSix))

				return true;

			return false;
		}

		// check if this student for this course is already verify or not
		private bool isFoundInVerifiedRequests(Int32 courseId, Int64? studentId)
		{
			verified_requests request = db.verified_requests.Where(a => a.courseId == courseId)
			.Where(a => a.studentIdOne == studentId ||
						 a.studentIdTwo == studentId ||
						 a.studentIdThree == studentId ||
						 a.studentIdFour == studentId ||
						 a.studentIdFive == studentId ||
						 a.studentIdSix == studentId).FirstOrDefault();
			if (request != null)
				return true;

			return false;
		}




		[HttpGet]
		public ActionResult ViewSentRequestStatus()
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("1"))// It checks if the person who requested the page is a student or not
				return RedirectToAction("acessdenied");


			Int64 studentId = Int64.Parse(Session["id"].ToString());

			var temp_requests = (db.temp_requests.Include(t => t.cours).Include(t => t.student).Include
				(t => t.student1).Include(t => t.student2).Include(t => t.student3).Include
				(t => t.student4).Include(t => t.student5))
				.Where(a=>a.stidentIdOne == studentId || a.studentIdTwo == studentId|| a.studentIdThree== studentId
				|| a.studentIdFour == studentId || a.studendIdFive == studentId || a.studentIdSix == studentId);
			
			return View(temp_requests.ToList().OrderBy(a=>a.cours.name));
		}









		// show the number of Unseen messages on student dashboard
		public ActionResult _ChatStudentCount()
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("1"))// It checks if the person who requested the page is a student or not
				return RedirectToAction("acessdenied");

			Int64 studentId = Int64.Parse(Session["id"].ToString());

			ViewBag.ChatStudentCount = db.chats.Where(a => a.status == false && a.receiverId == studentId).Count();
			
			return PartialView("_ChatStudentCount");
		}











		// GET: groups / View Training Groups
		public ActionResult ViewTrainingGroups()
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("1"))// It checks if the person who requested the page is a student or not
				return RedirectToAction("acessdenied");


			var groups = db.groups.Include(g => g.cours)
									.Include(g => g.day)
									.Include(g => g.department)
									.Include(g => g.hospital)
									.Include(g => g.shift)
									.Include(g => g.student)
									.Include(g => g.student1)
									.Include(g => g.student2)
									.Include(g => g.student3)
									.Include(g => g.student4)
									.Include(g => g.student5)
									.Include(g => g.trainer);


			ViewBag.courseId = new SelectList(db.courses, "Id", "name");
			ViewBag.hospitalId = new SelectList(db.hospitals, "Id", "name");
			ViewBag.dayId = new SelectList(db.days, "Id", "name");

			IEnumerable<group> gr = groups.ToList().OrderBy(a => a.courseId);

			List<group> result = new List<group>();

			foreach (group g in gr)
			{
				if (isFound(Session["id"].ToString(), g))
					result.Add(g);
			}

			return View(result);
		}







		// check if this student is already in this group or not
		private bool isFound(string id, group gr)
		{
			if (gr.studentIdOne.ToString().Equals(id))
				return true;

			if (gr.studentIdTwo.ToString().Equals(id))
				return true;

			if (gr.studentIdThree != null)
				if (gr.studentIdThree.ToString().Equals(id))
					return true;

			if (gr.studentIdFour != null)
				if (gr.studentIdFour.ToString().Equals(id))
					return true;

			if (gr.studentIdFive != null)
				if (gr.studentIdFive.ToString().Equals(id))
					return true;

			if (gr.studentIdSix != null)
				if (gr.studentIdSix.ToString().Equals(id))
					return true;

			return false;
		}
















		// GET: student chat page
		[HttpGet]
		public ActionResult StudentChat()
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("1"))// It checks if the person who requested the page is a student or not
				return RedirectToAction("acessdenied");



			var studentId = Int64.Parse(Session["id"].ToString());

			IEnumerable<chat> allChats = db.chats.Where(a => a.senderId == studentId || a.receiverId == studentId)
												  .OrderBy(a => a.date).ToList();

			ViewBag.allChats = allChats;

			return View();
		}













		// POST: student chat page
		[HttpPost]
		public ActionResult StudentChat([Bind(Include = "message")] chat chat)
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("1"))// It checks if the person who requested the page is a student or not
				return RedirectToAction("acessdenied");


			if (ModelState.IsValid)
			{
				try
				{
					chat.senderId = Int64.Parse(Session["ID"].ToString());
					chat.receiverId = 123;
					chat.date = DateTime.Now;
					if (chat.message != null && isMessageContainsChar(chat.message))
					{
						chat.message = chat.message.Trim();
						db.chats.Add(chat);
						db.SaveChanges();
					}
				}
				catch (Exception ex)
				{

				}

				return RedirectToAction("StudentChat");
			}

			return View(chat);
		}










		// view all chats between loged in student and admin
		[HttpPost]
		public ActionResult _chatStudent()
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("1"))// It checks if the person who requested the page is a student or not
				return RedirectToAction("acessdenied");

			try
			{
				var studentId = Int64.Parse(Session["id"].ToString());

				IEnumerable<chat> allChats = db.chats.Where(a => a.senderId == studentId || a.receiverId == studentId)
													 .OrderBy(a => a.date).ToList();

				ViewBag.allChats = allChats;


				IEnumerable<chat> chats = db.chats.Where(a => a.receiverId == studentId && a.status == false).ToList();

				foreach (chat c in chats)
				{
					c.status = true;
				}
				db.SaveChanges();
			}
			catch (Exception ex)
			{

			}

			return PartialView("_chatStudent");
		}





		//-----------------------------------------------------------------------------------------------///
		// admin Actions



		// GET: Admin Dashboard that show when admin is loged in
		[HttpGet]
		public ActionResult adminDashboard()
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			ViewBag.RequestPeriod = db.request_period.Find(1).status;

			ViewBag.CountGroups = db.groups.Count();
			ViewBag.CountRequests = db.verified_requests.Where(a => a.approved == false).Count();
			ViewBag.CountApprovedRequests = db.verified_requests.Where(a => a.approved == true).Count();
			ViewBag.CountTrainers = db.trainers.Count();
			ViewBag.CountHospitals = db.hospitals.Count();
			return View();
		}







		[HttpPost]
		public ActionResult adminDashboard(string open, string close)
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");

			if (!string.IsNullOrEmpty(open))
			{
				db.request_period.Find(1).status = true;
				ViewBag.RequestPeriod = true;
			}

			else if (!string.IsNullOrEmpty(close))
			{
				db.request_period.Find(1).status = false;
				ViewBag.RequestPeriod = false;
			}
			try
			{
				db.SaveChanges();
			}
			catch (Exception ex)
			{

			}



			ViewBag.CountGroups = db.groups.Count();
			ViewBag.CountRequests = db.verified_requests.Where(a => a.approved == false).Count();
			ViewBag.CountApprovedRequests = db.verified_requests.Where(a => a.approved == true).Count();
			ViewBag.CountTrainers = db.trainers.Count();
			ViewBag.CountHospitals = db.hospitals.Count();


			return View();
		}







		// GET: students page
		public ActionResult Students()
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			return View(db.students.ToList().OrderBy(a=>a.name));
		}





		// GET: trainers page
		public ActionResult Trainers()
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			return View(db.trainers.ToList().OrderBy(a=>a.name));
		}



		// GET: trainers/Create Trainer
		public ActionResult CreateTrainer()
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			return View();
		}


		// POST: trainers/CreateTrainer
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateTrainer([Bind(Include = "Id,name")] trainer trainer)
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			if (ModelState.IsValid)
			{
				try
				{
					db.trainers.Add(trainer);
					db.SaveChanges();
				}
				catch (Exception ex)
				{

				}

				return RedirectToAction("Trainers");
			}

			return View(trainer);
		}




		// GET: trainers/EditTrainer/ id
		public ActionResult EditTrainer(int? id)
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			trainer trainer = db.trainers.Find(id);

			if (trainer == null)
			{
				return HttpNotFound();
			}

			return View(trainer);
		}






		// POST: trainers/EditTrainer/ ?
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditTrainer([Bind(Include = "Id,name")] trainer trainer)
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			if (ModelState.IsValid)
			{
				try
				{
					db.Entry(trainer).State = EntityState.Modified;
					db.SaveChanges();
				}
				catch (Exception ex)
				{

				}
				return RedirectToAction("Trainers");
			}
			return View(trainer);
		}








		// GET: Courses page
		public ActionResult Courses()
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			return View(db.courses.ToList().OrderBy(a=>a.name));
		}











		// GET: cours/Edit Course/ ?id
		public ActionResult EditCourse(int? id)
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			cours cours = db.courses.Find(id);

			if (cours == null)
			{
				return HttpNotFound();
			}

			return View(cours);
		}











		// POST: cours/Edit Course/ ?id
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditCourse([Bind(Include = "Id,name")] cours cours)
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			if (ModelState.IsValid)
			{
				try
				{
					db.Entry(cours).State = EntityState.Modified;
					db.SaveChanges();
				}
				catch (Exception ex)
				{

				}

				return RedirectToAction("Courses");
			}
			return View(cours);
		}



















		// GET: cours/Create Course page
		public ActionResult CreateCourse()
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			return View();
		}










		// POST: cours/Create Course page
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateCourse([Bind(Include = "Id,name")] cours cours)
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			if (ModelState.IsValid)
			{
				try
				{
					db.courses.Add(cours);
					db.SaveChanges();
				}
				catch (Exception ex)
				{

				}

				return RedirectToAction("Courses");
			}

			return View(cours);
		}











		// GET: hospitals page
		public ActionResult Hospitals()
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			return View(db.hospitals.ToList().OrderBy(a=>a.name));
		}










		// GET: hospitals/Edit Hospital/ ?id
		public ActionResult EditHospital(int? id)
		{

			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			hospital hospital = db.hospitals.Find(id);

			if (hospital == null)
			{
				return HttpNotFound();
			}

			return View(hospital);
		}












		// POST: hospitals/Edit Hospital/ ?id
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditHospital([Bind(Include = "Id,name")] hospital hospital)
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			if (ModelState.IsValid)
			{
				try
				{
					db.Entry(hospital).State = EntityState.Modified;
					db.SaveChanges();
				}
				catch (Exception ex)
				{

				}

				return RedirectToAction("Hospitals");
			}
			return View(hospital);
		}
















		// GET: hospitals/Create Hospital page
		public ActionResult CreateHospital()
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			return View();
		}











		// POST: hospitals/Create Hospital page
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateHospital([Bind(Include = "Id,name")] hospital hospital)
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			if (ModelState.IsValid)
			{
				try
				{
					db.hospitals.Add(hospital);
					db.SaveChanges();
				}
				catch (Exception ex)
				{

				}

				return RedirectToAction("Hospitals");
			}

			return View(hospital);
		}













		// GET: depatrments page
		public ActionResult Departments()
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			return View(db.departments.ToList().OrderBy(a => a.name));
		}












		// GET: departments/Create Department page
		public ActionResult CreateDepartment()
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			return View();
		}












		// POST: departments/Create Department page
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateDepartment([Bind(Include = "Id,name")] department department)
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			if (ModelState.IsValid)
			{
				try
				{
					db.departments.Add(department);
					db.SaveChanges();
				}
				catch (Exception ex)
				{

				}

				return RedirectToAction("Departments");
			}

			return View(department);
		}










		// GET: departments/Edit Department/ ?id
		public ActionResult EditDepartment(int? id)
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			department department = db.departments.Find(id);

			if (department == null)
			{
				return HttpNotFound();
			}

			return View(department);
		}














		// POST: departments/Edit Department/ ?id
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditDepartment([Bind(Include = "Id,name")] department department)
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			if (ModelState.IsValid)
			{
				try
				{
					db.Entry(department).State = EntityState.Modified;
					db.SaveChanges();
				}
				catch (Exception ex)
				{

				}

				return RedirectToAction("Departments");
			}

			return View(department);
		}














		// Logout Page (this page does not contain any design its just kill the Session and Redirect To Action Login)
		[HttpGet]
		public ActionResult Logout()
		{
			Session.Abandon();
			return RedirectToAction("Login", "Home");
		}









		// GET: verified_requests( not approvedRequests ) page
		public ActionResult Requests()
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			var verified_requests = db.verified_requests.Include(v => v.cours)
														.Include(v => v.student)
														.Include(v => v.student1)
														.Include(v => v.student2)
														.Include(v => v.student3)
														.Include(v => v.student4)
														.Include(v => v.student5)
														.Include(v => v.temp_requests);

			
			ViewBag.courseId = new SelectList(db.courses, "Id", "name");
			ViewBag.studentIdOne = new SelectList(db.students, "Id", "name");
			ViewBag.studentIdTwo = new SelectList(db.students, "Id", "name");
			ViewBag.studentIdThree = new SelectList(db.students, "Id", "name");
			ViewBag.studentIdFour = new SelectList(db.students, "Id", "name");
			ViewBag.studentIdFive = new SelectList(db.students, "Id", "name");
			ViewBag.studentIdSix = new SelectList(db.students, "Id", "name");
			ViewBag.id = new SelectList(db.temp_requests, "id", "note");

			return View(verified_requests.Where( a => a.approved == false).ToList().OrderBy(a=>a.cours.name));
		}











		[HttpPost]
		public ActionResult Requests([Bind(Include = "id")] verified_requests verified_requests, string CreateGroup, string SeenRequests)
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			if (!string.IsNullOrEmpty(CreateGroup))
			{
				verified_requests info = db.verified_requests.Find(verified_requests.id);
				if (info != null)
				{
					group temp = new group();

					temp.cours = info.cours;
					temp.courseId = info.courseId;

					temp.student = info.student;
					temp.student1 = info.student1;
					temp.student2 = info.student2;
					temp.student3 = info.student3;
					temp.student4 = info.student4;
					temp.student5 = info.student5;

					temp.studentIdOne = info.studentIdOne;
					temp.studentIdTwo = info.studentIdTwo;
					temp.studentIdThree = info.studentIdThree;
					temp.studentIdFour = info.studentIdFour;
					temp.studentIdFive = info.studentIdFive;
					temp.studentIdSix = info.studentIdSix;

					Session["myGroup"] = temp;
					Session["requestId"] = verified_requests.id;
					return RedirectToAction("CreateGroup");
				}
				
			}
			else if (!string.IsNullOrEmpty(SeenRequests))
			{
				return RedirectToAction("SeenRequests", verified_requests);
			}

			return RedirectToAction("Requests", verified_requests);
		}









		// function to make approved column in verified_requests teble with true value
		public ActionResult SeenRequests([Bind(Include = "id")] verified_requests verified_requests)
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			if (ModelState.IsValid)
			{
				try
				{
					verified_requests r = db.verified_requests.Find(verified_requests.id);
					db.verified_requests.Remove(r);
					db.SaveChanges();
					r.approved = true;
					db.verified_requests.Add(r);
					db.SaveChanges();
				}
				catch (Exception ex)
				{

				}

				return RedirectToAction("Requests");
			}

			ViewBag.courseId = new SelectList(db.courses, "Id", "name", verified_requests.courseId);
			ViewBag.studentIdOne = new SelectList(db.students, "Id", "name", verified_requests.studentIdOne);
			ViewBag.studentIdTwo = new SelectList(db.students, "Id", "name", verified_requests.studentIdTwo);
			ViewBag.studentIdThree = new SelectList(db.students, "Id", "name", verified_requests.studentIdThree);
			ViewBag.studentIdFour = new SelectList(db.students, "Id", "name", verified_requests.studentIdFour);
			ViewBag.studentIdFive = new SelectList(db.students, "Id", "name", verified_requests.studentIdFive);
			ViewBag.studentIdSix = new SelectList(db.students, "Id", "name", verified_requests.studentIdSix);
			ViewBag.id = new SelectList(db.temp_requests, "id", "note", verified_requests.id);

			return RedirectToAction("Requests");

		}









		[HttpGet]
		// GET: verified_requests( approvedRequests )
		public ActionResult approvedRequests()
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			var verified_requests = db.verified_requests.Include(v => v.cours)
														.Include(v => v.student)
														.Include(v => v.student1)
														.Include(v => v.student2)
														.Include(v => v.student3)
														.Include(v => v.student4)
														.Include(v => v.student5)
														.Include(v => v.temp_requests);


			ViewBag.courseId = new SelectList(db.courses, "Id", "name");
			ViewBag.studentIdOne = new SelectList(db.students, "Id", "name");
			ViewBag.studentIdTwo = new SelectList(db.students, "Id", "name");
			ViewBag.studentIdThree = new SelectList(db.students, "Id", "name");
			ViewBag.studentIdFour = new SelectList(db.students, "Id", "name");
			ViewBag.studentIdFive = new SelectList(db.students, "Id", "name");
			ViewBag.studentIdSix = new SelectList(db.students, "Id", "name");
			ViewBag.id = new SelectList(db.temp_requests, "id", "note");

			return View(verified_requests.Where(a => a.approved == true).OrderBy(a=>a.cours.name).ToList());
		}








		//POST: verified_requests( approvedRequests )
		[HttpPost]
		public ActionResult approvedRequests([Bind(Include = "id,studentIdOne,studentIdTwo,studentIdThree,studentIdFour,studentIdFive,studentIdSix,courseId,note,approved")] verified_requests verified_requests)
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			if (ModelState.IsValid)
			{
				try
				{
					//db.verified_requests.Find(verified_requests.id).approved = false;

					verified_requests r = db.verified_requests.Find(verified_requests.id);
					db.verified_requests.Remove(r);
					db.SaveChanges();
					r.approved = false;
					db.verified_requests.Add(r);
					db.SaveChanges();
				}
				catch ( Exception ex )
				{

				}

				return RedirectToAction("approvedRequests");
			}
			ViewBag.courseId = new SelectList(db.courses, "Id", "name", verified_requests.courseId);
			ViewBag.studentIdOne = new SelectList(db.students, "Id", "name", verified_requests.studentIdOne);
			ViewBag.studentIdTwo = new SelectList(db.students, "Id", "name", verified_requests.studentIdTwo);
			ViewBag.studentIdThree = new SelectList(db.students, "Id", "name", verified_requests.studentIdThree);
			ViewBag.studentIdFour = new SelectList(db.students, "Id", "name", verified_requests.studentIdFour);
			ViewBag.studentIdFive = new SelectList(db.students, "Id", "name", verified_requests.studentIdFive);
			ViewBag.studentIdSix = new SelectList(db.students, "Id", "name", verified_requests.studentIdSix);
			ViewBag.id = new SelectList(db.temp_requests, "id", "note", verified_requests.id);
			return RedirectToAction("approvedRequests");

		}








		// View the number of approved requests on dashboard 
		public ActionResult _notification()
		{
			ViewBag.count = db.verified_requests.Where( a => a.approved == true).Count();
			return PartialView("_notification");
		}










		// View the number of unapproved requests on dashboard 
		public ActionResult _notification2()
		{
			ViewBag.count2 = db.verified_requests.Where(a => a.approved == false).Count();
			return PartialView("_notification2");
		}






		// Read Elelloooo
		// View the number of Unseen chats on dashboard 
		public ActionResult _ChatCount()
		{
			ViewBag.ChatsCount = db.chats.Where(a => a.status == false && a.receiverId == 123).Count();
			return PartialView("_ChatCount");
		}
		








		// GET: groups page
		public ActionResult Groups()
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			var groups = db.groups.Include(g => g.cours)
									.Include(g => g.day)
									.Include(g => g.department)
									.Include(g => g.hospital)
									.Include(g => g.shift)
									.Include(g => g.student)
									.Include(g => g.student1)
									.Include(g => g.student2)
									.Include(g => g.student3)
									.Include(g => g.student4)
									.Include(g => g.student5)
									.Include(g => g.trainer);


			ViewBag.courseId = new SelectList(db.courses, "Id", "name");
			ViewBag.hospitalId = new SelectList(db.hospitals, "Id", "name");
			ViewBag.dayId = new SelectList(db.days, "Id", "name");
			return View(groups.ToList().OrderBy(a=> a.cours.name));
		}

		
		
		public ActionResult DeleteGroup([Bind(Include = "courseId,hospitalId,departmentId,dayId,shiftId,trainerId,studentIdOne,studentIdTwo,studentIdThree,studentIdFour,studentIdFive,studentIdSix")] group group)
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");

			try
			{
				if (!db.groups.Local.Contains(group))
				{
					db.groups.Attach(group);
				}

				db.groups.Remove(group);
				db.SaveChanges();
				deleted_groups temp = new deleted_groups();

				temp.courseId = group.courseId;
				temp.hospitalId = group.hospitalId;
				temp.departmentId = group.departmentId;
				temp.dayId = group.dayId;
				temp.shiftId = group.shiftId;
				temp.trainerId = group.trainerId;


				temp.studentIdOne = group.studentIdOne;
				temp.studentIdTwo = group.studentIdTwo;
				temp.studentIdThree = group.studentIdThree;
				temp.studentIdFour = group.studentIdFour;
				temp.studentIdFive = group.studentIdFive;
				temp.studentIdSix = group.studentIdSix;

				db.deleted_groups.Add(temp);
				db.SaveChanges();
			}
			catch (Exception ex)
			{

			}

			return RedirectToAction("groups");
		}

		
		public ActionResult TempEditGroup([Bind(Include = "courseId,hospitalId,departmentId,dayId,shiftId,trainerId,studentIdOne,studentIdTwo,studentIdThree,studentIdFour,studentIdFive,studentIdSix,flag")] group group)
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			group.student = db.students.Find(group.studentIdOne);
			group.student1 = db.students.Find(group.studentIdTwo);
			group.student2 = db.students.Find(group.studentIdThree);
			group.student3 = db.students.Find(group.studentIdFour);
			group.student4 = db.students.Find(group.studentIdFive);
			group.student5 = db.students.Find(group.studentIdSix);
			Session["group"] = group;

			return RedirectToAction("EditGroup");
		}




		// GET: groups/Create Group page
		public ActionResult CreateGroup()
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			try
			{
				if (Session["myGroup"] != null)
				{
					group temp = (group)Session["myGroup"];
					ViewBag.courseId = new SelectList(db.courses, "Id", "name", temp.courseId);
				}
				else
					ViewBag.courseId = new SelectList(db.courses, "Id", "name");

				ViewBag.dayId = new SelectList(db.days, "Id", "name");
				ViewBag.departmentId = new SelectList(db.departments, "Id", "name");
				ViewBag.hospitalId = new SelectList(db.hospitals, "Id", "name");
				ViewBag.shiftId = new SelectList(db.shifts, "Id", "name");
				ViewBag.studentIdOne = new SelectList(db.students, "Id", "name");
				ViewBag.studentIdTwo = new SelectList(db.students, "Id", "name");
				ViewBag.studentIdThree = new SelectList(db.students, "Id", "name");
				ViewBag.studentIdFour = new SelectList(db.students, "Id", "name");
				ViewBag.studentIdFive = new SelectList(db.students, "Id", "name");
				ViewBag.studentIdSix = new SelectList(db.students, "Id", "name");
				ViewBag.trainerId = new SelectList(db.trainers, "Id", "name");
				ViewBag.flagCreate = 1;
			}
			catch (Exception ex)
			{

			}

			return View();
		}











		// POST: groups/Create Group
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateGroup([Bind(Include = "courseId,hospitalId,departmentId,dayId,shiftId,trainerId,studentIdOne,studentIdTwo,studentIdThree,studentIdFour,studentIdFive,studentIdSix,flag")] group group)
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			if (ModelState.IsValid)
			{
				try
				{
					db.groups.Add(group);
					db.SaveChanges();

					if (Session["myGroup"] != null)
					{
						Int32 requestId = Int32.Parse(Session["requestId"].ToString());
						db.verified_requests.Find(requestId).approved = true;
						db.SaveChanges();
						return RedirectToAction("Requests");
					}
					else
						return RedirectToAction("groups");
				}
				catch (Exception ex)
				{

				}
				return RedirectToAction("groups");
			}

			ViewBag.courseId = new SelectList(db.courses, "Id", "name", group.courseId);
			ViewBag.dayId = new SelectList(db.days, "Id", "name", group.dayId);
			ViewBag.departmentId = new SelectList(db.departments, "Id", "name", group.departmentId);
			ViewBag.hospitalId = new SelectList(db.hospitals, "Id", "name", group.hospitalId);
			ViewBag.shiftId = new SelectList(db.shifts, "Id", "name", group.shiftId);
			ViewBag.studentIdOne = new SelectList(db.students, "Id", "name", group.studentIdOne);
			ViewBag.studentIdTwo = new SelectList(db.students, "Id", "name", group.studentIdTwo);
			ViewBag.studentIdThree = new SelectList(db.students, "Id", "name", group.studentIdThree);
			ViewBag.studentIdFour = new SelectList(db.students, "Id", "name", group.studentIdFour);
			ViewBag.studentIdFive = new SelectList(db.students, "Id", "name", group.studentIdFive);
			ViewBag.studentIdSix = new SelectList(db.students, "Id", "name", group.studentIdSix);
			ViewBag.trainerId = new SelectList(db.trainers, "Id", "name", group.trainerId);
			ViewBag.flagCreate = 1;
			return View(group);
		}




		// GET: groups/Edit/ ? ? ? ?
		public ActionResult EditGroup()
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			if (Session["group"]  == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			group myGroup = (group)Session["group"];

			if (myGroup == null)
			{
				return HttpNotFound();
			}

			ViewBag.courseId = new SelectList(db.courses, "Id", "name", myGroup.courseId);
			ViewBag.dayId = new SelectList(db.days, "Id", "name", myGroup.dayId);
			ViewBag.departmentId = new SelectList(db.departments, "Id", "name", myGroup.departmentId);
			ViewBag.hospitalId = new SelectList(db.hospitals, "Id", "name", myGroup.hospitalId);
			ViewBag.shiftId = new SelectList(db.shifts, "Id", "name", myGroup.shiftId);
			ViewBag.studentIdOne = new SelectList(db.students, "Id", "name", myGroup.studentIdOne);
			ViewBag.studentIdTwo = new SelectList(db.students, "Id", "name", myGroup.studentIdTwo);
			ViewBag.studentIdThree = new SelectList(db.students, "Id", "name", myGroup.studentIdThree);
			ViewBag.studentIdFour = new SelectList(db.students, "Id", "name", myGroup.studentIdFour);
			ViewBag.studentIdFive = new SelectList(db.students, "Id", "name", myGroup.studentIdFive);
			ViewBag.studentIdSix = new SelectList(db.students, "Id", "name", myGroup.studentIdSix);
			ViewBag.trainerId = new SelectList(db.trainers, "Id", "name", myGroup.trainerId);
			ViewBag.flagEdit = 2;

			
			return View(myGroup);
		}

















		// POST: groups/Edit
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditGroup([Bind(Include = "courseId,oldHospitalId,oldDepartmentId,oldDayId,oldShiftId,oldTrainerId,hospitalId,departmentId,dayId,shiftId,trainerId,studentIdOne,studentIdTwo,studentIdThree,studentIdFour,studentIdFive,studentIdSix,flag")] group group)
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			if (ModelState.IsValid)
			{
				try
				{

					group temp = (group)Session["group"];

					group oldgroup = new group();

					oldgroup.studentIdOne = temp.studentIdOne;
					oldgroup.studentIdTwo = temp.studentIdTwo;
					oldgroup.studentIdThree = temp.studentIdThree;
					oldgroup.studentIdFour = temp.studentIdFour;
					oldgroup.studentIdFive = temp.studentIdFive;
					oldgroup.studentIdSix = temp.studentIdSix;

					oldgroup.courseId = temp.courseId;

					oldgroup.hospitalId = temp.hospitalId;
					oldgroup.departmentId = temp.departmentId;

					oldgroup.dayId = temp.dayId;
					oldgroup.shiftId = temp.shiftId;

					oldgroup.trainerId = temp.trainerId;

					group objGroup = new group();

					objGroup.studentIdOne = group.studentIdOne;
					objGroup.studentIdTwo = group.studentIdTwo;
					objGroup.studentIdThree = group.studentIdThree;
					objGroup.studentIdFour = group.studentIdFour;
					objGroup.studentIdFive = group.studentIdFive;
					objGroup.studentIdSix = group.studentIdSix;
					objGroup.courseId = group.courseId;
					objGroup.hospitalId = group.hospitalId;
					objGroup.departmentId = group.departmentId;
					objGroup.dayId = group.dayId;
					objGroup.shiftId = group.shiftId;
					objGroup.trainerId = group.trainerId;

					if (!db.groups.Local.Contains(oldgroup))
					{
						db.groups.Attach(oldgroup);
					}

					db.groups.Remove(oldgroup);


					db.SaveChanges();

					db.groups.Add(objGroup);
					db.SaveChanges();
				}
				catch (Exception ex)
				{

				}
				
				return RedirectToAction("Groups");

			}
			ViewBag.courseId = new SelectList(db.courses, "Id", "name", group.courseId);
			ViewBag.dayId = new SelectList(db.days, "Id", "name", group.dayId);
			ViewBag.departmentId = new SelectList(db.departments, "Id", "name", group.departmentId);
			ViewBag.hospitalId = new SelectList(db.hospitals, "Id", "name", group.hospitalId);
			ViewBag.shiftId = new SelectList(db.shifts, "Id", "name", group.shiftId);
			ViewBag.studentIdOne = new SelectList(db.students, "Id", "name", group.studentIdOne);
			ViewBag.studentIdTwo = new SelectList(db.students, "Id", "name", group.studentIdTwo);
			ViewBag.studentIdThree = new SelectList(db.students, "Id", "name", group.studentIdThree);
			ViewBag.studentIdFour = new SelectList(db.students, "Id", "name", group.studentIdFour);
			ViewBag.studentIdFive = new SelectList(db.students, "Id", "name", group.studentIdFive);
			ViewBag.studentIdSix = new SelectList(db.students, "Id", "name", group.studentIdSix);
			ViewBag.trainerId = new SelectList(db.trainers, "Id", "name", group.trainerId);
			ViewBag.flagEdit = 2;
			return View(group);
		}


		// GET: Deleted Groups page
		public ActionResult DeletedGroups()
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			var groups = db.deleted_groups.Include(g => g.cours)
											.Include(g => g.day)
											.Include(g => g.department)
											.Include(g => g.hospital)
											.Include(g => g.shift)
											.Include(g => g.student)
											.Include(g => g.student1)
											.Include(g => g.student2)
											.Include(g => g.student3)
											.Include(g => g.student4)
											.Include(g => g.student5)
											.Include(g => g.trainer);

			
			ViewBag.courseId = new SelectList(db.courses, "Id", "name");
			ViewBag.hospitalId = new SelectList(db.hospitals, "Id", "name");
			ViewBag.dayId = new SelectList(db.days, "Id", "name");
			
			return View(groups.ToList().OrderBy(a => a.cours.name));
		}

















		// GET: chats/ Admin Chat page
		[HttpGet]
		public ActionResult AdminChat()
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			var adminId = 123;

			IEnumerable<IGrouping<long,chat>> allChats = db.chats.Where(a =>a.senderId != adminId)
				                                                  .GroupBy(a => a.senderId)
																  .OrderBy(a=>a.Max(bb=>bb.date)).ToList();
			

			List<student> allStudentsFromDatabase = db.students.ToList();

			List<chat> allChatStudentsfromChat = new List<chat>();

			foreach (IGrouping<long, chat> item in allChats)
			{

				chat c = item.LastOrDefault();
				
				student s = allStudentsFromDatabase.Find(a => a.Id == c.senderId);
				allStudentsFromDatabase.Remove(s);

				allStudentsFromDatabase.Insert(0,s);

				allChatStudentsfromChat.Insert(0, c);
			}

			List<student_chat> allStudentsWithChats = new List<student_chat>();


			int i = 0;
			foreach (chat item in allChatStudentsfromChat)
			{
				
				allStudentsWithChats.Add(new student_chat(allStudentsFromDatabase[i++], item) );
			}

			for (int j = i; j < allStudentsFromDatabase.Count; j++)
			{
				allStudentsWithChats.Add(new student_chat(allStudentsFromDatabase[j], null));
			}

			ViewBag.allStudentsWithChats = allStudentsWithChats;


			return View();
		}











		// save who student will admin to show messages
		[HttpPost]
		public ActionResult _saveStudentIdToSession([Bind(Include = "receiverId")] chat chat)
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");

			try
			{
				Session["receiverId"] = chat.receiverId;

				Int64 receiverId = Int64.Parse(chat.receiverId.ToString());

				IEnumerable<chat> chats = db.chats.Where(a => a.senderId == receiverId && a.status == false).ToList();

				foreach (chat c in chats)
				{
					c.status = true;
				}
				db.SaveChanges();
			}
			catch (Exception ex)
			{

			}

			return PartialView("_saveStudentIdToSession");
		}















		[HttpPost]
		public ActionResult SendToAll([Bind(Include = "message")] chat chat)
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");

			try
			{
				chat.senderId = 123;

				foreach (student s in db.students.ToList())
				{
					chat temp = new chat();

					temp.senderId = chat.senderId;

					temp.receiverId = s.Id;

					temp.date = DateTime.Now;

					temp.message = chat.message;

					if (chat.message != null && isMessageContainsChar(chat.message))
						db.chats.Add(temp);
				}
				db.SaveChanges();
			}
			catch (Exception ex)
			{

			}
			return RedirectToAction("AdminChat");
		}






		// POST: chats/ Admin Chat page
		[HttpPost]
		public ActionResult AdminChat([Bind(Include = "message")] chat chat)
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			if (ModelState.IsValid)
			{
				try
				{
					if (Session["receiverId"] != null)
					{
						chat.receiverId = Int64.Parse(Session["receiverId"].ToString());
						chat.senderId = 123;
						chat.date = DateTime.Now;



						if (chat.message != null && isMessageContainsChar(chat.message))
						{
							chat.message = chat.message.Trim();

							db.chats.Add(chat);
							db.SaveChanges();
						}
					}
				}
				catch (Exception ex)
				{

				}

				return RedirectToAction("AdminChat");
			}

			return View(chat);
		}






		// validation on text of message
		private bool isMessageContainsChar(string message)
		{
			foreach (char c in message)
			{
				if (  (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') 
					|| (c >='0' && c<= '9') || ( c >= 'أ' && c <= 'ي' ) || c=='?' || c=='؟' || c=='!')
					return true;
			}
			return false;
		}













		[HttpPost]
		public ActionResult _chatAdmin()
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			var adminId = 123;

			IEnumerable<IGrouping<long, chat>> allChats = db.chats.Where(a => a.senderId != adminId).GroupBy(a => a.senderId).OrderBy(a => a.Max(bb => bb.date)).ToList();


			List<student> allStudentsFromDatabase = db.students.ToList();

			List<chat> allChatStudentsfromChat = new List<chat>();

			foreach (IGrouping<long, chat> item in allChats)
			{

				chat c = item.LastOrDefault();

				student s = allStudentsFromDatabase.Find(a => a.Id == c.senderId);
				allStudentsFromDatabase.Remove(s);

				allStudentsFromDatabase.Insert(0, s);

				allChatStudentsfromChat.Insert(0, c);
			}

			List<student_chat> allStudentsWithChats = new List<student_chat>();


			int i = 0;
			foreach (chat item in allChatStudentsfromChat)
			{

				allStudentsWithChats.Add(new student_chat(allStudentsFromDatabase[i++], item));
			}

			for (int j = i; j < allStudentsFromDatabase.Count; j++)
			{
				allStudentsWithChats.Add(new student_chat(allStudentsFromDatabase[j], null));
			}

			ViewBag.allStudentsWithChats = allStudentsWithChats;


			return PartialView("_chatAdmin");
		}
















		[HttpPost]
		public ActionResult _chatAdminStudent()
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			if (Session["receiverId"] == null)
			{
				ViewBag.allChats = null;
			}
			else
			{
				var studentId = Int64.Parse(Session["receiverId"].ToString());

				IEnumerable<chat> allChats = db.chats.Where(a => a.senderId == studentId || a.receiverId == studentId)
					                                 .OrderBy(a => a.date).ToList();

				ViewBag.allChats = allChats;
			}

			return PartialView("_chatAdminStudent");

		}












		public ActionResult _chatHeader()
		{
			if (Session["id"] == null) // check if any user loged in or not
				return RedirectToAction("Login");

			if (!Session["type"].ToString().Equals("2"))// It checks if the person who requested the page is a admin or not
				return RedirectToAction("acessdenied");


			if (Session["receiverId"] != null)
				ViewBag.studentName = "Chat with "+db.students.Find(Int64.Parse(Session["receiverId"].ToString())).name;
			else
				ViewBag.studentName = "Chat with";

			return PartialView("_chatHeader");
		}



		public ActionResult _GroupsCount()
		{
			ViewBag.GroupsCount = db.groups.Count();
			return PartialView("_GroupsCount");
		}


		
		public ActionResult _DeletedGroupsCount()
		{
			ViewBag.DeletedGroupsCount = db.deleted_groups.Count();
			return PartialView("_DeletedGroupsCount");
		}


	}
}