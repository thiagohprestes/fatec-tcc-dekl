using System;

namespace DEKL.CP.Domain.Entities
{
    public class Audit : EntityBase
    {
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }
        public string Event { get; set; }
        public DateTime DateTime { get; set; }
    }
}