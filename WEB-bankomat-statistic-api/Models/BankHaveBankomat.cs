using System;
using System.Collections.Generic;

namespace WEB_bankomat_statistic_api.Models;

public partial class BankHaveBankomat
{
    public int Id { get; set; }

    public int BankId { get; set; }

    public long BankomatId { get; set; }

    public virtual Bank Bank { get; set; } = null!;

    public virtual Bankomat Bankomat { get; set; } = null!;
}
