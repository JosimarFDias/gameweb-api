using GameWeb.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWeb.Controllers {

    /// <summary>
    /// Controller Home
    /// </summary>
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase {
        /// <summary>
        /// Método para testes de conexão com a base de dados
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public ActionResult Get([FromServices] DataContext context) { 

            var testConnectionDB = false;
            int count = 0;
            string errorTestConnection = string.Empty;
            try {
                count = context.Players.Count();                
                testConnectionDB = true;
            }
            catch (Exception ex) {
                errorTestConnection = ex.Message;
            }

            var response = new {
                applicationIsRunning = true,
                dataBase = new {
                    isOnline = testConnectionDB,
                    errorTestConnection = errorTestConnection,
                    playersCount = count
                }
            };
                        
            return Ok(response);
        }
    }
}
