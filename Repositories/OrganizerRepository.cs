using AKUH_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;
using WebAPICode.Helpers;

namespace AKUH_API.Repositories
{
    public class OrganizerRepository
    {
        public static DataTable _dt;
        public static DataSet _ds;

        public OrganizerRepository()
           : base()
        {

            _dt = new DataTable();
            _ds = new DataSet();
        }
        public async Task<RspOrganizer> GetAllOrganizer()
        {
            var RspOrganizer = new RspOrganizer();
            var repo = new List<OrganizerBLL>();
            try
            {
                SqlParameter[] p = new SqlParameter[0];
                
                _dt = await (new DBHelper().GetTableFromSPAsync)("sp_GetAllOrganizer_API", p);

                if (_dt.Rows.Count > 0)
                {
                    repo = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<OrganizerBLL>>().ToList();

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
                    RspOrganizer rspOrganizer = new RspOrganizer()
                    {
                        description = "Success.",
                        status = 200,
                        organizer = repo
                    };
                    return rspOrganizer;
                }
                else
                {
                    RspOrganizer rspOrganizer = new RspOrganizer()
                    {
                        description = "Organizer not found.",
                        status = 0,
                        organizer = null
                    };
                    return rspOrganizer;
                }
            }
            catch (Exception ex)
            {
                RspOrganizer rspOrganizer = new RspOrganizer()
                {
                    description = "Something went wrong.",
                    status = 0,
                    organizer = null
                };
                return rspOrganizer;
            }
        }
    }
    
    
}
