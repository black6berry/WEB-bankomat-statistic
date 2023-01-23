using System;
using System.Collections.Generic;

namespace WEB_bankomat_statistic_api.Models;

public partial class Cassette
{
    public int Id { get; set; }

    public string Number { get; set; } = null!;

    public int TypeCassetteId { get; set; }

    public virtual ICollection<CassetteBanknote> CassetteBanknotes { get; } = new List<CassetteBanknote>();

    public virtual TypeCassette TypeCassette { get; set; } = null!;
}
