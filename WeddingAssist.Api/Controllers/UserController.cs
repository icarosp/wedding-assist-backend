using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeddingAssist.Domain.Entities;
using WeddingAssist.Domain.Infra;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using WeddingAssist.Api.Models;

namespace WeddingAssist.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        [HttpPost]
        [Route("get_user_by_email")]
        public IActionResult CheckUserInfo([FromBody]string email)
        {
            try
            {
                UserRepository userRepository = new UserRepository();
                User user = userRepository.GetUserByEmail(email);
                if (user != null)
                    return Ok(new Result(user));
                return NotFound(new Result(user, "Usuário não encontrado!"));

            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost]
        [Route("save_fiance")]
        public IActionResult SaveFiance([FromBody]Fiance fiance)
        {
            try
            {
                UserRepository userRepository = new UserRepository();

                if (userRepository.IsEmailAlreadyRegistered(fiance.Email))
                {
                    return BadRequest(new Result(null, "Já existe um usuário cadastrado com esse email. Recupere a senha ou informe outro endereço de email."));
                }
                else {
                    userRepository.SaveFiance(fiance);
                    return Created("SaveFiance", new Result(null));
                }

            }
            catch (Exception e)
            {
                return StatusCode(500, new Result(null, e.Message));
            }
        }

        [HttpPost]
        [Route("save_provider")]
        public IActionResult SaveProvider([FromBody]Provider provider)
        {
            try
            {
                UserRepository userRepository = new UserRepository();
                userRepository.SaveProvider(provider);
                return Created("SaveProvider", provider);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost]
        [Route("confirm_email")]
        public IActionResult ConfirmEmail([FromBody]string email)
        {
            try
            {
                UserRepository userRepository = new UserRepository();
                userRepository.ConfirmEmail(email);
                return Ok($"Email {email} successful confirmed!");
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet]
        [Route("fiances")]
        public IActionResult GetAllFiances()
        {
            try
            {
                UserRepository userRepository = new UserRepository();
                List<Fiance> fiances = userRepository.GetAllFiances();
                return Ok(new { fiances = fiances });
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet]
        [Route("providers")]
        public IActionResult GetAllProviders()
        {
            try
            {
                UserRepository userRepository = new UserRepository();
                List<Provider> providers = userRepository.GetAllProviders();
                return Ok(new { providers = providers });
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
