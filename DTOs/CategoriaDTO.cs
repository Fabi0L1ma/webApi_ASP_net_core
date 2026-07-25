using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs
{
    public class CategoriaDTO
    {
        public int CategoriaID { get; set; }
        [Required]
        [StringLength(80)]
        public string? Nome { get; set; }
        [Required]
        [StringLength(300)]
        public string? ImageUrl { get; set; }
    }
}
