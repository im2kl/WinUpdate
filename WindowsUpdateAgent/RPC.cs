using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Net;

namespace WindowsUpdateAgent
{
    class RPC
    {
        public int RPCport { get; set; }
        
        public void Listen()
        {
            try
            {
                IPAddress ipaddress = IPAddress.Parse("127.0.0.1"); //localhost always
                TcpListener mylist = new TcpListener(ipaddress, RPCport);
                mylist.Start();

                Socket s = mylist.AcceptSocket();

                while (true)
                {
                    byte[] b = new byte[100];
                    int k = s.Receive(b);
                   
                    string comm = Encoding.UTF8.GetString(b, 0, k);

                    ASCIIEncoding asencd = new ASCIIEncoding();
                    s.Send(asencd.GetBytes(cnc(comm)));
                }

                s.Close();
                mylist.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine("FATALError.." + ex.StackTrace);
            }
        }

        private string cnc(string command)
        {
            Console.WriteLine(command);
            switch (command)
            {
                case "detect":
                    return ("detect");
                    
                case "download":
                    return ("down");
                    
                case "install":
                    return ("install");
                   
                case "reboot":
                    return ("install");
                   
                default:
                    return "ERROR";
            }
        }

    }
}
