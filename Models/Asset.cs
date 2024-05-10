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
        [Display(Name = "Alineamiento y objetivos de gobierno Sigla AG01-AG13")]
        public string CodigoActivo { get; set; }

        [Required(ErrorMessage = "El nombre del activo es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre del activo debe tener máximo 50 caracteres.")]
        [Display(Name = "Sigla EDM01")]



    }
}
