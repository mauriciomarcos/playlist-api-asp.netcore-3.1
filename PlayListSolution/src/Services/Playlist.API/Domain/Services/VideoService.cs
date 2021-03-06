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
        private readonly ICategoriaRepository _categoriaRepository;

        public VideoService(
            IVideoRepository videoRepository,
            ICategoriaRepository categoriaRepository
            )
        {
            _videoRepository = videoRepository;
            _categoriaRepository = categoriaRepository;
        }

        public async Task Atualizar(VideoViewModel e)
        {
            if (e.Visualizado) e.DataVisualizacao = DateTime.Now;
            else e.DataVisualizacao = default;

            _videoRepository.DetachLocal(_ => _.Id == Guid.Parse(e.Id));

            var video = await AssociarCategoria(e.CategoriaId, (Video)e);
            await _videoRepository.Atualizar(video);
        }

        public async Task<VideoViewModel> BuscarPorId(Guid id)
        {
            VideoViewModel viewModel = await _videoRepository.BuscarPorId(id);
            return viewModel;
        }

        public async Task<IEnumerable<VideoViewModel>> BuscarTodos()
        {
            var listaVideoRepository = await _videoRepository.BuscarTodos();
            return listaVideoRepository.Select(video => (VideoViewModel)video);
        }

        public async Task<PaginacaoViewModel<T>> BuscarTodosPaginado<T>(int? pageNumber, int? pageSize, bool visualizado) where T : class
        {
            var responseRepository = await _videoRepository.BuscarTodosPaginado(pageNumber, pageSize, visualizado);
            var paginacaoViewModel = new PaginacaoViewModel<VideoViewModel>();

            await Task.Run(() =>
            {
                paginacaoViewModel.PageCount = responseRepository.PageCount;
                paginacaoViewModel.TotalItemCount = responseRepository.TotalItemCount;
                paginacaoViewModel.PageNumber = responseRepository.PageNumber;
                paginacaoViewModel.PageSize = responseRepository.PageSize;
                paginacaoViewModel.HasPreviousPage = responseRepository.HasPreviousPage;
                paginacaoViewModel.HasNextPage = responseRepository.HasNextPage;
                paginacaoViewModel.IsFirstPage = responseRepository.IsFirstPage;
                paginacaoViewModel.IsLastPage = responseRepository.IsLastPage;
                paginacaoViewModel.FirstItemOnPage = responseRepository.FirstItemOnPage;
                paginacaoViewModel.Items = responseRepository.Items.Select(video => (VideoViewModel)video);                               
            });

            return paginacaoViewModel as PaginacaoViewModel<T>;
        }

        public async Task Excluir(VideoViewModel e)
        {
            _videoRepository.DetachLocal(_ => _.Id == Guid.Parse(e.Id));
            await _videoRepository.Excluir(e);
        }

        public async Task Inserir(VideoViewModel e)
        {
            var video = await AssociarCategoria(e.CategoriaId, (Video)e);
            await _videoRepository.Inserir(video);

            e.Id = video.Id.ToString();
            e.CategoriaId = video.Categoria?.Id.ToString();
            e.NomeCategoria = video.Categoria?.Nome;        
        }

        private async Task<Video> AssociarCategoria(string categoriaId, Video video)
        {
            if (!string.IsNullOrEmpty(categoriaId))
                video.Categoria = await _categoriaRepository.BuscarPorId(Guid.Parse(categoriaId));

            return video;
        }
    }
}