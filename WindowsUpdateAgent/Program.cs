using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace WindowsUpdateAgent
{
    class RemoteWinUpdateAgent
    {
        static void Main()
        {
            WuDetect searcher = new WuDetect();

            var updatelistjson = searcher.GetUpdateList();

            // INstallation
            var updateList = JsonConvert.DeserializeObject<List<WuModel>>(updatelistjson);

            WuDownloader updateDownloader = new WuDownloader();
            updateDownloader.Download(updateList);

            
            Console.WriteLine(updateList[0].Title);

            //Identity updateidentity = new Identity();
            //List<Identity> superceededidentity = new List<Identity>();


            Console.ReadKey();
        }
    }
}
