using System;
using System.Collections.Generic;

namespace WEB_bankomat_statistic_api.Models;

public partial class TypeAccount
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<BankAccountClient> BankAccountClients { get; } = new List<BankAccountClient>();
}
