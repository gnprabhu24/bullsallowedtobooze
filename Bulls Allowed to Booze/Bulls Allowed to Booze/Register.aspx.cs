using System;
using System.Web.Services;

namespace Bulls_Allowed_to_Booze
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static bool sendData(string data)
        {

            WebServiceReference.Service1Client webserviceclient = new WebServiceReference.Service1Client();
            return webserviceclient.RegisterUID(data);


        }
    }
    
}