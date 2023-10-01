using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAndAnalysis.App.DTOs.News
{
    public class OilNewsDto
    {
        public long Id { get; set; }
        public string? Title { get; set; }

        public string? LinkUrl { get; set; }

        public string? Description { get; set; }

        public string? PublishedDate { get; set; }

        public string? Enclosure { get; set; }

        public string? Author { get; set; }

        public string? Category { get; set; }

       
    }
}
