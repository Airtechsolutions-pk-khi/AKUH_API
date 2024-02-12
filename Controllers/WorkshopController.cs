using AKUH_API.Models;
using AKUH_API.Repositories;
using Microsoft.AspNetCore.Mvc;
 

namespace AKUH_API.Controllers
{     
    [ApiController]
    [Route("[Controller]")]
    public class WorkshopController : ControllerBase
    {
        WorkshopRepository _workshopRepo;

        public WorkshopController()
        {
            _workshopRepo = new WorkshopRepository();
        }
        [HttpGet]
        [Route("All")]
        public async Task<RspWorkshop> GetAll()
        {
            var data = _workshopRepo.GetAllWorkshop();
            return await data;
        }

    }
}