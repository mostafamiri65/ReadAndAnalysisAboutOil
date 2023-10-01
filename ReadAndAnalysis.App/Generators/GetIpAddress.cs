using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ReadAndAnalysis.App.Generators
{
    public static class GetIpAddress
    {
        public static string? GetIp()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            return Convert.ToString(ipHostInfo.AddressList.FirstOrDefault(address =>
                address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork));

        }
    }
}
