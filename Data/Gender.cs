using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StaffClient.Data;

public partial class Gender
{
    public int GenId { get; set; }

    public string GenName { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
