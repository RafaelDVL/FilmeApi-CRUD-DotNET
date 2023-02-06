using System.ComponentModel.DataAnnotations;

namespace FilmeApi.Model
{
    public class Filme
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Genero { get; set; }

        [Required]
        public int Duracao { get; set; }
    }
}
