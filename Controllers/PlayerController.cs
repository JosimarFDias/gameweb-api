using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameWeb.Data;
using GameWeb.Models;
using System;
using System.Linq;

namespace GameWeb.Controllers
{   
    /// <summary>
    /// Controller do Player
    /// </summary>
    [ApiController]
    [Route("api/players")]
    public class PlayerController : ControllerBase
    {   
        /// <summary>
        /// Método responsável por resgatar todos os players
        /// </summary>
        /// <remarks>Retorna a lista de players ordenados pela pontuação</remarks>
        /// <response code="500">Internal Server Error.</response>
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Player>>> Get([FromServices] DataContext context)
        {
            var players = await context.Players.ToListAsync();
            return players;
        }

        /// <summary>
        /// Método responsável por resgatar 100 players (Endpoint 2)
        /// </summary>
        /// <param name="id">Identificador do Game (GameId)</param>
        /// <remarks>Retorna a lista de 100 jogadores ordenados pela pontuação e filtrado pelo Identificador do Game (GameId)</remarks>
        /// <response code="500">Internal Server Error.</response>
        /// <response code="404">Not Found.</response>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Player>>> Get([FromServices] DataContext context, int id)
        {            
            var dbPlayers = await context.Players.ToListAsync();
            IQueryable<Player> players = dbPlayers.AsQueryable();

            var filteredList = players.OrderByDescending(x => x.Win).Where(x => x.GameId == id).Take(100).
                Select(item => new {
                    playerId = item.PlayerId,
                    balance =  item.Win,
                    lastUpdateDate = item.Timestamp
                }).ToList();            
            
            if (filteredList.Count > 0)  
                return Ok(filteredList);
            else
                return NotFound();
        }

        [HttpPost]
        [Route("")]        
        public async Task<ActionResult<Player>> Post([FromServices] DataContext context, 
            [FromBody] Player model)
        {
            if (ModelState.IsValid)
            {
                var dbPlayers = await context.Players.ToListAsync();
                IQueryable<Player> players = dbPlayers.AsQueryable();
                
                Player playerDb = players.FirstOrDefault(x => x.PlayerId == model.PlayerId && x.GameId == model.GameId);
                
                if (playerDb == null)
                {
                    model.Id = Guid.NewGuid();
                    model.DataInclusao = DateTime.Now;
                    context.Players.Add(model);                    
                }
                else
                {
                    playerDb.Win += model.Win;
                    playerDb.DataAlteracao = DateTime.Now;
                    playerDb.Timestamp = DateTime.UtcNow;                    
                }

                context.SaveChanges();
                if  (playerDb != null)
                    return playerDb;
                else
                    return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}