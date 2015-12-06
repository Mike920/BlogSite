using System.Collections.Generic;
using System.IO;
using System.Web.Optimization;

namespace Blog
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;
            
            //System.Web.Optimization.BundleTable.EnableOptimizations = false;

            bundles.Add(new ScriptBundle(Bundles.Js.Jquery,"//code.jquery.com/jquery-1.11.3.min.js").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle(Bundles.Js.JqueryVal).Include(
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle(Bundles.Js.JqueryUi, "https://code.jquery.com/ui/1.11.4/jquery-ui.min.js").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle(Bundles.Js.Modernizr).Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle(Bundles.Js.Bootstrap,"https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js")
                .Include("~/Scripts/bootstrap.js"));
            bundles.Add(new ScriptBundle(Bundles.Js.Respond, "https://cdnjs.cloudflare.com/ajax/libs/respond.js/1.4.2/respond.min.js").Include(
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle(Bundles.Js.Pace).Include(
                      "~/Scripts/pace.min.js"));

            bundles.Add(new ScriptBundle(Bundles.Js.Plupload)
                {Orderer = new NonOrderingBundleOrderer()}
                .Include(
                      "~/Scripts/plupload/moxie.js",
                      "~/Scripts/plupload/plupload.dev.js",
                      "~/Scripts/plupload/jquery.ui.plupload/jquery.ui.plupload.js"));

            bundles.Add(new ScriptBundle(Bundles.Js.MainSite).Include(
                      "~/Scripts/MainSite/owl.carousel.min.js",
                      "~/Scripts/MainSite/wow.min.js",
                      "~/Scripts/MainSite/slider.js",
                      "~/Scripts/MainSite/jquery.fancybox.js",
                      "~/Scripts/MainSite/main.js"));

            bundles.Add(new ScriptBundle(Bundles.Js.Angular,
                "https://ajax.googleapis.com/ajax/libs/angularjs/1.4.8/angular.min.js").Include(
                "~/Scripts/angular/angular.js"
                ));
            bundles.Add(new ScriptBundle(Bundles.Js.AngularRoute,
                "https://ajax.googleapis.com/ajax/libs/angularjs/1.4.8/angular-route.min.js").Include(
                "~/Scripts/angular/angular-route.js"
                ));
            bundles.Add(new ScriptBundle(Bundles.Js.AngularResource,
                "https://ajax.googleapis.com/ajax/libs/angularjs/1.4.8/angular-resource.min.js").Include(
                "~/Scripts/angular/angular-resource.js"
                ));
   

            bundles.Add(new ScriptBundle(Bundles.Js.AngularApp).Include(
                        "~/SPA/app.js",
                        "~/SPA/Services/*.js",
                        "~/SPA/Controllers/*.js"));
            /*bundles.Add(new ScriptBundle(Bundles.Js.AngularControllers).Include(
                        "~/SPA/Controllers/*.js"));
            bundles.Add(new ScriptBundle(Bundles.Js.AngularServices).Include(
                        "~/SPA/Services/*.js"));*/

            bundles.Add(new ScriptBundle(Bundles.Js.AutoNgModel).Include(
                "~/Scripts/autoNgModel.js"));

            bundles.Add(new StyleBundle(Bundles.Css.Site).Include(
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle(Bundles.Css.Bootstrap, "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css")
                .Include("~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle(Bundles.Css.MainSite).Include(
                      "~/Content/MainSite/ionicons.min.css",
                      "~/Content/MainSite/animate.css",
                      "~/Content/MainSite/slider.css",
                      "~/Content/MainSite/owl.carousel.css",
                      "~/Content/MainSite/owl.theme.css",
                      "~/Content/MainSite/jquery.fancybox.css",
                      "~/Content/MainSite/main.css",
                      "~/Content/MainSite/responsive.css",
                      "~/Content/MainSite/Custom.css"
                      ));

            /*-------------- ADMIN PANEL --------------*/

            bundles.Add(new ScriptBundle(Bundles.Js.AdminPanel).Include(
                        "~/Scripts/AdminPanel/app.js"));


            bundles.Add(new LessBundle(Bundles.Css.AdminPanel).Include("~/Content/AdminPanel/AdminLTE.less"));
            bundles.Add(new LessBundle(Bundles.Css.Skin).Include("~/Content/AdminPanel/skin-blue.less"));
        }
    }

    public class Bundles
    {
        public class Js
        {
            public const string Jquery = "~/js/jquery";
            public const string JqueryVal = "~/js/jqueryVal";
            public const string JqueryUi = "~/js/jqueryUi";
            public const string Modernizr = "~/js/Modernizr";
            public const string Bootstrap = "~/js/Bootstrap";
            public const string Respond = "~/js/Respond";
            public const string Plupload = "~/js/Plupload";
            public const string MainSite = "~/js/MainSite";
            public const string Pace = "~/js/Pace";

            public const string AdminPanel = "~/js/AdminPanel";

            public const string Angular = "~/js/Angular";
            public const string AngularRoute = "~/js/AngularRoute";
            public const string AngularResource = "~/js/AngularResource";

            public const string AngularApp = "~/js/AngularApp";

            public const string AutoNgModel = "~/js/AutoNgModel";

        }

        public class Css
        {
            public const string Site = "~/css/Site";
            public const string Bootstrap = "~/css/Bootstrap";
            public const string MainSite = "~/css/MainSite";
            public const string AdminPanel = "~/css/AdminPanel";
            public const string Skin = "~/css/Skin";

        }
    }

    class NonOrderingBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
}
