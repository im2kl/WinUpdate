﻿using Newtonsoft.Json;
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


            List<WUpdate> thisisalistttt = new List<WUpdate>();



            foreach (IUpdate5 upd in results.Updates)
            {
                WUpdate updatex = new WUpdate();
                // these can be leveraged for a profile install 
                //if (upd.AutoSelectOnWebSites &&
                //    !upd.InstallationBehavior.CanRequestUserInput &&
                //    !upd.IsInstalled &&
                //    !upd.IsHidden &&
                //    (upd.InstallationBehavior.RebootBehavior == InstallationRebootBehavior.irbNeverReboots))
                //{
                    // Console.WriteLine(upd.Title);
                    Console.WriteLine("Totalupdates-" + results.Updates.Count);

                    for (int i =0;i < upd.KBArticleIDs.Count ;i++)
                    {

                        Identity superceededidentity = new Identity();


                        superceededidentity.UpdateID = upd.Identity.UpdateID;
                        updatex.Identity = superceededidentity;

                        updatex.Title = upd.Title;
                        updatex.KBArticleID = upd.KBArticleIDs[0]; //Must change to list of Updates KBs
                        updatex.Description = upd.Description;
                        updatex.EulaAccepted = upd.EulaAccepted;
                        updatex.RebootRequired = upd.RebootRequired;


                        Console.WriteLine("Total Super-" + upd.SupersededUpdateIDs.Count);
                        for (int j = 0; j < upd.SupersededUpdateIDs.Count; j++)
                        {
                            // Console.WriteLine(upd.SupersededUpdateIDs[j].ToString());
                            //superceededidentity.UpdateID = upd.SupersededUpdateIDs[j].ToString();
                           
                            // superceededidentity.UpdateID = "testsuperseeded";

                            Console.WriteLine(superceededidentity.UpdateID);
                            //updatex.Superseded.Add(superceededidentity);
                        }

                        // NEXT TIME :D
                        Console.WriteLine("Total Categories:" + upd.Categories.Count);
                        //var cat = upd.Categories;
                        for (int j = 0; j < upd.Categories.Count; j++)
                        {
                            Console.WriteLine(upd.Categories[j].Name);
                            //Console.WriteLine(cat[j].Description);
                            //Console.WriteLine(cat[j].CategoryID);
                        }
                    }

                //} // if defenition


                thisisalistttt.Add(updatex);
                //wupdateCollection.Add(updatex);


            }


            Console.WriteLine("\n json collection \n\n");

            var jsonx = JsonConvert.SerializeObject(thisisalistttt, Formatting.Indented);
            Console.WriteLine(jsonx.ToString());

            // https://social.msdn.microsoft.com/Forums/en-US/8789e9e1-444b-4968-930a-1137681b17c4/how-can-i-query-for-an-accurate-and-localized-list-of-windows-updates-installed-on-a-machine-using?forum=csharpgeneral

            Console.ReadKey();
        }
    }
}
