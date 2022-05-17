using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yahtzee.BL;
using Yahtzee.BL.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Yahztee.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ScorecardController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Scorecard>>> Get()
        {
            try
            {
                return Ok(await ScorecardManager.Load());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Scorecard>> Get(Guid id)
        {
            try
            {
                return Ok(await ScorecardManager.LoadById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{userId}/{run}")]
        public async Task<ActionResult<Scorecard>> GetUserId(Guid userId, bool run = true)
        {
            try
            {
                return Ok(await ScorecardManager.LoadByUserId(userId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //For the url. Passing in ID
        [HttpPost("{rollback?}")]
        public async Task<IActionResult> Post([FromBody] Scorecard Scorecard, bool rollback = false)
        {
            try
            {
                await ScorecardManager.Insert(Scorecard, rollback);
                return Ok(Scorecard.Id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //Default Put parameters changed, got rid of Guid id to be able to use on UpdateByUserId (below)
        [HttpPut("{id}/{rollback?}")]
        public async Task<IActionResult> Put([FromBody] Scorecard Scorecard, bool rollback = false)
        {
            try
            {
                return Ok(await ScorecardManager.Update(Scorecard, rollback));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{userId}/{rollback?}/{run}")]
        public async Task<IActionResult> Put(Guid userId, [FromBody] Scorecard scorecard, bool rollback = false, bool run = true)
        {
            try
            {
                return Ok(await ScorecardManager.UpdateByUserId(userId, scorecard, rollback));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}/{rollback?}")]
        public async Task<IActionResult> Delete(Guid id, bool rollback = false)
        {
            try
            {
                return Ok(await ScorecardManager.Delete(id, rollback));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
