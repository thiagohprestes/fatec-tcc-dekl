using System;
using DEKL.CP.Domain.Contracts.Entities;

namespace DEKL.CP.Domain.Entities
{
    public abstract class EntityBase : IEntityBase
    {
        public int Id { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public bool Active { get; set; } = true;
    }
}
