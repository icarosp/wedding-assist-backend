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
        private UserRepository _repo = new UserRepository();


        [HttpPost]
        [Route("get_user_by_email")]
        public IActionResult CheckUserInfo([FromBody]string email)
        {
            try
            {
                //UserRepository userRepository = new UserRepository();
                User user = _repo.GetUserByEmail(email);
                if (user == null)
                    return Ok(user);
                return NotFound();

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
                if (_repo.IsEmailAlreadyRegistered(fiance.Email))
                {
                    return BadRequest(new Result(null, "Já existe um usuário cadastrado com esse email. Recupere a senha ou informe outro endereço de email."));
                }
                else {
                    _repo.SaveFiance(fiance);
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
                if (_repo.IsEmailAlreadyRegistered(provider.Email))
                {
                    return BadRequest(new Result(null, "Já existe um usuário cadastrado com esse email. Recupere a senha ou informe outro endereço de email."));
                }


                //UserRepository userRepository = new UserRepository();
                _repo.SaveProvider(provider);
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
                //UserRepository userRepository = new UserRepository();
                _repo.ConfirmEmail(email);
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
                //UserRepository userRepository = new UserRepository();
                List<Fiance> fiances = _repo.GetAllFiances();
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
                //UserRepository userRepository = new UserRepository();
                List<Provider> providers = _repo.GetAllProviders();
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
