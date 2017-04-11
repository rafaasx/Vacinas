using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vacinas.Models
{
    [Table("Estoque")]
    public class Estoque
    {
        public int Id { get; set; }
        [Required]
        public int Quantidade { get; set; }
    }
}