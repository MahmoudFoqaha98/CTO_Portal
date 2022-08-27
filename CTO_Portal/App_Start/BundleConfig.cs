using System.Web;
using System.Web.Optimization;

namespace CTO_Portal
{
	public class BundleConfig
	{
		// For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.js"));

			
			
			
			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/bootstrap.css",
					  "~/Content/Site.css"));

			bundles.Add(new StyleBundle("~/myNavStyle").Include(
			"~/Content/CSS/navbar.css"));

			bundles.Add(new StyleBundle("~/indexStyle").Include(
			"~/Content/CSS/index.css"));

			bundles.Add(new StyleBundle("~/loginStyle").Include(
			"~/Content/CSS/login.css",
			"~/Content/Site.css"));

			bundles.Add(new ScriptBundle("~/bundles/popper").Include(
					"~/Scripts/umd/popper.js"));

			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.validate*"));



		}
	}
}
