using System;
using System.Collections.Generic;

namespace WEB_bankomat_statistic_api.Models;

public partial class CassetteBanknote
{
    public int Id { get; set; }

    public int CassetteId { get; set; }

    public long BanknoteId { get; set; }

    public int QuantityBanknote { get; set; }

    public virtual Banknote Banknote { get; set; } = null!;

    public virtual ICollection<Bankomat> Bankomats { get; } = new List<Bankomat>();

    public virtual Cassette Cassette { get; set; } = null!;
}
