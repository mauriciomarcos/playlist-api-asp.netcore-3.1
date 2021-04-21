using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Playlist.API.Domain.Interfaces.Repository
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        Task Inserir(TEntity e);

        Task Atualizar(TEntity e);

        Task Excluir(TEntity e);

        Task<TEntity> BuscarPorId(Guid id);

        Task<IEnumerable<TEntity>> BuscarTodosAsNoTracking();

        Task<IEnumerable<TEntity>> BuscarTodos();
    }
}