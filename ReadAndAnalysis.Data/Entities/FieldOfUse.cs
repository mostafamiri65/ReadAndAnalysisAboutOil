using System;
using System.Collections.Generic;

namespace ReadAndAnalysis.Data.Entities;

public partial class FieldOfUse
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long? ParentId { get; set; }

    public virtual ICollection<EvaluatedResult> EvaluatedResults { get; set; } = new List<EvaluatedResult>();
}
