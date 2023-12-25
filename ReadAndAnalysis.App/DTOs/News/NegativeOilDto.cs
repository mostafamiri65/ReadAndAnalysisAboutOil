using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAndAnalysis.App.DTOs.News
{
    public class NegativeOilDto
    {
        public long Id { get; set; }
        public long NewsId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
