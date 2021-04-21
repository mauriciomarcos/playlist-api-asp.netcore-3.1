using Playlist.API.Domain.Models;
using System;

namespace Playlist.API.Domain.Interfaces.Repository
{
    public interface ICategoriaRepository : IBaseRepository<Categoria>
    {
        void DetachLocal(Func<Categoria, bool> precicate);
    }
}