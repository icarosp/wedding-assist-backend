﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeddingAssist.Domain.Entities;
using WeddingAssist.Domain.Infra;
using WeddingAssist.Api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeddingAssist.Api.Controllers
{
    [Route("api/[controller]")]
    public class BudgetController : Controller
    {
        private BudgetRepository _repo = new BudgetRepository();

        [HttpPost]
        [Route("save_budget")]
        public IActionResult SaveBudget([FromBody] Domain.Entities.AuctionBudget budget)
        {
            try
            {
                int auctionId = _repo.SaveBudget(budget);
                return Created("SaveBudget", new Result(new { auctionId = auctionId }));
            }
            catch(Exception e)
            {
                return StatusCode(500, new Result(null, e.Message));
            }
        }

        [HttpGet]
        [Route("get_budgets_by_fiance/{id}")]
        public IActionResult GetBudgesByFiance([FromRoute]int id)
        {
            try
            {
                List<Domain.Entities.AuctionBudget> budgets = _repo.GetBudgetsByFiance(id);
                return Ok(new Result(budgets));
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
