using System;
using System.Collections.Generic;

namespace WEB_bankomat_statistic_api.Models;

public partial class Bank
{
    public int Id { get; set; }

    public string Bik { get; set; } = null!;

    public string Denomination { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string Home { get; set; } = null!;

    public virtual ICollection<BankHaveBankomat> BankHaveBankomats { get; } = new List<BankHaveBankomat>();
}
