using Playlist.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Playlist.API.Domain.Interfaces.Service
{
    public interface IVideoService<TEntity>
    {
        Task Inserir(TEntity e);

        Task Atualizar(TEntity e);

        Task Excluir(TEntity e);

        Task<TEntity> BuscarPorId(Guid id);

        Task<IEnumerable<TEntity>> BuscarTodos();

        Task<PaginacaoViewModel<T>> BuscarTodosPaginado<T>(int? pageNumber, int? pageSize, bool visualizado) where T : class;
    }
}