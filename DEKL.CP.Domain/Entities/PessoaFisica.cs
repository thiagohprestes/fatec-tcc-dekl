using System;

namespace DEKL.CP.Domain.Entities
{
    public class PessoaFisica : EntityBase
    {
        public string Nome { get; set; }
        public string RG { get; set; }
        public string Genero { get; set; }
        public DateTime? DataNascimento { get; set; }
    }
}
