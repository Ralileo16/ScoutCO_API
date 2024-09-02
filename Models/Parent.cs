using System;
using System.Collections.Generic;

namespace ScoutCO_API.Models;

public partial class Parent
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Gender { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Child> ChildFkParent1Navigations { get; set; } = new List<Child>();
    
    public virtual ICollection<Child> ChildFkParent2Navigations { get; set; } = new List<Child>();
}
