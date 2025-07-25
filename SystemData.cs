﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JYLIB
{
    public class SystemData
    {

        public string SQLConnectionstring(string server ,string DataBase) {

            
             

            return $@"Data Source={server};Integrated Security=True;Connect Timeout=30;Encrypt=False;Initial Catalog={DataBase}";
        
        }

        public string AccessConnectionString(string filepath)
        {

            return @$"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filepath};Persist Security Info=False;";
        }

        public static string HostName() {
           


            string hostName = Dns.GetHostName();

            return hostName;


        }

        public static string LocalIPaddress()
        {
            string ipAddress = string.Empty;


            string hostName = Dns.GetHostName();





            IPAddress[] ipAddresses = Dns.GetHostAddresses(hostName);


            foreach (IPAddress ip in ipAddresses)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddress = ip.ToString();
                    break;
                }
            }

            return ipAddress;
        }



    }
}
