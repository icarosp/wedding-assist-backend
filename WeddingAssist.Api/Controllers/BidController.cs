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
    }
}
