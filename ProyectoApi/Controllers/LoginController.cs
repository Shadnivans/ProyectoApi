using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProyectoApi.Datos;
using ProyectoApi.Models.Login.Operaciones;
using System;
using System.Threading.Tasks;

namespace ProyectoApi.Controllers
{
    [Route("proyectoapi/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        #region Miembros privados del controlador

        private readonly ILogger<LoginController> logger;
        private readonly IMapeoDatosLogin mapeoDatosLogin;

        #endregion

        #region Construcctor

        public LoginController(IMapeoDatosLogin _mapeoDatosLogin, ILogger<LoginController> _logger)
        {
            this.mapeoDatosLogin = _mapeoDatosLogin;
            this.logger = _logger;
        }

        #endregion

        #region Metodos Api

        [HttpPost("access")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AccessResponse>> Access([FromBody] AccessRequest Request)
        {
            try
            {
                AccessResponse data = await this.mapeoDatosLogin.Access(Request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        #endregion
    }
}
