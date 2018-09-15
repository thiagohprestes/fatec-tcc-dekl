﻿namespace DEKL.CP.Domain.Entities
{
    public class Credor : EntityBase
    {
        public char Tipo { get; set; }
        public string NumeroSocial { get; set; }
        public Endereco Endereco { get; set; }
        public string Telefone { get; set; }
        public string Contato { get; set; }
        public string TelefoneContato { get; set; }
        public short Prioridade { get; set; }
        public string Email { get; set; }
    }
}
