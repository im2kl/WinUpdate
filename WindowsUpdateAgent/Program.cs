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

            var updatelistjson = searcher.GetUpdateList();

            // INstallation
            var updateList = JsonConvert.DeserializeObject<List<WUpdate>>(updatelistjson);


            
            Console.WriteLine(updateList[0].Title);

            //Identity updateidentity = new Identity();
            //List<Identity> superceededidentity = new List<Identity>();


            Console.ReadKey();
        }
    }
}
