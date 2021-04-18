using Canducci.Pagination;
using Playlist.API.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Playlist.API.Domain.Interfaces.Repository
{
    public interface IVideoRepository : IBaseRepository<Video>
    {
        Task<PaginatedRest<Video>> BuscarTodosPaginado(int? pageNumber, int? pageSize, bool visualizado);

        void DetachLocal(Func<Video, bool> precicate);
    }
}