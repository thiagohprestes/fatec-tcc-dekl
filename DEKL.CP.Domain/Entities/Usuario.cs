namespace DEKL.CP.Domain.Entities
{
    public class Usuario : EntityBase
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Nivel { get; set; }
    }
}
