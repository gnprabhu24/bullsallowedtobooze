using System;
using System.Text;
using System.Web;
using System.Web.Services;

namespace Bulls_Allowed_to_Booze
{
    public partial class Verify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string encoded_user_obj = HttpUtility.UrlDecode(Request.QueryString["id"]);

            lblUID.InnerHtml = Encoding.UTF8.GetString(Convert.FromBase64String(encoded_user_obj));
            txtUID.Value = Encoding.UTF8.GetString(Convert.FromBase64String(encoded_user_obj));
            

        }
        
        [WebMethod]
        public static bool verifyUID(string id, bool isverified, string comment)
        {
            WebServiceReference.Service1Client webserviceclient = new WebServiceReference.Service1Client();

            return webserviceclient.VerifyUID(id, isverified, comment);
        }

        
    }

    
}