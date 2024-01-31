using AKUH_API.Models;
using AKUH_API.Repositories;
using Microsoft.AspNetCore.Mvc;
 

namespace AKUH_API.Controllers
{     
    [ApiController]
    [Route("[Controller]")]
    public class BannerController : ControllerBase
    {
        BannerRepository _bannerRepo;
        
        public BannerController()
        {
            _bannerRepo = new BannerRepository();
            
        }
        [HttpGet]
        [Route("ALL")]
        public async Task<RspBanner> AllBanner()
        {
            var data = _bannerRepo.GetAllBanner();
            return await data;
        }
        [HttpGet]
        [Route("BannerPopup")]
        public async Task<RspBannerPopup> BannerPopup()
        {
            var data = _bannerRepo.BannerPopup();
            return await data;
        }
    }
}