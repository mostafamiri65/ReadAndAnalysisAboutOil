using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAndAnalysis.App.DTOs.Accounting
{
    public class CreateUserDto
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Mobile { get; set; }
        public required string FullName { get; set; }
        public long? LoginUserId { get; set; }
        public List<long> RoleIds { get; set; }

    }
}
