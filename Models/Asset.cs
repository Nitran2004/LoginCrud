using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecureAssetManager.Models
{
    public class Asset
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "El código de activo es obligatorio.")]
        [StringLength(10, ErrorMessage = "El código de activo debe tener máximo 10 caracteres.")]
        [Display(Name = "ArtistaID")]
        public string CodigoActivo { get; set; }

        [Required(ErrorMessage = "El nombre del activo es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre del activo debe tener máximo 50 caracteres.")]
        [Display(Name = "NombrePilaArtista")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El responsable del activo es obligatorio.")]
        [StringLength(50, ErrorMessage = "El responsable del activo debe tener máximo 50 caracteres.")]
        [Display(Name = "NombreArtistico")]
        public string Responsable { get; set; }

        [StringLength(250, ErrorMessage = "La descripción del activo debe tener máximo 250 caracteres.")]
        [Display(Name = "FechadeNacimiento")]
        public string Descripcion { get; set; }


    }
}
