using System;
using System.Collections.Generic;

namespace WEB_bankomat_statistic_api.Models;

public partial class TypeOperation
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Operation> Operations { get; } = new List<Operation>();
}
