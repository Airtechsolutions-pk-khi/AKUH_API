using AKUH_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;
using WebAPICode.Helpers;

namespace AKUH_API.Repositories
{
    public class MessageRepository
    {
        public static DataTable _dt;
        public static DataSet _ds;

        public MessageRepository()
           : base()
        {

            _dt = new DataTable();
            _ds = new DataSet();
        }
        public async Task<RspMsg> GetAllMsgs()
        {
            var RspMsg = new RspMsg();
            var repo = new List<MessageBLL>();
            try
            {
                SqlParameter[] p = new SqlParameter[0];
                
                _dt = await (new DBHelper().GetTableFromSPAsync)("sp_GetAllMsgs_API", p);

                if (_dt.Rows.Count > 0)
                {
                    repo = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<MessageBLL>>().ToList();
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
                    RspMsg rspMsg = new RspMsg()
                    {
                        description = "Success.",
                        status = 200,
                        message = repo
                    };
                    return rspMsg;
                }
                else
                {
                    RspMsg rspMsg = new RspMsg()
                    {
                        description = "Speakers not found.",
                        status = 0,
                        message = null
                    };
                    return rspMsg;
                }
            }
            catch (Exception ex)
            {
                RspMsg rspMsg = new RspMsg()
                {
                    description = "Something went wrong.",
                    status = 0,
                    message = null
                };
                return rspMsg;
            }
        }
    }
    
    
}
