using System.ComponentModel.DataAnnotations;

namespace FilmeApi.Data.DTOS
{
    public class UpdateFilmeDto
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string Genero { get; set; }

        [Required]
        public int Duracao { get; set; }
    }
}
