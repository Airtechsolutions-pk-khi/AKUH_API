using AKUH_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;
using WebAPICode.Helpers;

namespace AKUH_API.Repositories
{
    public class OrganisingCommitteeRepository
    {
        public static DataTable _dt;
        public static DataSet _ds;

        public OrganisingCommitteeRepository()
           : base()
        {

            _dt = new DataTable();
            _ds = new DataSet();
        }
        public async Task<RspOrganisingCommittee> GetAll()
        {
            var RspOrganisingCommittee = new RspOrganisingCommittee();
            var repo = new List<OrganisingCommitteeBLL>();
            try
            {
                SqlParameter[] p = new SqlParameter[0];
                
                _dt = await (new DBHelper().GetTableFromSPAsync)("sp_GetAllOrganisingCommittee_API", p);

                if (_dt.Rows.Count > 0)
                {
                    repo = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<OrganisingCommitteeBLL>>().ToList();
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
                    RspOrganisingCommittee rspOrganisingCommittee = new RspOrganisingCommittee()
                    {
                        description = "Success.",
                        status = 200,
                        OrganisingCommittee = repo
                    };
                    return rspOrganisingCommittee;
                }
                else
                {
                    RspOrganisingCommittee rspOrganisingCommittee = new RspOrganisingCommittee()
                    {
                        description = "Speakers not found.",
                        status = 0,
                        OrganisingCommittee = null
                    };
                    return rspOrganisingCommittee;
                }
            }
            catch (Exception ex)
            {
                RspOrganisingCommittee rspOrganisingCommittee = new RspOrganisingCommittee()
                {
                    description = "Something went wrong.",
                    status = 0,
                    OrganisingCommittee = null
                };
                return rspOrganisingCommittee;
            }
        }
    }
    
    
}
