namespace CountdownMvc.App_Start
{
	using System.Web.Optimization;

	/// <summary>
	/// the bungle config for register bundles.
	/// </summary>
	public static class BundleConfig
	{
		/// <summary>
		/// Registers the bundles.
		/// </summary>
		/// <param name="bundles">The bundles.</param>
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js",
						"~/Scripts/jquery-ui-{version}.js",
						"~/Scripts/jquery.cookie.js"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.js",
					  "~/Scripts/respond.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.validate*"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/bootstrap.css",
					  "~/Content/site.css",
					  "~/Content/colorpicker.css",
					  "~/Content/myStyle.css",
					  "~/Content/bootstrap-fileinput/css/fileinput.css"));

			bundles.Add(new ScriptBundle("~/bundles/angular").Include(
					   "~/Scripts/angular.js",
					   "~/Scripts/angular-cookies.js",
					   "~/Scripts/angular-animate.js",
						"~/Scripts/angular-ui/ui-bootstrap.js",
						"~/Scripts/angular-ui/ui-bootstrap-tpls.min.js",
						"~/Scripts/ngDraggable.js",
						"~/Scripts/Checklist-model.js",
						  "~/Scripts/bootstrap-colorpicker-module.js"));

			bundles.Add(new ScriptBundle("~/bundles/countdowns").Include(
				  "~/Scripts/MyScripts/Infrastructure/CountdownCollection.js",
						  "~/Scripts/MyScripts/Infrastructure/Formats.js",
						  "~/Scripts/MyScripts/Infrastructure/Times.js",
						"~/Scripts/MyScripts/App.js",
						"~/Scripts/MyScripts/Controllers/ApplicationController.js",
						"~/Scipts/MyScripts/Directives/ProgressbarDirective.js",
						"~/Scripts/MyScripts/Filters/NewLineParagraph.js",
						"~/Scripts/MyScripts/Directives/MyDrag.js",
						"~/Scripts/MyScripts/Directives/onFinishRender.js",
						 "~/Scripts/MyScripts/Services/CountdownService.js",
						  "~/Scripts/MyScripts/Controllers/CountdownController.js",
						  "~/Scripts/MyScripts/Services/SettingsService.js",
						  "~/Scripts/MyScripts/Controllers/SettingController.js",
						  "~/Scripts/MyScripts/Controllers/FileController.js",
						  "~/Scripts/MyScripts/Controllers/ExerciseController.js",
						  "~/Scripts/MyScripts/Controllers/RepeatController.js",
						  "~/Scripts/MyScripts/Directives/TextName.js",
						  "~/Scripts/MyScripts/Modals/NotifyModal.js",
						  "~/Scripts/MyScripts/Modals/CountdownModal.js"));

			bundles.Add(new ScriptBundle("~/bundles/logs").Include(
				"~/Scripts/MyScripts/Logger/logApp.js",
				"~/Scripts/MyScripts/Logger/loggerCtrl.js"));

			bundles.Add(new ScriptBundle("~/bundles/anim").Include(
				"~/Scripts/MyScripts/Smoke/animation.css",
				"~/Scripts/MyScripts/Smoke/settingsAnim.js"));
		}
	}
}
