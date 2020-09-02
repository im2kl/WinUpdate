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

            var lookup = session.CreateUpdateSearcher();
            
            Console.WriteLine("Searching... async");
            var results = lookup.Search(""); // parameters for search

            foreach (IUpdate upd in results.Updates)
            {
                // these can be leveraged for a profile install 
                if (upd.AutoSelectOnWebSites &&
                    !upd.InstallationBehavior.CanRequestUserInput &&
                    !upd.IsInstalled &&
                    !upd.IsHidden &&
                    (upd.InstallationBehavior.RebootBehavior == InstallationRebootBehavior.irbNeverReboots))
                {
                    Console.WriteLine(upd.Title);
                }
            }
            Console.ReadKey();
        }
    }
}
