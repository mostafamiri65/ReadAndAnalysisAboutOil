using System;
using System.Collections.Generic;

namespace ReadAndAnalysis.Data.Entities;

public partial class EstimateNews
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<EvaluatedResult> EvaluatedResults { get; set; } = new List<EvaluatedResult>();
}
