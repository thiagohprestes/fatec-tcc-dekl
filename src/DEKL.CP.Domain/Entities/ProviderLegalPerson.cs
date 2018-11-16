namespace DEKL.CP.Domain.Entities
{
    public class ProviderLegalPerson : Provider
    {
        public string CorporateName { get; set; }
        public string CNPJ { get; set; }
        public string InscricaoEstadual { get; set; }
        public string InscricaoMunicipal { get; set; }
    }
}
