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

            UpdateSession session = new UpdateSession();

            Console.WriteLine("session start");
            var updateSearcher = session.CreateUpdateSearcher();
           
            Console.WriteLine("Searching... async");
            var results = updateSearcher.Search("IsInstalled = 0 and IsHidden = 0"); // parameters for search

            UpdateCollection collection = new UpdateCollection();


            ICollection<WUpdate> wupdateCollection = new WUpdateCollection().WUpdateCol;

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
                    
                    for(int i =0;i < upd.KBArticleIDs.Count ;i++)
                    {
                        Console.WriteLine(upd.KBArticleIDs[i]);
                        Console.WriteLine(upd.Identity.UpdateID);

                        Console.WriteLine("Superceeds:");
                        for (int j = 0; j < upd.SupersededUpdateIDs.Count; j++)
                        {
                            Console.WriteLine(upd.SupersededUpdateIDs[j].ToString());
                        }

                        Console.WriteLine("Categories:");
                        var cat = upd.Categories;
                        for (int j = 0; j < cat.Count; j++)
                        {
                            Console.WriteLine(cat[j].Name);
                            Console.WriteLine(cat[j].Description);
                            Console.WriteLine(cat[j].CategoryID);
                        }
                    }

                    collection.Add(upd);
                   //
                   // add to custom class
                   //

                    var json = JsonConvert.SerializeObject(upd.ToString(), Formatting.Indented);
                    Console.WriteLine(json.ToString());
                }
            }

            // https://social.msdn.microsoft.com/Forums/en-US/8789e9e1-444b-4968-930a-1137681b17c4/how-can-i-query-for-an-accurate-and-localized-list-of-windows-updates-installed-on-a-machine-using?forum=csharpgeneral

            Console.ReadKey();
        }
    }
}
