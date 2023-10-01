using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAndAnalysis.App.DTOs.News
{
    public class KeyWordsUsedInOilNewsDto
    {
        public long NewsId { get; set; }
        public List<string>? MainKeys { get; set; }
        public List<string>? SecondStepKeys { get; set; }

    }
}
