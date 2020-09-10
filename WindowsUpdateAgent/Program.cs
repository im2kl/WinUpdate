using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WUApiLib;

namespace WindowsUpdateAgent
{
    class WinUpdate
    {
        static void Main()
        {

            UpdateSession session = new UpdateSession();

            Console.WriteLine("session start");
            var updateSearcher = session.CreateUpdateSearcher();
           
            Console.WriteLine("Searching... async");
            var results = updateSearcher.Search(""); // parameters for search

            UpdateCollection collection = new UpdateCollection();

            foreach (IUpdate5 upd in results.Updates)
            {
                // these can be leveraged for a profile install 
                if (upd.AutoSelectOnWebSites &&
                    !upd.InstallationBehavior.CanRequestUserInput &&
                    !upd.IsInstalled &&
                    !upd.IsHidden &&
                    (upd.InstallationBehavior.RebootBehavior == InstallationRebootBehavior.irbNeverReboots))
                {
                    Console.WriteLine(upd.Title);
                    collection.Add(upd);

                    var json = JsonConvert.SerializeObject(upd.ToString(), Formatting.Indented);
                    Console.WriteLine(json.ToString());
                }
            }


            Console.ReadKey();
        }
    }
}
