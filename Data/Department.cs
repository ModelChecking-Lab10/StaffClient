using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StaffClient.Data;

public partial class Department
{
    public int DepId { get; set; }

    public string DepName { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
