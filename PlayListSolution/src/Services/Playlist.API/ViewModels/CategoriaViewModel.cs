using System.ComponentModel.DataAnnotations;

namespace Playlist.API.ViewModels
{
    public class CategoriaViewModel
    {
        [Display(Name = "Código da categoria", ShortName = "Id")]
        public string Id { get; set; }

        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "O nome da descrição excede o tamanho máximo de caracter permitido de 250 caracteres.")]
        [Display(Name = "Descrição do Vídeo", ShortName = "Descrição")]
        public string Nome { get; set; }
    }
}