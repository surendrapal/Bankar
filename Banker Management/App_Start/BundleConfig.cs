using System.Web;
using System.Web.Optimization;

namespace BM.Web.Settings
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").IncludeDirectory("~/assets/js/", "*.js", false));
            bundles.Add(new ScriptBundle("~/bundles/date-picker").IncludeDirectory("~/assets/js/date-picker", "*.js", false));
            bundles.Add(new ScriptBundle("~/bundles/flot").IncludeDirectory("~/assets/js/flot", "*.js", false));
            bundles.Add(new ScriptBundle("~/bundles/fullcalendar").IncludeDirectory("~/assets/js/fullcalendar", "*.js", false));
            bundles.Add(new ScriptBundle("~/bundles/gallery").IncludeDirectory("~/assets/js/gallery", "*.js", false));
            bundles.Add(new ScriptBundle("~/bundles/morris").IncludeDirectory("~/assets/js/morris", "*.js", false));
            bundles.Add(new ScriptBundle("~/bundles/nvd").IncludeDirectory("~/assets/js/nvd", "*.js", false));
            bundles.Add(new ScriptBundle("~/bundles/pie-charts").IncludeDirectory("~/assets/js/pie-charts", "*.js", false));
            bundles.Add(new ScriptBundle("~/bundles/wizard").IncludeDirectory("~/assets/js/wizard", "*.js", false));
            bundles.Add(new ScriptBundle("~/bundles/wysiwyg").IncludeDirectory("~/assets/js/wysiwyg", "*.js", false));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/assets/js/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/assets/js/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryajaxval").Include(
                       "~/assets/js/jquery.unobtrusive*"));


            bundles.Add(new ScriptBundle("~/bundles/custom").Include("~/assets/js/bootstrap-datepicker.js", "~/assets/js/custom/custom.js", "~/assets/js/custom/custom-forms.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));
        }
    }
}
