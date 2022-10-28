using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos
{
    public class UpdateFilmeDto{
        [Key]
        [Required]
        public int Id { get;  set; }

       [Required(ErrorMessage = "Campo Titulo é Obrigatorio")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Campo Diretor é Obrigatorio")]
        public string Diretor { get; set; }

        [Required(ErrorMessage = "Campo Genero é obrigatorio")]
        [StringLength(30, ErrorMessage = "Campo Genero é Deve ter menos de 30 caracteres")]
        public string Genero { get; set; }
        
        [Range(1, 600, ErrorMessage = "Campo Duracao é Obrigatorio")]
        public int Duracao { get; set; }
    }
}