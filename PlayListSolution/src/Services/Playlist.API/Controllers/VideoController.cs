using Canducci.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Playlist.API.Domain.Interfaces.Service;
using Playlist.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlist.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService<VideoViewModel> _service;

        public VideoController(IVideoService<VideoViewModel> service)
        {
            _service = service;
        }

        [HttpGet("buscarTodos")]
        [ProducesResponseType(typeof(IEnumerable<VideoViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _service.BuscarTodos();
                if (response.Count() is 0) return NoContent();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    MensagemErro = ex.Message,
                    StakTraceErro = ex.StackTrace
                });
            }
            
        }

        [HttpGet]
        [Route("buscarPorId/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(VideoViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var response = await _service.BuscarPorId(id);
                if (response is null) return NoContent();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    MensagemErro = ex.Message,
                    StakTraceErro = ex.StackTrace
                });
            }
        }

        [HttpGet]
        [Route("buscarPaginado/{page?}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VideoViewModel>> GetPaginated(int? page, [FromQuery] bool visualizado = false)
        {
            try
            {
                page ??= 1;
                if (page < 0) page = 1;

                var listaVideos = await _service.BuscarTodos(visualizado);
                var responsePaginated = listaVideos.OrderBy(e => e.Id).ToPaginatedRestAsync(page.Value, 10);

                if (responsePaginated.Result.PageCount is 0) return NoContent();

                return Ok(responsePaginated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    MensagemErro = ex.Message,
                    StakTraceErro = ex.StackTrace
                });
            }
        }

        [HttpPost("criar")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(VideoViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] VideoViewModel video)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                _service.Inserir(video);

                return Created(new Uri($"{Request.Path}/{video.Id}", UriKind.Relative), video);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    MensagemErro = ex.Message,
                    StakTraceErro = ex.StackTrace
                });
            }            
        }

        [HttpPut]
        [Route("atualizar/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(VideoViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(Guid id, [FromBody] VideoViewModel video)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                var videoAtualizacao = await _service.BuscarPorId(id);
                if (videoAtualizacao is null)
                    return NoContent();
                else
                    _service.Atualizar(video);

                return Ok(video);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    MensagemErro = ex.Message,
                    StakTraceErro = ex.StackTrace
                });
            }           
        }

        [HttpDelete]
        [Route("excluir/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(VideoViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                var videoExclusao = await _service.BuscarPorId(id);
                if (videoExclusao is null)
                    return NoContent();
                else
                    _service.Excluir(videoExclusao);

                return Ok(videoExclusao);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    MensagemErro = ex.Message,
                    StakTraceErro = ex.StackTrace
                });
            }           
        }
    }
}