using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Playlist.API.Domain.Interfaces.Service;
using Playlist.API.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlist.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService<CategoriaViewModel> _service;

        public CategoriaController(ICategoriaService<CategoriaViewModel> categoriaService)
        {
            _service = categoriaService;
        }

        [HttpGet("buscarTodos")]
        [ProducesResponseType(typeof(IEnumerable<CategoriaViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.BuscarTodos();
            if (response.Count() is 0) return NoContent();

            return Ok(response);
        }
    }
}