using System;
using System.Collections.Generic;

namespace ScoutCO_API.Models;

public partial class Child
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Province { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public bool ConsentImage { get; set; }

    public string? Notes { get; set; }

    public int? FkParent1 { get; set; }

    public int? FkParent2 { get; set; }

    public virtual Parent? FkParent1Navigation { get; set; }

    public virtual Parent? FkParent2Navigation { get; set; }

    public DateTime DateRegistration { get; set; }

    public DateTime? DatePaid { get; set; }

    public bool IsPaid { get; set; }
}
