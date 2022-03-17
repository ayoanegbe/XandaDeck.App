using DnsClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace XandaApp.App.Services
{
    public static class DnsCheck
    {
        public static async Task<bool> IsEmailValidAsync(string emailAddress)
        {
            string host = emailAddress.Split(Convert.ToChar("@"))[1];
            return await CheckDnsEntriesAsync(host);
        }

        public static bool IsEmailValid(string emailAddress)
        {
            string host = emailAddress.Split(Convert.ToChar("@"))[1];
            return CheckDnsEntries(host);
        }

        private static async Task<bool> CheckDnsEntriesAsync(string domain)
        {
            try
            {
                var lookup = new LookupClient();

                var result = await lookup.QueryAsync(domain, QueryType.MX).ConfigureAwait(false);

                var records = result.Answers.Where(record => record.RecordType == DnsClient.Protocol.ResourceRecordType.MX);
                return records.Any();
            }
            catch (DnsResponseException)
            {
                return false;
            }
        }

        private static bool CheckDnsEntries(string domain)
        {
            try
            {
                var lookup = new LookupClient(); 

                var result = lookup.Query(domain, QueryType.MX);

                var records = result.Answers.Where(record => record.RecordType == DnsClient.Protocol.ResourceRecordType.MX);
                return records.Any();
            }
            catch (DnsResponseException)
            {
                return false;
            }
        }

        public static string GetLocalIPAddress()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
            }
            catch (Exception)
            {
                /*throw new Exception("No network adapters with an IPv4 address in the system!")*/
                return null;
            }

            return null;
        }

        public static string GetLocalIPAddress(string hostname)
        {

            try
            {
                IPHostEntry iphost = Dns.GetHostEntry(hostname);
                foreach (var ip in iphost.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
            }
            catch (Exception)
            {
                /*throw new Exception("No network adapters with an IPv4 address in the system!")*/
                return null;
            }

            return null;
        }

        public static string GetHostName()
        {
            return Dns.GetHostName(); 
        }

    }   
}

