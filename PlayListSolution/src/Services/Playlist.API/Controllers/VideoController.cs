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
            var response = await _service.BuscarTodos();
            if (response.Count() is 0) return NoContent();

            return Ok(response);
        }

        [HttpGet]
        [Route("buscarPorId/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(VideoViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _service.BuscarPorId(id);
            if (response is null) return NoContent();

            return Ok(response);
        }

        [HttpGet]
        [Route("buscarPaginado/{pageNumber?}/{pageSize?}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginacaoViewModel<VideoViewModel>>> GetPaginated(int? pageNumber, int? pageSize, [FromQuery] bool visualizado = false)
        {
            pageNumber ??= 1;
            pageSize ??= 9;

            if (pageNumber < 0) pageNumber = 1;

            var videosComDadosDaPaginacao = (await _service.BuscarTodosPaginado<VideoViewModel>(pageNumber, pageSize, visualizado));
            if (videosComDadosDaPaginacao.Items.Count() == 0) return NoContent();

            return Ok(videosComDadosDaPaginacao);
        }

        [HttpPost("criar")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(VideoViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] VideoViewModel video)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _service.Inserir(video);

            return Created(new Uri($"{Request.Path}/{video.Id}", UriKind.Relative), video);
        }

        [HttpPut]
        [Route("atualizar/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(VideoViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(Guid id, [FromBody] VideoViewModel video)
        {
            if (id != Guid.Parse(video.Id)) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();

            var videoAtualizacao = await _service.BuscarPorId(id);
            if (videoAtualizacao is null)
                return NoContent();
            else
                await _service.Atualizar(video);

            return Ok(video);
        }

        [HttpDelete]
        [Route("excluir/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(VideoViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var videoExclusao = await _service.BuscarPorId(id);
            if (videoExclusao is null)
                return NoContent();
            else
                await _service.Excluir(videoExclusao);

            return Ok(videoExclusao);
        }
    }
}