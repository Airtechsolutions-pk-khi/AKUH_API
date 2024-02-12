using AKUH_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;
using WebAPICode.Helpers;

namespace AKUH_API.Repositories
{
    public class WorkshopRepository
    {
        public static DataTable _dt;
        public static DataSet _ds;

        public WorkshopRepository()
           : base()
        {

            _dt = new DataTable();
            _ds = new DataSet();
        }
        public async Task<RspWorkshop> GetAllWorkshop()
        {
            var RspWorkshop = new RspWorkshop();
            var repo = new List<WorkshopBLL>();
            try
            {
                SqlParameter[] p = new SqlParameter[0];
                
                _dt = await (new DBHelper().GetTableFromSPAsync)("sp_GetAllWorkshop_API", p);

                if (_dt.Rows.Count > 0)
                {
                    repo = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<WorkshopBLL>>().ToList();
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
                    RspWorkshop rspWorkshop = new RspWorkshop()
                    {
                        description = "Success.",
                        status = 200,
                        workshop = repo
                    };
                    return rspWorkshop;
                }
                else
                {
                    RspWorkshop rspWorkshop = new RspWorkshop()
                    {
                        description = "Speakers not found.",
                        status = 0,
                        workshop = null
                    };
                    return rspWorkshop;
                }
            }
            catch (Exception ex)
            {
                RspWorkshop rspWorkshop = new RspWorkshop()
                {
                    description = "Something went wrong.",
                    status = 0,
                    workshop = null
                };
                return rspWorkshop;
            }
        }
    }
    
    
}
