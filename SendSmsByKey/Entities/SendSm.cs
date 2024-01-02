using System;
using System.Collections.Generic;

namespace SendSmsByKey.Entities;

public partial class SendSm
{
    public int Id { get; set; }

    public string? Mobile { get; set; }

    public string? SendedText { get; set; }

    public string? Detail { get; set; }

    public bool? IsSended { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? SendDate { get; set; }

    public long? NotOilNewsForSendingSmsId { get; set; }
}
