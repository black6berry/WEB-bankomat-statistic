using System;
using System.Collections.Generic;

namespace WEB_bankomat_statistic_api.Models;

public partial class Operation
{
    public long Id { get; set; }

    public long BankomatNumber { get; set; }

    public int TypeOperationId { get; set; }

    public DateTime DateOperation { get; set; }

    public bool Commission { get; set; }

    public decimal ValueMoney { get; set; }

    public virtual ICollection<BankAccountClient> BankAccountClients { get; } = new List<BankAccountClient>();

    public virtual Bankomat BankomatNumberNavigation { get; set; } = null!;

    public virtual TypeOperation TypeOperation { get; set; } = null!;
}
