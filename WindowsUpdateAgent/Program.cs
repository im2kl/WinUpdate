using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WUApiLib;

namespace WindowsUpdateAgent
{
    class WinUpdate
    {
        static void Main()
        {
            RemoteWindowsUpdater searcher = new RemoteWindowsUpdater();

            Console.WriteLine(searcher.GetUpdateList());


            Console.ReadKey();
        }
    }
}
