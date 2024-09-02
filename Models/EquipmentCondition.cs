using System;
using System.Collections.Generic;

namespace ScoutCO_API.Models;

public partial class EquipmentCondition
{
    public int Id { get; set; }

    public string Condition { get; set; } = null!;

    public virtual ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();
}
