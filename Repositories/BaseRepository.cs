using System.Text;
using System.Net;
using System.Net.Mail;
using AKUH_API.Models;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using WebAPICode.Helpers;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace AKUH_API.Repositories
{
    public class BaseRepository
    {
       
        StreamWriter _sw;
        public static DataTable _dt;
        public static DataSet _ds;
        public BaseRepository()
        {
            
            _dt = new DataTable();
            _ds = new DataSet();
        }
        public void ErrorLog(Exception e, string FnName, string FileName)
        {
            try
            {


                //ErrorLog1 Log = new ErrorLog1();
                //Log.Errorin = FnName + " : " + e.InnerException;
                //Log.ErrorMessage = e.Message;
                //Log.CreatedDate = DateTime.UtcNow;
                ////Log.UserID = 2;
                ////Log.CreatedBy= 2;
                //DBContext.ErrorLogs1.Attach(Log);
                //DBContext.ErrorLogs1.Add(Log);
                //DBContext.SaveChanges();
                ////function
                //LogWrite(Log.ErrorMessage, FileName);
            }
            catch
            {
            }
        }
        public void LogWrite(string msg, string fileName)
        {
            //var logPath = ConfigurationManager.AppSettings["LogPath"];
            //_sw = new StreamWriter(@logPath + fileName + DateTime.UtcNow.ToString("yyyyMMdd") + ".txt", true);

            _sw.WriteLine(DateTime.UtcNow.ToLongTimeString() + " " + msg);
            _sw.Close();
        }

        //public string CurrentDate(SessionInfo session)
        //{
        //    #region timmings

        //    DateTime t1 = DateTime.UtcNow.AddMinutes(session.UTC);
        //    DateTime t2 = Convert.ToDateTime(session.OpenTime.ToString());
        //    DateTime t3 = Convert.ToDateTime(session.CloseTime.ToString());

        //    string startday;

        //    TimeSpan diff = t3 - t2;

        //    DateTime NewDate = t2.AddHours(diff.Hours <= 0 ? (24 - (-diff.Hours)) : diff.Hours);

        //    if (t3.Date != NewDate.Date)
        //    {
        //        int b = DateTime.Compare(t1, t3);

        //        if (b == 1)
        //        {
        //            startday = DateTime.Today.ToString("yyyy-MM-dd hh:mm:ss");
        //        }
        //        else
        //        {
        //            startday = DateTime.Today.AddDays(-1).ToString();
        //        }
        //    }
        //    else
        //    {
        //        startday = DateTime.Today.ToString("yyyy-MM-dd hh:mm:ss");
        //    }
        //    return startday;
        //    #endregion timmings 
        //}


        public string DateFormat(string Date)
        {
            if (Date != "")
                return Convert.ToDateTime(Date).ToString("yyyy-MM-dd hh:mm:ss");
            else
                return "";
        }

        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        public void Email(string _SubjectEmail, string _BodyEmail, string _To, string FromAddress, string Password, string SMTP, int Port)
        {
            try
            {
                using (System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage())
                {
                    mail.From = new MailAddress(FromAddress, "MarnPos");
                    mail.To.Add(_To);
                    mail.Subject = _SubjectEmail;
                    mail.Body = _BodyEmail;
                    mail.IsBodyHtml = true;
                    using (SmtpClient smtp = new SmtpClient(SMTP, Port))
                    {
                        smtp.Credentials = new NetworkCredential(FromAddress, Password);
                        smtp.EnableSsl = false;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
       
        public void PushNotificationAndroid(PushNoticationBLL obj)
        {
            try
            {
                var applicationID = "AAAA_pHx1WA:APA91bHZZdZb6r-0IfnisWfw81-ovD3oAbAqJr6kEFKQJaf_YsKkT8x69lgiRKTJd50LgziVABJmpC_rm6L8OmVZvM9b63_1heNeMJbllrUaCdqQLOG0trQ2pWe9lT2Ri4cmHDmiOg6j";
                var senderId = "1093370238304";

                string deviceId = obj.DeviceID;
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";

                var data = new
                {
                    to = deviceId,
                    notification = new
                    {
                        body = obj.Message,
                        title = obj.Title,
                        icon = "myicon",
                        sound = "default"
                    }
                };

                var jsonString = JsonSerializer.Serialize(data);
                byte[] byteArray = Encoding.UTF8.GetBytes(jsonString);

                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                string str = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
       
    }
}
