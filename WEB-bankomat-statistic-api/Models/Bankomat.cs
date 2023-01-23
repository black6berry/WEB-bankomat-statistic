using System;
using System.Collections.Generic;

namespace WEB_bankomat_statistic_api.Models;

public partial class Bankomat
{
    public long Number { get; set; }

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string Home { get; set; } = null!;

    public int StatusId { get; set; }

    public int CasseteBanknoteId { get; set; }

    public virtual ICollection<BankHaveBankomat> BankHaveBankomats { get; } = new List<BankHaveBankomat>();

    public virtual CassetteBanknote CasseteBanknote { get; set; } = null!;

    public virtual ICollection<Operation> Operations { get; } = new List<Operation>();

    public virtual Status Status { get; set; } = null!;
}
