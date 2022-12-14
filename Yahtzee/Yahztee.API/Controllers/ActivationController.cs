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
    public class ActivationController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activation>>> Get()
        {
            try
            {
                return Ok(await ActivationManager.Load());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //For the url. Passing in ID
        [HttpPost("{rollback?}")]
        public async Task<IActionResult> Post([FromBody] Activation activation, bool rollback = false)
        {
            try
            {
                await ActivationManager.Insert(activation, rollback);
                return Ok(activation.Id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}/{rollback?}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Activation activation, bool rollback = false)
        {
            try
            {
                return Ok(await ActivationManager.Update(activation, rollback));
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
                return Ok(await ActivationManager.Delete(id, rollback));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
