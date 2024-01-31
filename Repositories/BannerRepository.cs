using AKUH_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;
using WebAPICode.Helpers;

namespace AKUH_API.Repositories
{
    
    public class BannerRepository
    {
        
        public static DataTable _dt;
        public static DataSet _ds;

        public BannerRepository()
           : base()
        {

            _dt = new DataTable();
            _ds = new DataSet();
           
        }
        public async Task<RspBanner> GetAllBanner()
        {
            
            var RspBanner = new RspBanner();
            var repo = new List<BannerBLL>();
            try
            {
                SqlParameter[] p = new SqlParameter[0];
                
                _dt = await (new DBHelper().GetTableFromSPAsync)("sp_GetAllBanner_API", p);

                if (_dt.Rows.Count > 0)
                {
                    repo = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<BannerBLL>>().ToList();

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
                   


                    RspBanner rspBanner = new RspBanner()
                    {
                        description = "Success.",
                        status = 200,
                        banner = repo
                    };
                    return rspBanner;
                }
                else
                {
                    RspBanner rspBanner = new RspBanner()
                    {
                        description = "Banners not found.",
                        status = 0,
                        banner = null
                    };
                    return rspBanner;
                }
            }
            catch (Exception ex)
            {
                RspBanner rspBanner = new RspBanner()
                {
                    description = "Something went wrong.",
                    status = 0,
                    banner = null
                };
                return rspBanner;
            }
        }

        public async Task<RspBannerPopup> BannerPopup()
        {

            var RspBannerPopup = new RspBannerPopup();
            var repo = new List<BannerPopupBLL>();
            try
            {
                SqlParameter[] p = new SqlParameter[0];

                _dt = await (new DBHelper().GetTableFromSPAsync)("sp_GetAllBannerPopup_API", p);

                if (_dt.Rows.Count > 0)
                {
                    repo = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_dt)).ToObject<List<BannerPopupBLL>>().ToList();

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



                    RspBannerPopup rspBannerPopup = new RspBannerPopup()
                    {
                        description = "Success.",
                        status = 200,
                        bannerPopup = repo
                    };
                    return rspBannerPopup;
                }
                else
                {
                    RspBannerPopup rspBannerPopup = new RspBannerPopup()
                    {
                        description = "Banners not found.",
                        status = 0,
                        bannerPopup = null
                    };
                    return rspBannerPopup;
                }
            }
            catch (Exception ex)
            {
                RspBannerPopup rspBannerPopup = new RspBannerPopup()
                {
                    description = "Something went wrong.",
                    status = 0,
                    bannerPopup = null
                };
                return rspBannerPopup;
            }
        }
    }
    
    
}
