using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DEKL.CP.Domain.Contracts.Entities;

namespace DEKL.CP.Domain.Entities
{
    public class ApplicationUser : IEntityBase
    {
        public int Id { get; set; }
        public virtual DateTime AddedDate { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
        public virtual bool Active { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual ICollection<Audit> Audits { get; set; } = new Collection<Audit>();
        public virtual string Email { get; set; }
        public virtual bool EmailConfirmed { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string SecurityStamp { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual bool PhoneNumberConfirmed { get; set; }
        public virtual bool TwoFactorEnabled { get; set; }
        public virtual DateTime? LockoutEndDateUtc { get; set; }
        public virtual bool LockoutEnabled { get; set; }
        public virtual int AccessFailedCount { get; set; }
        public virtual string UserName { get; set; }
    }
}
