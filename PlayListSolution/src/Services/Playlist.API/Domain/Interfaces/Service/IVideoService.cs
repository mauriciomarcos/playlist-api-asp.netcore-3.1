using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Playlist.API.Domain.Interfaces.Service
{
    public interface IVideoService<VideoViewModel>
    {
        void Inserir(VideoViewModel e);

        void Atualizar(VideoViewModel e);

        void Excluir(VideoViewModel e);

        Task<VideoViewModel> BuscarPorId(Guid id);

        Task<IEnumerable<VideoViewModel>> BuscarTodos();
    }
}