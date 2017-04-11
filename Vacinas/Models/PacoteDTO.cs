using System;

namespace Vacinas.Models
{
	public class PacoteDTO
    {
		public string Regiao_Nome { get; set; }
		public System.Int32 Id { get; set; }
		public System.DateTime DataEnvio { get; set; }
		public System.Int32 QuantidadeCriancas { get; set; }
		public System.Int32 QuantidadeAdultos { get; set; }
		public System.Int32 QuantidadeIdosos { get; set; }
		public System.Int32 TotalCriancas { get; set; }
		public System.Int32 TotalAdultos { get; set; }
		public System.Int32 TotalIdosos { get; set; }
		public System.Int32 TotalGeral { get; set; }
		public System.Int32 RegiaoId { get; set; }

        public static System.Linq.Expressions.Expression<Func<Pacote, PacoteDTO>> SELECT =
            x => new  PacoteDTO
            {
                Regiao_Nome = x.Regiao.Nome,
                Id = x.Id,
                DataEnvio = x.DataEnvio,
                QuantidadeCriancas = x.QuantidadeCriancas,
                QuantidadeAdultos = x.QuantidadeAdultos,
                QuantidadeIdosos = x.QuantidadeIdosos,
                TotalCriancas = x.TotalCriancas,
                TotalAdultos = x.TotalAdultos,
                TotalIdosos = x.TotalIdosos,
                TotalGeral = x.TotalGeral,
                RegiaoId = x.RegiaoId,
            };

	}
}