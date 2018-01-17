using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Bulls_Allowed_to_Booze_Service
{
    [ServiceContract]
    public interface IService1
    {
        
        [OperationContract]
        bool RegisterUID(string value);

        [OperationContract]
        bool VerifyUID(string id, bool isverified, string comment);

        [OperationContract]
        string CheckUID(string id);   
    }

    [DataContract]
    public class UIDdetails
    {
        [JsonProperty("uid")]
        public string uid { get; set; }
        [JsonProperty("given_name")]
        public string given_name { get; set; }
        [JsonProperty("last_name")]
        public string last_name { get; set; }
        [JsonProperty("date_of_birth")]
        public string date_of_birth { get; set; }
        [JsonProperty("uid_verified")]
        public bool uid_verified { get; set; }
        [JsonProperty("uid_image")]
        public string uid_image { get; set; }
        [JsonProperty("email")]
        public string email { get; set; }
    }

    [DataContract]
    public class AdminCreds
    {
        public string email { get{ return "You Councellor email-id"; } set { } }
        public string password { get { return "Your Councellor password"; } set { } }
    }

    [DataContract]
    public class ServerCreds
    {
        public string host { get{ return "smtp.gmail.com"; } set { } }
    }

    
}
