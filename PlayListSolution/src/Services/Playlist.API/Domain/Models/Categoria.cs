using Playlist.API.ViewModels;
using System;
using System.Collections.Generic;

namespace Playlist.API.Domain.Models
{
    public class Categoria
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public ICollection<Video> Videos { get; set; }

        public static implicit operator CategoriaViewModel(Categoria categoria)
        {
            if (categoria is null) return null;

            return new CategoriaViewModel
            {
                Id = categoria.Id.ToString(),
                Nome = categoria.Nome
            };
        }
    }
}