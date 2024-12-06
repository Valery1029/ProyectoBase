using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAppApiMoto.Model
{
    public class Moto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Codigo { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Marca { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Descripcion { get; set; }

        [Range(0, double.MaxValue)]
        public double Modelo { get; set; }

        [Range(0, double.MaxValue)]
        public double Precio { get; set; }

        public bool Disponible { get; set; }
    }
}

