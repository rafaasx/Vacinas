using System.Web.Optimization;

namespace Vacinas
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                      "~/Scripts/angular.js",
                      "~/Scripts/angular-sanitize.js",
                      "~/Scripts/angular-route.js",
                      "~/Scripts/angular-br-filters.js",
                      "~/Scripts/angular-locale_pt-br.js",
                      "~/Scripts/angular-input-masks-standalone.js"));
            bundles.Add(new ScriptBundle("~/bundles/angular-app").Include(
                "~/Scripts/app/*.js"));

            bundles.Add(new ScriptBundle("~/bundles/ui-bootstrap").Include(
                                  "~/Scripts/angular-ui/ui-bootstrap-tpls.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
