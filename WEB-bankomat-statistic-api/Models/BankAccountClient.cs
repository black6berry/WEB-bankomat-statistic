using System;
using System.Collections.Generic;

namespace WEB_bankomat_statistic_api.Models;

public partial class BankAccountClient
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string AccountNumber { get; set; } = null!;

    public long CardId { get; set; }

    public int TypeAccountId { get; set; }

    public DateTime DateOpen { get; set; }

    public DateTime DateClose { get; set; }

    public decimal ValueMoney { get; set; }

    public long OperationId { get; set; }

    public virtual Card Card { get; set; } = null!;

    public virtual Operation Operation { get; set; } = null!;

    public virtual TypeAccount TypeAccount { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
