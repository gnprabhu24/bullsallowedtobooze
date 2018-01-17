using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;
using System.Globalization;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Data;

namespace Bulls_Allowed_to_Booze_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public bool RegisterUID(string value)
        {
            AdminCreds admin = new AdminCreds();
            UIDdetails obj = JsonConvert.DeserializeObject<UIDdetails>(value);
            var cloudinaryURL = storeUID(obj);
            if (!String.IsNullOrEmpty(cloudinaryURL))
            {
                string user_id = JsonConvert.SerializeObject(obj);
                string encoded_user_id = Convert.ToBase64String(Encoding.UTF8.GetBytes(obj.uid));
                string councelorEmail = "councelor.bullsallowedtobooze@gmail.com";
                string emailSubject = "Verification for UID: " + obj.uid;
                string emailbody = "<table align='center' cellspacing='0' style='border:none!important;margin: none !important;' width='auto'>" +
                            "<tr><td colspan='2' bgcolor='#016648' style='text-align: center; padding-top: 10px;'> <img style='border: 2px solid white; border-radius: 5px; width: 130px;' src='https://cdn0.vox-cdn.com/thumbor/nxgcGlGlYmGUiZZzGWDm2nve4Bk=/0x0:547x365/1310x873/cdn0.vox-cdn.com/uploads/chorus_image/image/33573117/Rocky_The_Bull.0.jpg'></td></tr>" +
                            "<tr><td colspan='2' bgcolor='#016648' style='color: white; font-size: 300%; padding: 10px; text-align:center;'><span style='width: 100%;'>Bulls Allowed to Booze</span></td></tr>" +
                            "<tr><td style='padding: 5px;'> Hello Councelor, </td><td style='padding: 5px; text-align: center;'> </td></tr>" +
                             "<tr><td style='padding: 5px;'> </td><td style='padding: 5px; text-align: center;'> </td></tr>" +
                             "<tr><td style='padding: 5px;'> Please verify the following UID details: </td><td style='padding: 5px; text-align: center;'> </td></tr>" +
                             "<tr><td style='padding: 5px;'> </td><td style='padding: 5px; text-align: center;'> </td></tr>" +
                             "<tr><td style='padding: 5px;'> UID: </td><td style='padding: 5px; text-align: center;'>" + obj.uid + "</td></tr>" +
                             "<tr><td style='padding: 5px;'> Given Name: </td><td style='padding: 5px; text-align: center;'>" + obj.given_name + "</td></tr>" +
                             "<tr><td style='padding: 5px;'> Last Name: </td><td style='padding: 5px; text-align: center;'>" + obj.last_name + "</td></tr>" +
                             "<tr><td style='padding: 5px;'> Date of Birth: </td><td style='padding: 5px; text-align: center;'>" + obj.date_of_birth+ "</td></tr>" +
                             "<tr><td style='padding: 5px;'> Photo: </td><td style='padding: 5px; text-align: center;'><img src = '" + cloudinaryURL + "' /></td></tr>" +
                             "<tr><td style='padding: 5px;'> Click link to Validate / Invalidate: </td><td style='padding: 5px; text-align: center;'><a href='http://localhost:55880/Verify.aspx?id=" + HttpUtility.UrlEncode(encoded_user_id) + "'>Validate / Invalidate</a></td></tr>" +
                             "<tr><td style='padding: 5px;'> </td><td style='padding: 5px; text-align: center;'> </td></tr>" +
                             "<tr><td style='padding: 5px;'> </td><td style='padding: 5px; text-align: center;'> </td></tr>" +
                             "<tr><td style='padding: 5px;'>Thanks,</td><td style='padding: 5px; text-align: center;'> </td></tr>" +
                             "<tr><td style='padding: 5px;'>Team Bulls Allowed to Booze</td><td style='padding: 5px; text-align: center;'> </td></tr>" +
                             "<tr><td colspan='2' bgcolor='#016648' style='padding:5px;'></td></tr>" +
                        "</table> ";
                bool checkSendEmail = SendEmail(admin.email, admin.password, councelorEmail, emailSubject, emailbody, true);
                if ( checkSendEmail == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public string storeUID(UIDdetails userInfo)
        {
            string cloudinaryURL = uploadImage(userInfo.uid_image);
            DateTime date_of_birth_obj = DateTime.ParseExact(userInfo.date_of_birth, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            Boolean data_stored;
            string connectionconfig = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionconfig);
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into register(uid,given_name,last_name,email,date_of_birth,date_of_birth_obj,uid_image,uid_verified) values(@uid,@given_name,@last_name,@email,@date_of_birth,@date_of_birth_obj,@uid_image,@uid_verified)", con);
            cmd.Parameters.AddWithValue("@uid", userInfo.uid);
            cmd.Parameters.AddWithValue("@given_name", userInfo.given_name);
            cmd.Parameters.AddWithValue("@last_name", userInfo.last_name);
            cmd.Parameters.AddWithValue("@email", userInfo.email);
            cmd.Parameters.AddWithValue("@date_of_birth", userInfo.date_of_birth);
            cmd.Parameters.AddWithValue("@date_of_birth_obj", date_of_birth_obj);
            cmd.Parameters.AddWithValue("@uid_image", cloudinaryURL);
            cmd.Parameters.AddWithValue("@uid_verified", userInfo.uid_verified);
            try
            {
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    data_stored = true;
                }
                else
                {
                    data_stored = false;
                }
                con.Close();
                if (data_stored)
                {
                    return cloudinaryURL;
                }
                else
                {
                    return "";
                }
            }
            catch(Exception e)
            {
                return "";
            }
        }

        public double generate_unique_id()
        {
            int max = 99999;
            int min = 10000;

           
            double n = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            Random random = new Random();
            int randomNumber = random.Next(0, 10);
            n = Math.Truncate(n * 100 + (randomNumber * (max - min) + min));

            return n;
        }
        
        public string uploadImage(string base64_img)
        {
            Account account = new Account(
                  "dz8yzgnjg",
                  "229824756235718",
                  "H13l7wpvm-R96375_lvzi1Ar1lE");

            Cloudinary cloudinary = new Cloudinary(account);

            string path = AppDomain.CurrentDomain.BaseDirectory;
            String dir = Path.GetDirectoryName(path);
            dir += "\\images";
            string tmp_image_path = dir + "\\tmp" + generate_unique_id() + ".jpeg";
            var bytes = Convert.FromBase64String(base64_img.Replace("data:image/jpeg;base64,",""));
            using (var imageFile = new FileStream(tmp_image_path, FileMode.Create, FileAccess.ReadWrite))
            {
                imageFile.Write(bytes, 0, bytes.Length);
                imageFile.Flush();
            }

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(@tmp_image_path)
            };
            var uploadResult = cloudinary.Upload(uploadParams);
            if (uploadResult.StatusCode.ToString().Equals("OK"))
            {
                File.Delete(tmp_image_path);
            }
            return uploadResult.SecureUri.ToString(); ;
        }
        

        public bool SendEmail(string UserAddress, string UserPassword, string emailTo, string subject, string body, bool isBodyHtml)
        {
            bool mailSent;
            ServerCreds server = new ServerCreds();
           
            MailMessage email = new MailMessage();
            email.To.Add(emailTo);
            email.From = new MailAddress(UserAddress);
            email.Subject = subject;
            email.Body = body;
            email.IsBodyHtml = isBodyHtml;

            SmtpClient mailClient = new SmtpClient();
            mailClient.Host = server.host;
            mailClient.Credentials = new System.Net.NetworkCredential(UserAddress, UserPassword);
            mailClient.EnableSsl = true;
            try
            {
                mailClient.Send(email);
                mailSent = true;
            }
            catch
            {
                mailSent = false;
            }
            return mailSent;
        }

        public bool VerifyUID(string id, bool isverified, string comment)
        {
            bool data_updated; 
            AdminCreds admin = new AdminCreds();

            string connectionconfig = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionconfig);
            con.Open();

            string user_email = "";
            string user_given_name = "";

            SqlCommand cmd = new SqlCommand("SELECT given_name,email from register WHERE uid=@uid", con);
            cmd.Parameters.AddWithValue("@uid", id);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                // Check is the reader has any rows at all before starting to read.
                if (reader.HasRows)
                {
                    // Read advances to the next row.
                    while (reader.Read())
                    {
                        user_email = reader.GetString(reader.GetOrdinal("email"));
                        user_given_name = reader.GetString(reader.GetOrdinal("given_name"));
                    }
                }
            }

            
            if (isverified) {

                cmd = new SqlCommand("UPDATE register SET uid_verified = @isverified WHERE uid = @uid", con);
                cmd.Parameters.AddWithValue("@isverified", isverified);
                cmd.Parameters.AddWithValue("@uid", id);
                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    string emailbody = "<table align='center' cellspacing='0' style='border:none!important;margin: none !important;' width='auto'>" +
                            "<tr><td colspan='2' bgcolor='#016648' style='text-align: center; padding-top: 10px;'> <img style='border: 2px solid white; border-radius: 5px; width: 130px;' src='https://cdn0.vox-cdn.com/thumbor/nxgcGlGlYmGUiZZzGWDm2nve4Bk=/0x0:547x365/1310x873/cdn0.vox-cdn.com/uploads/chorus_image/image/33573117/Rocky_The_Bull.0.jpg'></td></tr>" +
                            "<tr><td colspan='2' bgcolor='#016648' style='color: white; font-size: 300%; padding: 10px; text-align:center;'><span style='width: 100%;'>Bulls Allowed to Booze</span></td></tr>" +
                            "<tr><td style='padding: 5px;'> Hello "+ user_given_name + ", </td><td style='padding: 5px; text-align: center;'> </td></tr>" +
                            "<tr><td style='padding: 5px;'> </td><td style='padding: 5px; text-align: center;'> </td></tr>" +
                            "<tr><td style='padding: 5px;'> You have been Successfully Registered and Verified as a Bull allowed to Booze.</td><td style='padding: 5px; text-align: center;'> </td></tr>" +
                            "<tr><td style='padding: 5px;'> </td><td style='padding: 5px; text-align: center;'> </td></tr>" +
                            "<tr><td style='padding: 5px;'> </td><td style='padding: 5px; text-align: center;'> </td></tr>" +
                            "<tr><td style='padding: 5px;'> </td><td style='padding: 5px; text-align: center;'> </td></tr>" +
                            "<tr><td style='padding: 5px;'>Thanks,</td><td style='padding: 5px; text-align: center;'> </td></tr>" +
                            "<tr><td style='padding: 5px;'>Team Bulls Allowed to Booze</td><td style='padding: 5px; text-align: center;'> </td></tr>" +
                            "<tr><td colspan='2' bgcolor='#016648' style='padding:5px;'></td></tr>" +
                    "</table> ";
                    SendEmail(admin.email, admin.password, user_email, "Registration Successfull!", emailbody, true);
                    data_updated = true;
                }
                else
                {
                    data_updated = false;
                }
            }
            else
            {
                cmd = new SqlCommand("Delete from register WHERE uid=@uid", con);
                cmd.Parameters.AddWithValue("@uid", id);
                if(cmd.ExecuteNonQuery() == 1)
                {
                    string emailbody = "<table align='center' cellspacing='0' style='border:none!important;margin: none !important;' width='auto'>" +
                            "<tr><td colspan='2' bgcolor='#016648' style='text-align: center; padding-top: 10px;'> <img style='border: 2px solid white; border-radius: 5px; width: 130px;' src='https://cdn0.vox-cdn.com/thumbor/nxgcGlGlYmGUiZZzGWDm2nve4Bk=/0x0:547x365/1310x873/cdn0.vox-cdn.com/uploads/chorus_image/image/33573117/Rocky_The_Bull.0.jpg'></td></tr>" +
                            "<tr><td colspan='2' bgcolor='#016648' style='color: white; font-size: 300%; padding: 10px; text-align:center; '><span style='width: 100%;'>Bulls Allowed to Booze</span></td></tr>" +
                            "<tr><td style='padding: 5px;'> Hello Gopalkirshna, </td><td style='padding: 5px; text-align: center;'> </td></tr>" +
                            "<tr><td style='padding: 5px;'> </td><td style='padding: 5px; text-align: center;'> </td></tr>" +
                            "<tr><td style='padding: 5px;'> Your Registeration to Bulls Allowed to booze has failed.</td><td style='padding: 5px; text-align: center;'> </td></tr>" +
                            "<tr><td style='padding: 5px;'> <h4>Reason: " + comment + "</h4></td><td style='padding: 5px; text-align: center;'> </td></tr>" +
                            "<tr><td style='padding: 5px;'> </td><td style='padding: 5px; text-align: center;'> </td></tr>" +
                            "<tr><td style='padding: 5px;'> Please register again with the changes as per the reason.</td><td style='padding: 5px; text-align: center;'> </td></tr>" +
                            "<tr><td style='padding: 5px;'> </td><td style='padding: 5px; text-align: center;'> </td></tr>" +
                            "<tr><td style='padding: 5px;'> </td><td style='padding: 5px; text-align: center;'> </td></tr>" +
                            "<tr><td style='padding: 5px;'>Thanks,</td><td style='padding: 5px; text-align: center;'> </td></tr>" +
                            "<tr><td style='padding: 5px;'>Team Bulls Allowed to Booze</td><td style='padding: 5px; text-align: center;'> </td></tr>" +
                            "<tr><td colspan='2' bgcolor='#016648' style='padding:5px;'></td></tr>" +
                    "</table> ";
                    SendEmail(admin.email, admin.password, user_email, "Registration failed!", emailbody, true);
                    data_updated = true;
                }
                else
                {
                    data_updated = false;
                }
            }
            
            con.Close();
            return data_updated;
        }

        public string CheckUID(string id)
        {
            string connectionconfig = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionconfig);
            con.Open();
            
            SqlCommand cmd = new SqlCommand("SELECT * from register WHERE uid=@uid", con);
            cmd.Parameters.AddWithValue("@uid", id);
            SqlDataReader reader = cmd.ExecuteReader();
            



            if (reader.HasRows)
            {
                Dictionary<string, object> user_obj = null;
                while (reader.Read())
                {
                   user_obj = Enumerable.Range(0, reader.FieldCount).ToDictionary(reader.GetName, reader.GetValue);
                }
                DateTime user_age = (DateTime)user_obj["date_of_birth_obj"];

                user_age = user_age.AddYears(21);
                DateTime dt_now = DateTime.Now;
                if(dt_now < user_age)
                {
                    user_obj["is_legal"] = false;
                }
                else
                {
                    user_obj["is_legal"] = true;
                }

                return JsonConvert.SerializeObject(user_obj);
            }
            return "";
        }



    }
}
