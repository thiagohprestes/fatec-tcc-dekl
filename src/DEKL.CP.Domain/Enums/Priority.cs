using System.ComponentModel;

namespace DEKL.CP.Domain.Enums
{
    public enum Priority
    {
        [Description("Alta")]
        High,

        [Description("Média")]
        Medium,

        [Description("Baixa")]
        Low
    }
}
