using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAndAnalysis.App.DTOs.News
{
    public class SelectedNewsDto
    {
        public long Id { get; set; }
        public long NewsId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public string? PublishedDate { get; set; }
        public string? TypeTitle { get; set; }
        public int TypeId { get; set; }
        public string? LinkeGenerated { get; set; }
        public int SendLevelId { get; set; }
    }
}
