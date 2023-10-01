using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAndAnalysis.App.DTOs.News
{
    public class OilNewsListDto
    {
        public long Id { get; set; }
        public string? Title { get; set; }

        public string? LinkUrl { get; set; }
        public string? PublishedDate { get; set; }
        public string? Description { get; set; }
        public string? TitleUrl { get; set;}
        public DateTime CreateDate { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
