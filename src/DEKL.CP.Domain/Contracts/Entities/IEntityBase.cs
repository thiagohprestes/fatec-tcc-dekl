using System;

namespace DEKL.CP.Domain.Contracts.Entities
{
    public interface IEntityBase
    {
        int Id { get; set; }
        DateTime AddedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
        bool Active { get; set; }
    }
}
