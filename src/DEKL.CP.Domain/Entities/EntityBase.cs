using System;
using DEKL.CP.Domain.Contracts.Repositories;

namespace DEKL.CP.Domain.Entities
{
    public abstract class EntityBase : IEntityBase
    {
        public int Id { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Active { get; set; } = true;
    }
}
