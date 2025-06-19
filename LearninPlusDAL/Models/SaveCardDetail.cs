using System;
using System.Collections.Generic;

namespace LearninPlusDAL.Models;

public partial class SaveCardDetail
{
    public int CardId { get; set; }

    public int UserId { get; set; }

    public string AccountNumber { get; set; }

    public string CardNumber { get; set; }

    public string CardHolderName { get; set; }

    public DateOnly ExpiryDate { get; set; }

    public int Cvv { get; set; }

    public bool? IsDefault { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User User { get; set; }
}
