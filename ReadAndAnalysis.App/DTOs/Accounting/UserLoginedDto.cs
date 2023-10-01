using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAndAnalysis.App.DTOs.Accounting
{
    public class UserLoginedDto
    {
        public long Id { get; set; }
        public string? Mobile { get; set; }
        public string? Fullname { get; set; }

    }
}
