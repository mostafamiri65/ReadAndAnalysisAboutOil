using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAndAnalysis.App.DTOs.News
{
    public class CreateInsertedNewsDto
    {

        public string NewsTitle { get; set; } = null!;

        public string? NewsDescription { get; set; }

        public string NewsUrl { get; set; } = null!;

        public string NewsPublishedDate { get; set; } = null!;
        public int? ManagementSourceId { get; set; }

        public long? EstimateId { get; set; }

        public string? EstimateDscp { get; set; }

        public long? NegativeReasonId { get; set; }

    }
}
