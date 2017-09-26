using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeddingAssist.Api.Models;
using WeddingAssist.Domain.Entities;
using WeddingAssist.Domain.Infra;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeddingAssist.Api.Controllers
{
    [Route("api/[controller]")]
    public class BidController : Controller
    {
        private BidRepository _repo = new BidRepository();

        [HttpPost]
        [Route("save_bid")]
        public IActionResult SaveBid([FromBody]Bid bid)
        {
            try
            {
                int bidId = _repo.SaveBid(bid);
                return Created("SaveBid", new Result(new { bidId = bidId }));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Result(null, e.Message));
            }
        }

        [HttpGet]
        [Route("get_bid/{id}")]
        public IActionResult GetBidById([FromRoute] int id)
        {
            try
            {
                Bid bid = _repo.GetBidById(id);
                return Created("SaveBid", new Result(bid));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Result(null, e.Message));
            }
        }

        [HttpPost]
        [Route("chooseBid")]
        public IActionResult ChooseBid([FromBody] int id)
        {
            try
            {
                _repo.SaveWinnerBid(id);
                return Created("SaveBid", new Result(null));
            }
            catch (Exception e)
            {
                return StatusCode(500, new Result(null, e.Message));
            }
        }

        [HttpGet]
        [Route("teste")]
        public IActionResult teste() => Ok("|==============|\n|=API RODANDO!=|\n|==============|");
    }
}
