using AKUH_API.Models;
using AKUH_API.Repositories;
using Microsoft.AspNetCore.Mvc;
 

namespace AKUH_API.Controllers
{     
    [ApiController]
    [Route("[Controller]")]
    public class OrganisingCommitteeController : ControllerBase
    {
        OrganisingCommitteeRepository _organisingCommitteeRepo;

        public OrganisingCommitteeController()
        {
            _organisingCommitteeRepo = new OrganisingCommitteeRepository();
        }
        [HttpGet]
        [Route("All")]
        public async Task<RspOrganisingCommittee> GetAll()
        {
            var data = _organisingCommitteeRepo.GetAll();
            return await data;
        }

    }
}