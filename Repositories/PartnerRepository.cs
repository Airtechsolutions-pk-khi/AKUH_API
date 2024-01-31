using AKUH_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;
using WebAPICode.Helpers;

namespace AKUH_API.Repositories
{
    public class PartnerRepository
    {
        public static DataTable _dt;
        public static DataSet _ds;

        public PartnerRepository()
           : base()
        {

            _dt = new DataTable();
            _ds = new DataSet();
        }
        public async Task<RspPartner> GetAllPartner()
        {
            var RspPartner = new RspPartner();
            var repo = new List<PartnerBLL>();
            try
            {
                SqlParameter[] p = new SqlParameter[0];
                
                _dt = await (new DBHelper().GetTableFromSPAsync)("sp_GetAllPartner_API", p);

                if (_dt.Rows.Count > 0)
                {
                    repo = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<PartnerBLL>>().ToList();
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
                    RspPartner rspPartner = new RspPartner()
                    {
                        description = "Success.",
                        status = 200,
                        partner = repo
                    };
                    return rspPartner;
                }
                else
                {
                    RspPartner rspPartner = new RspPartner()
                    {
                        description = "Partners not found.",
                        status = 0,
                        partner = null
                    };
                    return rspPartner;
                }
            }
            catch (Exception ex)
            {
                RspPartner rspPartner = new RspPartner()
                {
                    description = "Something went wrong.",
                    status = 0,
                    partner = null
                };
                return rspPartner;
            }
        }
    }
    
    
}
