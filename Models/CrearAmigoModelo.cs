
using System.ComponentModel.DataAnnotations;

namespace MVCTest01.Models
{
    public class CrearAmigoModelo
    {

        [Required(ErrorMessage = "Obligatorio")]
        [MaxLength(100, ErrorMessage = "No más de 100 carácteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Obligatorio")]
        [Display(Name = "Email")]
        [RegularExpression(@"^[a-zA-z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Formato incorrecto")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        public Provincia? Ciudad { get; set; }

        public IFormFile Foto { get; set; }
    }
}
