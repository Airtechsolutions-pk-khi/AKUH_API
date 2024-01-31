using AKUH_API.Models;
using AKUH_API.Repositories;
using Microsoft.AspNetCore.Mvc;
 

namespace AKUH_API.Controllers
{     
    [ApiController]
    [Route("[Controller]")]
    public class SpeakerController : ControllerBase
    {
        SpeakerRepository _speakerRepo;

        public SpeakerController()
        {
            _speakerRepo = new SpeakerRepository();
        }
        [HttpGet]
        [Route("All")]
        public async Task<RspSpeaker> GetAll()
        {
            var data = _speakerRepo.GetAllSpeakers();
            return await data;
        }

    }
}