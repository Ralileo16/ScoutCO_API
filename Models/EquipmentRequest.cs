using System;
using System.Collections.Generic;

namespace ScoutCO_API.Models;

public partial class EquipmentRequest
{
    public int Id { get; set; }

    public int FkEquipment { get; set; }

    public int FkEmployee { get; set; }

    public DateOnly DateStart { get; set; }

    public DateOnly DateEnd { get; set; }

    public virtual Employee FkEmployeeNavigation { get; set; } = null!;

    public virtual Equipment FkEquipmentNavigation { get; set; } = null!;
}
