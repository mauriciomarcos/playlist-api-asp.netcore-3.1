using Canducci.Pagination;
using Playlist.API.Domain.Interfaces.Repository;
using Playlist.API.Domain.Interfaces.Service;
using Playlist.API.Domain.Models;
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

        public async Task Atualizar(VideoViewModel e)
        {
            _videoRepository.DetachLocal(_ => _.Id == Guid.Parse(e.Id));
            await _videoRepository.Atualizar(e);
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

        public async Task<PaginatedRest<VideoViewModel>> BuscarTodosPaginado(int? pageNumber, int? pageSize, bool visualizado)
        {
            var responseRepository = await _videoRepository.BuscarTodosPaginado(pageNumber, pageSize, visualizado);

            return await responseRepository.Items.Select(video => (VideoViewModel)video)
                .ToPaginatedRestAsync(pageNumber.Value, pageSize.Value);           
        }

        public async Task Excluir(VideoViewModel e)
        {
            _videoRepository.DetachLocal(_ => _.Id == Guid.Parse(e.Id));
            await _videoRepository.Excluir(e);
        }

        public async Task Inserir(VideoViewModel e)
        {
            var video = (Video)e;
            await _videoRepository.Inserir(video);

            e.Id = video.Id.ToString();
        }
    }
}