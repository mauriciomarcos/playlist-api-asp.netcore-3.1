using Playlist.API.Domain.Interfaces.Repository;
using Playlist.API.Domain.Interfaces.Service;
using Playlist.API.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlist.API.Domain.Services
{
    public class CategoriaService : ICategoriaService<CategoriaViewModel>
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IEnumerable<CategoriaViewModel>> BuscarTodos()
        {
            var listaCategoriaRepository = await _categoriaRepository.BuscarTodos();
            return listaCategoriaRepository.Select(e => (CategoriaViewModel)e);
        }
    }
}