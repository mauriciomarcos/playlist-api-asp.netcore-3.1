using Playlist.API.Domain.Interfaces.Repository;
using Playlist.API.Domain.Interfaces.Service;
using Playlist.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlist.API.Domain.Services
{
    public class VideoService : IVideoService<VideoViewModel>
    {
        private readonly IVideoRepository _videoRepository;

        public VideoService(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public void Atualizar(VideoViewModel e)
        {
            _videoRepository.Atualizar(e);
        }

        public async Task<VideoViewModel> BuscarPorId(Guid id)
        {
            VideoViewModel viewModel = await _videoRepository.BuscarPorId(id);
            return viewModel;
        }

        public async Task<IEnumerable<VideoViewModel>> BuscarTodos()
        {
            var listaVideoRepository = await _videoRepository.BuscarTodos();
            var listaViewModel = listaVideoRepository.Select(video => (VideoViewModel)video);

            return listaViewModel;
        }

        public async Task<IEnumerable<VideoViewModel>> BuscarTodos(bool visualizado)
        {
            var listaVideoRepository = await _videoRepository.BuscarTodos(visualizado);
            var listaViewModel = listaVideoRepository.Select(video => (VideoViewModel)video);

            return listaViewModel;
        }

        public void Excluir(VideoViewModel e)
        {
            _videoRepository.Excluir(e);
        }

        public void Inserir(VideoViewModel e)
        {
            _videoRepository.Inserir(e);
        }
    }
}