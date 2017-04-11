using System;

namespace Vacinas.Models
{
	public class RegiaoDTO
    {
		public System.Int32 Id { get; set; }
		public System.String Nome { get; set; }

        public static System.Linq.Expressions.Expression<Func<Regiao, RegiaoDTO>> Select =
            x => new  RegiaoDTO
            {
                Id = x.Id,
                Nome = x.Nome,
            };

	}
}