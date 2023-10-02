using ReadAndAnalysis.App.Extensions;
using ReadAndAnalysis.Data.Entities;

namespace ReadAndAnalysis.App.DTOs.News
{
    public class HomeControllerIndexDto
    {
        public string StartDate { get; set; } = "1402/01/01";
        public string EndDate { get; set; } = DateTime.Now.ToShamsi();
        public List<NewsRelevance>? Relevances { get; set; }
        public string RelevanceTitle { get; set; } = string.Empty;
        public int PosetiveCount { get; set; }
        public int NegativeCount { get; set; }
        public int NeutralCount { get; set; }
        public int RelevanceCount { get; set; }
    }
}
