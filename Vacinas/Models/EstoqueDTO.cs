using System;

namespace Vacinas.Models
{
	public class EstoqueDTO
    {
		public System.Int32 Id { get; set; }
		public System.Int32 Quantidade { get; set; }

        public static System.Linq.Expressions.Expression<Func<Estoque, EstoqueDTO>> SELECT =
            x => new  EstoqueDTO
            {
                Id = x.Id,
                Quantidade = x.Quantidade,
            };

	}
}