using AKUH_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;
using WebAPICode.Helpers;

namespace AKUH_API.Repositories
{
    public class LoginRepository 
    {
        public static DataTable _dt;
        public static DataSet _ds;

        public LoginRepository()
           : base()
        {

            _dt = new DataTable();
            _ds = new DataSet();
        }
        public async Task<RspLogin> GetCustomerInfo(string email, string password)
        {
            var RspLogin = new RspLogin();
            var repo = new UserBLL();
            try
            {
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@email", email);
                p[1] = new SqlParameter("@password", password);

                // Assuming GetDatasetFromSP is an asynchronous method
                _dt = await (new DBHelper().GetTableFromSPAsync)("sp_AuthenticateUser_admin", p);

                if (_dt.Rows.Count > 0)
                {
                    repo = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<UserBLL>>().FirstOrDefault();

                    RspLogin rspLogin = new RspLogin()
                    {
                        description = "Login Success.",
                        status = 200,
                        login = repo
                    };
                    return rspLogin;
                }
                else
                {
                    RspLogin rspLogin = new RspLogin()
                    {
                        description = "Incorrect Email or Password.",
                        status = 0,
                        login = null
                    };
                    return rspLogin;
                }
            }
            catch (Exception ex)
            {
                RspLogin rspLogin = new RspLogin()
                {
                    description = "Incorrect Email or Password.",
                    status = 0,
                    login = null
                };
                return rspLogin;
            }
        }
    }
    
    
}
