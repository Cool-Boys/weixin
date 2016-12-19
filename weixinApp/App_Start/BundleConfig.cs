using System.Web;
using System.Web.Optimization;

namespace weixinApp
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //SUI注册
            bundles.Add(new ScriptBundle("~/bundles/SUI").Include(
                         "~/Scripts/SUI/zepto.js",
                          "~/Scripts/SUI/config.js",
                         "~/Scripts/SUI/js/sm.js",
                        "~/Scripts/SUI/js/sm-extend.js",
                         "~/Scripts/jweixin-1.0.0.js",
                        "~/Scripts/common.js"
                        ));
            //样式
            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Scripts/SUI/css/sm.css",
                "~/Scripts/SUI/css/sm-extend.css",
                 "~/Scripts/SUI/css/common.css"
                ));
        }
    }
}