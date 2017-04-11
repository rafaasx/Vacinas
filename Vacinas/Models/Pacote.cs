using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vacinas.Models
{
    [Table("Pacote")]
    public class Pacote
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime DataEnvio { get; set; }
        public int QuantidadeCriancas { get; set; }
        public int QuantidadeAdultos { get; set; }
        public int QuantidadeIdosos { get; set; }
        public int TotalCriancas { get; set; }
        public int TotalAdultos { get; set; }
        public int TotalIdosos { get; set; }
        public int TotalGeral { get; set; }
        [Required]
        public int RegiaoId { get; set; }
        [ForeignKey("RegiaoId")]
        public virtual Regiao Regiao { get; set; }
    }
}