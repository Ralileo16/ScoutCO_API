using System;
using System.Collections.Generic;

namespace ScoutCO_API.Models;

public partial class Equipment
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int FkCondtion { get; set; }

    public int? FkEmployeeLastUsed { get; set; }

    public virtual ICollection<EquipmentRequest> EquipmentRequests { get; set; } = new List<EquipmentRequest>();

    public virtual EquipmentCondition FkCondtionNavigation { get; set; } = null!;
}
