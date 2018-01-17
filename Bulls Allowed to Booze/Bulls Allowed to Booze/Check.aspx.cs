using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bulls_Allowed_to_Booze
{
    public partial class Check : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string checkUID(string uid)
        {
            WebServiceReference.Service1Client webserviceclient = new WebServiceReference.Service1Client();

            return webserviceclient.CheckUID(uid); 
        }
    }
}