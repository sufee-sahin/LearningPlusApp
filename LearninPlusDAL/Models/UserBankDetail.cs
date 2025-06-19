using System;
using System.Collections.Generic;

namespace LearninPlusDAL.Models;

public partial class UserBankDetail
{
    public int BankId { get; set; }

    public int UserId { get; set; }

    public string AccountNumber { get; set; }

    public string AccountHolderName { get; set; }

    public string BankName { get; set; }

    public string IfscCode { get; set; }

    public bool? IsDefault { get; set; }

    public decimal? AvailableBalance { get; set; }

    public virtual User User { get; set; }
}
