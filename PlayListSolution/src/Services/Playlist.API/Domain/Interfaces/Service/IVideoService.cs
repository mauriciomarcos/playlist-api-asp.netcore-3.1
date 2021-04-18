using Canducci.Pagination;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Playlist.API.Domain.Interfaces.Service
{
    public interface IVideoService<VideoViewModel>
    {
        Task Inserir(VideoViewModel e);

        Task Atualizar(VideoViewModel e);

        Task Excluir(VideoViewModel e);

        Task<VideoViewModel> BuscarPorId(Guid id);

        Task<IEnumerable<VideoViewModel>> BuscarTodos();

        Task<PaginatedRest<VideoViewModel>> BuscarTodosPaginado(int? pageNumber, int? pageSize, bool visualizado);
    }
}