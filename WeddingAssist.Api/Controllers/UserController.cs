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
                User user = _repo.GetUserByEmail(email);
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
                else {
                    _repo.SaveProvider(provider);
                    return Created("SaveProvider", new Result(null));
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new Result(null, e.Message));
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
                return Ok(new Result(null));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Result(null, e.Message));
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
                return Ok(new Result(new { fiances = fiances }));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Result(null, e.Message));
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
                return Ok(new Result(new { providers = providers }));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Result(null, e.Message));
            }
        }


        [HttpGet]
        [Route("fiance/{id}")]
        public IActionResult GetFianceById([FromRoute]int id)
        {
            try
            {
                Fiance fiance = _repo.GetFianceById(id);
                if (fiance != null)
                    return Ok(new Result(fiance));
                return NotFound(new Result(fiance, "Usuário não encontrado!"));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Result(null, e.Message));
            }
        }

        [HttpGet]
        [Route("provider/{id}")]
        public IActionResult GetProviderById([FromRoute]int id)
        {
            try
            {
                Provider provider = _repo.GetProviderById(id);
                if (provider != null)
                    return Ok(new Result(provider));
                return NotFound(new Result(provider, "Usuário não encontrado!"));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Result(null, e.Message));
            }
        }

        [HttpPut]
        [Route("provider/{id}")]
        public IActionResult UpdateProvider([FromRoute]int id, [FromBody]Provider provider)
        {
            try
            {
                Provider updatedProvider = _repo.UpdateProvider(id, provider);
                return Ok(new Result(updatedProvider));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Result(null, e.Message));
            }
        }

        [HttpPut]
        [Route("fiance/{id}")]
        public IActionResult UpdateFiance([FromRoute]int id, [FromBody]Fiance fiance)
        {
            try
            {
                Fiance updatedFiance = _repo.UpdateFiance(id, fiance);
                return Ok(new Result(updatedFiance));
            }
            catch(Exception e)
            {
                return StatusCode(500, new Result(null, e.Message));
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
