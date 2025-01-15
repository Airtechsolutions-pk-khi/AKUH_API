using AKUH_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json.Linq;
using QRCoder;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Net.Mail;
using System.Security.Cryptography;
using WebAPICode.Helpers;
using ZXing.QrCode.Internal;
using static QRCoder.PayloadGenerator;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AKUH_API.Repositories
{
    public class EventRepository :BaseRepository
    {
        
        public static DataTable _dt;
        public static DataSet _ds;

        public EventRepository()
           : base()
        {
             
            _dt = new DataTable();
            _ds = new DataSet();
        }
        public async Task<RspEvent> GetAllEvents()
        {
            var lstEvent = new List<EventBLL>();
            var lstEventCategory = new List<EventCategoryBLL>();
            var lstOrganizer = new List<OrganizerBLL>();
            var lstSpeaker = new List<SpeakerBLL>();
            var lstImg = new List<EventImageBLL>();
            var rspEvent = new RspEvent();
            try
            {
                var ds = await GetEvents();
                var _dsEventCategory = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0])).ToObject<List<EventCategoryBLL>>();
                var _dsEvent = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[1])).ToObject<List<EventBLL>>();
                var _dsSpeaker = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[2])).ToObject<List<SpeakerBLL>>();
                var _dsOrganizer = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[3])).ToObject<List<OrganizerBLL>>();
                var _dsImgList = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[4])).ToObject<List<EventImageBLL>>();


                foreach (var i in _dsEventCategory)
                {
                    lstEvent = new List<EventBLL>();
                    
                    foreach (var j in _dsEvent.Where(x => x.EventCategoryID == i.EventCategoryID))
                    {
                        lstSpeaker = new List<SpeakerBLL>();
                        foreach (var k in _dsSpeaker.Where(k => k.EventID == j.EventID))
                        {
                            lstSpeaker.Add(new SpeakerBLL
                            {
                                SpeakerID = k.SpeakerID,
                                StatusID = k.StatusID,
                                Name = k.Name,
                                Designation = k.Designation,
                                Company = k.Company,
                                About = k.About,
                                Type = k.Type,
                                Image = "http://akuapp-001-site2.mysitepanel.net/" + k.Image,
                                Createdon = k.Createdon

                            });
                        }
                        lstOrganizer = new List<OrganizerBLL>();
                        foreach (var l in _dsOrganizer.Where(l => l.EventID == j.EventID))
                        {
                            lstOrganizer.Add(new OrganizerBLL
                            {
                                OrganizerID = l.OrganizerID,
                                StatusID = l.StatusID,
                                Name = l.Name,
                                Description = l.Description,
                                Image = "http://akuapp-001-site2.mysitepanel.net/" + l.Image,
                                Createdon = l.Createdon
                            });
                        }
                        lstImg = new List<EventImageBLL>();
                        foreach (var m in _dsImgList.Where(m => m.EventID == j.EventID))
                        {
                            lstImg.Add(new EventImageBLL
                            {
                                EventID = m.EventID,
                                Image = "http://akuapp-001-site2.mysitepanel.net/" + m.Image,                                
                            });
                        }
                        //string eventDate = j.EventDate.ToString("dd-MM-yyyy");
                        lstEvent.Add(new EventBLL
                        {
                            EventID = j.EventID,
                            StatusID = j.StatusID,
                            Name = j.Name,
                            Type = j.Type,
                            Description = j.Description,
                            FromDate =  Convert.ToDateTime(j.FromDate).ToString("dd-MM-yyyy"),
                            Image = "http://akuapp-001-site2.mysitepanel.net/" + j.Image,
                            ToDate =  Convert.ToDateTime(j.ToDate).ToString("dd-MM-yyyy"),
                            EventDate = Convert.ToDateTime(j.EventDate).ToString("dd-MM-yyyy"), 
                            EventTime = j.EventTime,
                            EventCity = j.EventCity,
                            LocationLink = j.LocationLink,
                            Location = j.Location,
                            EventEndTime = j.EventEndTime,
                            PhoneNo = j.PhoneNo,
                            Email = j.Email,
                            RemainingTicket = j.RemainingTicket,
                            Facebook = j.Facebook,
                            Instagram = j.Instagram,
                            IsFeatured = j.IsFeatured,
                            Createdon = j.Createdon,
                            Updatedon = j.Updatedon,
                            EventLink = j.EventLink,
                            Speakers = lstSpeaker,
                            Organizers = lstOrganizer,
                            ImgList = lstImg
                        });
                        
                    }
                    lstEventCategory.Add(new EventCategoryBLL
                    {
                        EventCategoryID = i.EventCategoryID,
                        Name = i.Name,
                        Description = i.Description,
                        StatusID = i.StatusID,
                        Createdon = i.Createdon,
                        Image = "http://akuapp-001-site2.mysitepanel.net/" + i.Image,
                        events = lstEvent,

                    });
                }
                rspEvent.eventcategories = lstEventCategory;
                rspEvent.status = 1;
                rspEvent.description = "Success";

                return rspEvent;
                
            }
             catch (Exception ex)
            {
                rspEvent.eventcategories = new List<EventCategoryBLL>();
                rspEvent.status = 0;
                rspEvent.description = "Event Not Found.";
                return rspEvent;
            }
        }
        public async Task<RspMyEvent> MyEvents(int UserID)
        {
            var lstMyEvent = new List<MyEventBLL>();             
           // var lstImg = new List<EventImageBLL>();
            var rspEvent = new RspMyEvent();
            try
            {
                var ds = await GetEventByUserID(UserID);               
                var _dsMyEvent = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0])).ToObject<List<MyEventBLL>>();               
                //var _dsImgList = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[1])).ToObject<List<EventImageBLL>>();

                foreach (var i in _dsMyEvent)
                {
                    lstMyEvent = new List<MyEventBLL>();

                    foreach (var j in _dsMyEvent.Where(x => x.UserID == i.UserID))
                    {
                         
                        //lstImg = new List<EventImageBLL>();
                        //foreach (var m in _dsImgList.Where(m => m.EventID == j.EventID))
                        //{
                        //    lstImg.Add(new EventImageBLL
                        //    {
                        //        EventID = m.EventID,
                        //        Image = "http://akuapp-001-site2.mysitepanel.net/" + m.Image,
                        //    });
                        //}
                         
                        lstMyEvent.Add(new MyEventBLL
                        {
                            EventID = j.EventID,
                            UserID = j.UserID,
                            AttendeesID = j.AttendeesID,
                            StatusID = j.StatusID,
                            FullName = j.FullName,
                            Email = j.Email,
                            PhoneNo = j.PhoneNo,
                            FromDate = Convert.ToDateTime(j.FromDate).ToString("dd-MM-yyyy"),
                            Image = "http://akuapp-001-site2.mysitepanel.net/" + j.Image,                                                        
                            EventTime = j.EventTime,
                            EventName = j.EventName,  
                            MeetingLink = j.MeetingLink,
                            Subject = j.Subject,
                            MessageForAttendee  = j.MessageForAttendee
                            
                        });

                    }
                     
                }
                rspEvent.myEvent = lstMyEvent;
                rspEvent.status = 1;
                rspEvent.description = "Success";

                return rspEvent;

            }
            catch (Exception ex)
            {
                rspEvent.myEvent = new List<MyEventBLL>();
                rspEvent.status = 0;
                rspEvent.description = "Event Not Found.";
                return rspEvent;
            }
        }
        public async Task<DataSet> GetEvents()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] p = new SqlParameter[0];

                ds = await (new DBHelper().GetDatasetFromSPAsync)("sp_GetAllEvents_API", p);
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<DataSet> GetEventByUserID(int UserID)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@UserID", UserID);
                ds = await (new DBHelper().GetDatasetFromSPAsync)("sp_GetMyEvents_API", p);
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> RegisterEvent(AttendeesBLL? attendees)
        {
            
            int result = 0;
            try
            {
                SqlParameter[] p1 = new SqlParameter[2];
                p1[0] = new SqlParameter("@EventID", attendees.EventID);
                p1[1] = new SqlParameter("@UserID", attendees.UserID);
                _dt = await (new DBHelper().GetTableFromSPAsync)("sp_CheckEvents_API", p1);
                if (_dt.Rows.Count == 0) 
                {
                    SqlParameter[] p = new SqlParameter[10];
                    p[0] = new SqlParameter("@EventID", attendees.EventID);
                    p[1] = new SqlParameter("@UserID", attendees.UserID);
                    p[2] = new SqlParameter("@FullName", attendees.FullName);
                    p[3] = new SqlParameter("@Email", attendees.Email);
                    p[4] = new SqlParameter("@PhoneNo", attendees.PhoneNo);
                    p[5] = new SqlParameter("@Occupation", attendees.Occupation);
                    p[6] = new SqlParameter("@Gender", attendees.Gender);
                    p[7] = new SqlParameter("@StatusID", 1);
                    p[8] = new SqlParameter("@Createdon", DateTime.UtcNow.AddMinutes(300));
                    p[9] = new SqlParameter("@UpdatedBy", attendees.UserID);

                    result = Convert.ToInt32(new DBHelper().GetTableFromSP("sp_EventRegister_API", p).Rows[0]["AttendeesID"]);
                }
                else
                {
                    return 0;
                }
                 
            }
            catch (Exception ex)
            {
                return 0;
            }
            return result;
        }
        public int UploadSS(AttendeesBLL? attendees, IWebHostEnvironment _env)
        {
            try
            {
                attendees.ImageSS = UploadImage(attendees.ImageSS, "Screenshot", _env);
                
                var result =  UpdateScreenShotData(attendees);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public  int UpdateScreenShotData(AttendeesBLL? attendees)
        {
            int result = 0;
            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@UserID", attendees.UserID);
                p[1] = new SqlParameter("@ImageSS", attendees.ImageSS);
                
                result =  (new DBHelper().ExecuteNonQueryReturn)("sp_UpdateAttendeesSS_API", p);
                result = Convert.ToInt32(new DBHelper().GetTableFromSP("sp_UpdateAttendeesSS_API", p).Rows[0]["AttendeesID"]);
                if (result > 0)
                {
                    return 2;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return result;
        }
        public async Task<int> UpdateAttendees(AttendeesUpdtBLL? attendees)
        {
            int result = 0;
            try
            {
                if (attendees.AttendeesID != null || attendees.AttendeesID == 0)
                {
                    SqlParameter[] p1 = new SqlParameter[1];
                    p1[0] = new SqlParameter("@AttendeesID", attendees.AttendeesID);
                    DataTable resultTable = new DBHelper().GetTableFromSP("sp_ChkAttendees_API", p1);

                    if (resultTable.Rows.Count > 0)
                    {
                        return 2;
                    }
                    else
                    {
                        SqlParameter[] p = new SqlParameter[4];

                        p[0] = new SqlParameter("@FullName", attendees.FullName);
                        p[1] = new SqlParameter("@Email", attendees.Email);
                        p[2] = new SqlParameter("@AttendeesID", attendees.AttendeesID);
                        p[3] = new SqlParameter("@Updatedon", DateTime.UtcNow.AddMinutes(300));

                        result = await (new DBHelper().ExecuteNonQueryReturnAsync)("sp_UpdateAttendees_API", p);
                        
                    }

                }
                //if (attendees.AttendeesID != null || attendees.AttendeesID == 0)
                //{
                //    SqlParameter[] p = new SqlParameter[4];

                //    p[0] = new SqlParameter("@FullName", attendees.FullName);
                //    p[1] = new SqlParameter("@Email", attendees.Email);
                //    p[2] = new SqlParameter("@AttendeesID", attendees.AttendeesID);
                //    p[3] = new SqlParameter("@Updatedon", DateTime.UtcNow.AddMinutes(300));

                //    result = await (new DBHelper().ExecuteNonQueryReturnAsync)("sp_UpdateAttendees_API", p);
                //}
            }
            catch (Exception ex)
            {
                return 0;
            }
            return result;
        }

        public EventAttendeesEmailBLL GetAllDataForEmail(int attendeesID)
        {
            try
            {
                var lst = new EventAttendeesEmailBLL();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@AttendeesID", attendeesID);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetAttendeesDataForEmail_API", p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<EventAttendeesEmailBLL>>().FirstOrDefault();
                    }
                }

                return lst;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }


}
