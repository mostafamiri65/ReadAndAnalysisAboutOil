using System;
using System.Collections.Generic;

namespace SendSmsByKey.Entities;

public partial class TbCaln
{
    public int Id { get; set; }

    public DateOnly? DateEn { get; set; }

    public string? DateEn1 { get; set; }

    public string? DateEn2 { get; set; }

    public string? DateFa1 { get; set; }

    public string? DateFa2 { get; set; }
}
