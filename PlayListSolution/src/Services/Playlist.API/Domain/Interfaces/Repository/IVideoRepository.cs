using Playlist.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Playlist.API.Domain.Interfaces.Repository
{
    public interface IVideoRepository : IBaseRepository<Video>
    {
        Task<IEnumerable<Video>> BuscarTodos(bool visualizado);

        void DetachLocal(Func<Video, bool> precicate);
    }
}