using System;
using System.Linq;
namespace Bulls_Allowed_to_Booze
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var page_uri = Request.Url.AbsolutePath;
            var page_name = page_uri.Split('/').Last();

            switch (page_name)
            {
                case "Register.aspx":
                    RegisterLink.Attributes["class"] = "active";
                    break;
                case "Check.aspx":
                    CheckLink.Attributes["class"] = "active";
                    break;
                case "Verify.aspx":
                    AppTabs.Visible = false;
                    break;
            }
        }
    }
}