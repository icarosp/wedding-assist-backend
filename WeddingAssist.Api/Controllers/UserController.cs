using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeddingAssist.Domain.Entities;
using WeddingAssist.Domain.Infra;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace WeddingAssist.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        [HttpPost]
        [Route("get_user_by_email")]
        public IActionResult CheckUserInfo([FromBody]string email)
        {
            UserRepository userRepository = new UserRepository();
            userRepository.GetUserByEmail(email);
            return Ok(email);
        }

        [HttpPost]
        [Route("save_fiance")]
        public IActionResult SaveFiance([FromBody]Fiance fiance)
        {
            try
            {
                UserRepository userRepository = new UserRepository();
                userRepository.SaveFiance(fiance);
                return Created("SaveFiance", fiance);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet]
        [Route("teste")]
        public IActionResult teste()
        {
            return Ok("|==============|\n|=API RODANDO!=|\n|==============|");
        }
    }
}
