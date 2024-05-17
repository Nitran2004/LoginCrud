using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecureAssetManager.Models
{
    public class Asset
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "El código de activo es obligatorio.")]
        [Display(Name = "Codigo")]
        public string CodigoActivo { get; set; }

        [Required(ErrorMessage = "El nombre del activo es obligatorio.")]
        [Display(Name = "Persona encargada")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El tipo de activo es obligatorio.")]
        [Display(Name = "Nivel de Importancia")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "La categoría del activo es obligatoria.")]
        [Display(Name = "Alineamiento y objetivos de gobierno Sigla AG01-AG13")]
        public string Categoria { get; set; }

        [Required(ErrorMessage = "La ubicación del activo es obligatoria.")]
        [Display(Name = "Alineamiento y objetivos de gobierno Sigla (EDM01-EDM05)(APO01-APO14)(BAI01-BAI11)(DSS01-DSS06)(MEA01-MEA04)")]
        public string Ubicacion { get; set; }


    }
}
