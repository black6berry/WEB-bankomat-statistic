using System;
using System.Collections.Generic;

namespace WEB_bankomat_statistic_api.Models;

public partial class Card
{
    public long Id { get; set; }

    public string Number { get; set; } = null!;

    public string Day { get; set; } = null!;

    public string Month { get; set; } = null!;

    public string Year { get; set; } = null!;

    public string Cvv { get; set; } = null!;

    public int TypeCardId { get; set; }

    public virtual ICollection<BankAccountClient> BankAccountClients { get; } = new List<BankAccountClient>();

    public virtual TypeCard TypeCard { get; set; } = null!;
}
