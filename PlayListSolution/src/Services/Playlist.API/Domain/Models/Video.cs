using Playlist.API.ViewModels;
using System;

namespace Playlist.API.Domain.Models
{
    public class Video
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string NomeCanal { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime DataVisualizacao { get; set; }

        public string LinkVideo { get; set; }

        public bool Visualizado { get; set; }

        public static implicit operator VideoViewModel(Video video)
        {
            if (video is null) return null;

            return new VideoViewModel
            {
                Id = video.Id.ToString(),
                DescricaoVideo = video.Nome,
                NomeCanal = video.NomeCanal,
                DataCadastro = video.DataCadastro,
                DataVisualizacao = video.DataVisualizacao,
                LinkVideoExterno = video.LinkVideo,
                Visualizado = video.Visualizado
            };
        }            
    }
}