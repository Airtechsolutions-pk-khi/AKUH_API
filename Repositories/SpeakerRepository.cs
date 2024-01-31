using AKUH_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;
using WebAPICode.Helpers;

namespace AKUH_API.Repositories
{
    public class SpeakerRepository
    {
        public static DataTable _dt;
        public static DataSet _ds;

        public SpeakerRepository()
           : base()
        {

            _dt = new DataTable();
            _ds = new DataSet();
        }
        public async Task<RspSpeaker> GetAllSpeakers()
        {
            var RspSpeaker = new RspSpeaker();
            var repo = new List<SpeakerBLL>();
            try
            {
                SqlParameter[] p = new SqlParameter[0];
                
                _dt = await (new DBHelper().GetTableFromSPAsync)("sp_GetAllSpeaker_API", p);

                if (_dt.Rows.Count > 0)
                {
                    repo = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<SpeakerBLL>>().ToList();
                    foreach (var item in repo)
                    {
                        if (item.Image != null && item.Image != "")
                        {
                            item.Image = "http://akuapp-001-site2.mysitepanel.net/" + item.Image;
                        }
                        else
                        {
                            item.Image = "";

                        }
                    }
                    RspSpeaker rspSpeaker = new RspSpeaker()
                    {
                        description = "Success.",
                        status = 200,
                        speaker = repo
                    };
                    return rspSpeaker;
                }
                else
                {
                    RspSpeaker rspSpeaker = new RspSpeaker()
                    {
                        description = "Speakers not found.",
                        status = 0,
                        speaker = null
                    };
                    return rspSpeaker;
                }
            }
            catch (Exception ex)
            {
                RspSpeaker rspSpeaker = new RspSpeaker()
                {
                    description = "Something went wrong.",
                    status = 0,
                    speaker = null
                };
                return rspSpeaker;
            }
        }
    }
    
    
}
