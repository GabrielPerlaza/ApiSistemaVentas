using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemadeVentas.BLL.Servicios.Contrato;
using SistemadeVentas.DTO;
using SistemadeVentas.Api.Utilidad;
using SistemadeVentas.BLL.Servicios;
namespace SistemadeVentas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolServicio;

        public RolController(IRolService rolServicio)
        {
            _rolServicio = rolServicio; 
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<RolDTO>>();

            try
            {
                rsp.Status = true;
                rsp.value = await _rolServicio.Lista();


            }
            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);

        }
    }
}
