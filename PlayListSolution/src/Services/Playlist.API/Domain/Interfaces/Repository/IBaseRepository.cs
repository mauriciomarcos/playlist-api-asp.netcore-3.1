using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Playlist.API.Domain.Interfaces.Repository
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        void Inserir(TEntity e);

        void Atualizar(TEntity e);

        void Excluir(TEntity e);

        Task<TEntity> BuscarPorId(Guid id);

        Task<IEnumerable<TEntity>> BuscarTodos();
    }
}