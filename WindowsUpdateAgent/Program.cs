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
            Console.WriteLine("start");
            WuProfileSettings settings = new WuProfileSettings();

            if (settings.IsUpdatePageHidden())
            {
                Console.WriteLine("itshidden");
                settings.HideUpdatePage(true);
            }

            var x = settings.GetAU();
            Console.WriteLine(x);

            settings.ConfigAU(WuProfileSettings.AUOptions.Disabled);

            var y = settings.GetAU();
            Console.WriteLine(y);

            Thread.Sleep(4000);
            Console.WriteLine("set default");
            
            settings.ConfigAU(WuProfileSettings.AUOptions.Default);

            Console.WriteLine("sleep");
            Thread.Sleep(10000);
            Console.WriteLine("hello");



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
