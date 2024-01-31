using AKUH_API.Models;
using AKUH_API.Repositories;
using Microsoft.AspNetCore.Mvc;
 

namespace AKUH_API.Controllers
{     
    [ApiController]
    [Route("[Controller]")]
    public class SettingController : ControllerBase
    {
        SettingRepository _settingRepo;

        public SettingController()
        {
            _settingRepo = new SettingRepository();
        }
        [HttpGet]
        [Route("All")]
        public async Task<RspSetting> GetSettings()
        {
            var data = _settingRepo.GetAllSettings();
            return await data;
        }

    }
}