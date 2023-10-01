using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAndAnalysis.App.DTOs.Accounting
{
    public class UsersDto
    {
        public long Id { get; set; }
        public string? Username { get; set; }
        public string? FullName { get; set; }
        public string? Mobile { get; set; }
    }
}
