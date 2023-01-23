using System;
using System.Collections.Generic;

namespace WEB_bankomat_statistic_api.Models;

public partial class TypeCard
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public decimal WithdrawalLimit { get; set; }

    public virtual ICollection<Card> Cards { get; } = new List<Card>();
}
