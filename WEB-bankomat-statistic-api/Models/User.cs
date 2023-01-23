using System;
using System.Collections.Generic;

namespace WEB_bankomat_statistic_api.Models;

public partial class User
{
    public long Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Patronomic { get; set; } = null!;

    public DateTime Dateborn { get; set; }

    public string PassportNumber { get; set; } = null!;

    public string PassportSeries { get; set; } = null!;

    public DateTime PassportIssueDate { get; set; }

    public string RegistrationAddress { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string Home { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool BankEmploye { get; set; }

    public int RoleId { get; set; }

    public virtual ICollection<BankAccountClient> BankAccountClients { get; } = new List<BankAccountClient>();

    public virtual Role Role { get; set; } = null!;
}
