using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vacinas.Models
{
    [Table("Regiao")]
    public class Regiao
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }
    }
}