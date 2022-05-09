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
    public class LobbyController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lobby>>> Get()
        {
            try
            {
                return Ok(await LobbyManager.Load());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Lobby>> Get(Guid id)
        {
            try
            {
                return Ok(await LobbyManager.LoadById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //For the url. Passing in ID
        [HttpPost("{rollback?}")]
        public async Task<IActionResult> Post([FromBody] Lobby Lobby, bool rollback = false)
        {
            try
            {
                await LobbyManager.Insert(Lobby, rollback);
                return Ok(Lobby.Id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}/{rollback?}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Lobby Lobby, bool rollback = false)
        {
            try
            {
                return Ok(await LobbyManager.Update(Lobby, rollback));
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
                return Ok(await LobbyManager.Delete(id, rollback));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
