using System.Collections.Generic;
using System.Threading.Tasks;

namespace Playlist.API.Domain.Interfaces.Service
{
    public interface ICategoriaService<TEntity>
    {
        Task<IEnumerable<TEntity>> BuscarTodos();
    }
}