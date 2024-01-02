using System;
using System.Collections.Generic;

namespace SendSmsByKey.Entities;

public partial class NegativeOilNewsForSendingSm
{
    public long Id { get; set; }

    public long NewsId { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? CreatedUserId { get; set; }

    public int TypeId { get; set; }

    public int SendedLevel { get; set; }

    public string? UniqueCode { get; set; }

    public DateTime? SendDate { get; set; }

    public long? UserSendId { get; set; }
}
