using System;
using System.Collections.Generic;

namespace WEB_bankomat_statistic_api.Models;

public partial class TypeCassette
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Cassette> Cassettes { get; } = new List<Cassette>();
}
