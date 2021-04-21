using Playlist.API.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Playlist.API.ViewModels
{
    public class VideoViewModel
    {
        [Display(Name = "Código do Vídeo", ShortName = "Id")]
        public string Id { get; set; }

        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "O nome da descrição excede o tamanho máximo de caracter permitido de 250 caracteres.")]
        [Display(Name = "Descrição do Vídeo", ShortName = "Descrição")]
        public string DescricaoVideo { get; set; }

        [StringLength(maximumLength: 250, ErrorMessage = "O nome da descrição excede o tamanho máximo de caracter permitido de 250 caracteres.")]
        [Display(Name = "Nome do Canal", ShortName = "Canal")]
        public string NomeCanal { get; set; }


        [DataType(DataType.Date, ErrorMessage = "Data no formato inválido.")]
        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Data no formato inválido.")]
        [Display(Name = "Data da Visualização do Vídeo.")]
        public DateTime DataVisualizacao { get; set; }

        [Required]
        [Display(Name = "Link do Vídeo", ShortName = "Link")]
        [Url(ErrorMessage = "URL com formato inválido.")]
        public string LinkVideoExterno { get; set; }

        public bool Visualizado { get; set; }

        [Display(Name = "Identificador da categoria do vídeo.")]
        public string CategoriaId { get; set; }

        [Display(Name = "Nome da categoria do vídeo.")]
        [StringLength(maximumLength: 250, ErrorMessage = "O nome da categoria excede o tamanho máximo de caracter permitido de 250 caracteres.")]
        public string NomeCategoria { get; set; }

        public static implicit operator Video(VideoViewModel viewModel) =>
            new Video
            {
                Id = string.IsNullOrEmpty(viewModel.Id) ? Guid.NewGuid() : Guid.Parse(viewModel.Id),
                Nome = viewModel.DescricaoVideo,
                NomeCanal = viewModel.NomeCanal,
                DataCadastro = viewModel.DataCadastro,
                DataVisualizacao = viewModel.DataVisualizacao,
                LinkVideo = viewModel.LinkVideoExterno,
                Visualizado = viewModel.Visualizado,
                Categoria = new Categoria
                {
                    Id = Guid.Parse(viewModel.CategoriaId),
                    Nome = viewModel.NomeCategoria
                }
            };
    }
}