using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace WindowsUpdateAgent
{
    class RemoteWinUpdateAgent
    {
        static void Main()
        {
            WuDetect searcher = new WuDetect();
            var updatelistjson = searcher.GetUpdateList();
            //var updateList = JsonConvert.DeserializeObject<List<WuModel>>(updatelistjson);


            Console.WriteLine(updatelistjson);


            //Identity updateidentity = new Identity();
            //List<Identity> superceededidentity = new List<Identity>();

            //WuDownloader down = new WuDownloader();
            //down.GetWindowsUpdates();

            Console.ReadKey();
        }
    }
}
