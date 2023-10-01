using System;
using System.Collections.Generic;

namespace ReadAndAnalysis.Data.Entities;

public partial class NewsRelevance
{
    public long Id { get; set; }

    public string? Title { get; set; }

    public virtual ICollection<EvaluatedResult> EvaluatedResults { get; set; } = new List<EvaluatedResult>();
}
