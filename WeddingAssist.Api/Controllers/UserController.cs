using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeddingAssist.Domain.Entities;
using WeddingAssist.Domain.Infra;

namespace WeddingAssist.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        [HttpPost]
        [Route("check_user_info")]
        public IActionResult CheckUserInfo([FromBody]User user)
        {
            UserRepository userRepository = new UserRepository();
            userRepository.GetFianceByEmail("icaro.ifg@gmail.com");
            return Ok();
        }

        [HttpGet]
        [Route("teste")]
        public IActionResult teste()
        {
            UserRepository userRepository = new UserRepository();
            userRepository.GetFianceByEmail("icaro.ifg@gmail.com");
            return Ok();
        }
    }
}
