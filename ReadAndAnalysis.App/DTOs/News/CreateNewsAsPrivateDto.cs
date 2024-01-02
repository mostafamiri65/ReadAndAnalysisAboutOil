using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAndAnalysis.App.DTOs.News
{
    public class CreateNewsAsPrivateDto
    {
        public string? Title { get; set; }

        public string? LinkUrl { get; set; }

        public string? Description { get; set; }

        public string? PublishedDate { get; set; }

        public int TypeId { get; set; }
        public long EstimateId { get; set; }

        public long FieldOfUseId { get; set; }

        public long StatisticalSourceId { get; set; }
        public long? RelevanceId { get; set; }
    }
}
