using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos
{
    public class UpdateDiretorDto{
        [Key]
        [Required]
        public int Id { get;  set; }

        [Required(ErrorMessage = "Campo Diretor é Obrigatorio")]
        public string Diretor { get; set; }

    }
}