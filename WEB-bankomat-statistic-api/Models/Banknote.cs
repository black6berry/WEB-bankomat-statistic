using System;
using System.Collections.Generic;

namespace WEB_bankomat_statistic_api.Models;

public partial class Banknote
{
    public long Id { get; set; }

    public string Currency { get; set; } = null!;

    public string Nominal { get; set; } = null!;

    public string BanknoteSeries { get; set; } = null!;

    public virtual ICollection<CassetteBanknote> CassetteBanknotes { get; } = new List<CassetteBanknote>();
}
