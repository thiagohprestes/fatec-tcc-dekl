﻿namespace DEKL.CP.Domain.Contracts.Entities
{
    public interface IBankAgency
    {
        int Number { get; set; }
        string ManagerName { get; set; }
        string PhoneNumber { get; set; }
        string Email { get; set; }
    }
}
