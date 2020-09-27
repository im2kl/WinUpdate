using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WUApiLib;

namespace WindowsUpdateAgent
{
    class WuDownloader
    {
        UpdateCollection updatecollection = new UpdateCollection();
        
        // private IUpdate Entry = null;
        public void Download(List<WuModel> updateList)
        {
            foreach(WuModel updatex in updateList)
            {
                
                // convert update back to Iupdate :/
            }

        }
        public void GetWindowsUpdates()
        {

            UpdateSession session = new UpdateSession();
            // Download updates
            UpdateDownloader downloader = session.CreateUpdateDownloader();
            downloader.Updates = updatecollection;
            downloader.Download();

            // Add updates to install list
            UpdateCollection updatesToInstall = new UpdateCollection();
            foreach (IUpdate update in updatecollection)
                if (update.IsDownloaded)
                    updatesToInstall.Add(update);

            // Create installer from list
            IUpdateInstaller installer = session.CreateUpdateInstaller();
            installer.Updates = updatesToInstall;

            // Install updates
            IInstallationResult installationRes = installer.Install();

            // List update result after installation
            for (int i = 0; i < updatesToInstall.Count; i++)
            {
                if (installationRes.GetUpdateResult(i).HResult == 0)
                    Console.WriteLine("Installed : " + updatesToInstall[i].Identity.UpdateID[0]);
                else
                    Console.WriteLine("Failed : " + updatesToInstall[i].Identity.UpdateID[0]);
            }

        }




    }
}
