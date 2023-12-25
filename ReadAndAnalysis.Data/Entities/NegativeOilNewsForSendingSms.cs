using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAndAnalysis.Data.Entities
{
    public class NegativeOilNewsForSendingSms
    {
        public long Id { get; set; }
        public long NewsId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedUserId { get; set; }
    }
}
