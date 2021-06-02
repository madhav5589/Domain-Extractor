using System;
using System.Net;
using System.Net.Sockets;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            // string input = "69.59.196.211"; // stackoverflow IP address
            string input = "https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-5.0";
            bool isInputIPAddress = IsIPAddress(input);
            IPHostEntry hostInfo;

            try
            {
                if (isInputIPAddress)
                {
                    Console.WriteLine("GIVEN INPUT IS IPADDRESS.!!!");
                    IPAddress ip = IPAddress.Parse(input);
                    hostInfo = Dns.GetHostEntry(ip);
                }
                else
                {
                    string url = ExtractDomainNameFromURL(input);
                    Console.WriteLine("Extracted Domain URL is: " + url);
                    hostInfo = Dns.GetHostEntry(url); // DNS value
                }

                // Get the IP address list that resolves to the host names contained in the
                // Alias property.
                IPAddress[] address = hostInfo.AddressList;
                // Get the alias names of the addresses in the IP address list.
                String[] alias = hostInfo.Aliases;

                Console.WriteLine("Host name : " + hostInfo.HostName);
                Console.WriteLine("\nAliases : ");
                for (int index = 0; index < alias.Length; index++)
                {
                    Console.WriteLine(alias[index]);
                }
                Console.WriteLine("\nIP Address list :");
                for (int index = 0; index < address.Length; index++)
                {
                    Console.WriteLine(address[index]);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException caught!!!");
                Console.WriteLine("Source : " + e.Source);
                Console.WriteLine("Message : " + e.Message);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException caught!!!");
                Console.WriteLine("Source : " + e.Source);
                Console.WriteLine("Message : " + e.Message);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("NullReferenceException caught!!!");
                Console.WriteLine("Source : " + e.Source);
                Console.WriteLine("Message : " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught!!!");
                Console.WriteLine("Source : " + e.Source);
                Console.WriteLine("Message : " + e.Message);
            }
        }

        public static string ExtractDomainNameFromURL(string Url)
        {
            if (Url.Contains(@"://"))
                Url = Url.Split(new string[] { "://" }, 2, StringSplitOptions.None)[1];

            return Url.Split('/')[0];
        }

        private static bool IsIPAddress(string ipAddress)
        {
            bool retVal;

            try
            {
                IPAddress address;
                retVal = IPAddress.TryParse(ipAddress, out address);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occurred while checking the validity of IPAddress. Exception is: "+ ex.Message);
            }
            return retVal;
        }
    }
}
