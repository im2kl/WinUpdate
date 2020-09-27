using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WUApiLib;


namespace WindowsUpdateAgent
{
 	public class WuDetect
	{
		public string GetUpdateList()
		{
			UpdateSession session = new UpdateSession();

			var updateSearcher = session.CreateUpdateSearcher();

			var results = updateSearcher.Search("IsInstalled = 0 and IsHidden = 0"); // parameters for search

			List<WuModel> updateList = new List<WuModel>();

			foreach (IUpdate5 upd in results.Updates)
			{
				WuModel updatex = new WuModel();
				// these can be leveraged for a profile install 
				//if (upd.AutoSelectOnWebSites &&
				//    !upd.InstallationBehavior.CanRequestUserInput &&
				//    !upd.IsInstalled &&
				//    !upd.IsHidden &&
				//    (upd.InstallationBehavior.RebootBehavior == InstallationRebootBehavior.irbNeverReboots))
				//{
				for (int i = 0; i < upd.KBArticleIDs.Count; i++)
				{
					Identity updateidentity = new Identity();
					List<Identity> superceededidentity = new List<Identity>();

					updateidentity.UpdateID = upd.Identity.UpdateID;
					updatex.Identity = updateidentity;

					updatex.Title = upd.Title;
					updatex.KBArticleID = upd.KBArticleIDs[0]; //Must change to list of Updates KBs
					updatex.Description = upd.Description;
					updatex.EulaAccepted = upd.EulaAccepted;
					updatex.RebootRequired = upd.RebootRequired;

					for (int j = 0; j < upd.SupersededUpdateIDs.Count; j++)
					{
                        Identity supers = new Identity
                        {
                            UpdateID = upd.SupersededUpdateIDs[j].ToString()
                        };
                        superceededidentity.Add(supers);
					}
					updatex.Superseded = superceededidentity;

					//var cat = upd.Categories;
					for (int j = 0; j < upd.Categories.Count; j++)
					{
						//Console.WriteLine(upd.Categories[j].Name);
						//Console.WriteLine(cat[j].Description);
						//Console.WriteLine(cat[j].CategoryID);
					}

					updatex.AdminInstallAccepted = false;
				}
				//} // if defenition
				updateList.Add(updatex);
			}

			var jsonx = JsonConvert.SerializeObject(updateList, Formatting.Indented);

			return jsonx.ToString();
		}

	}
}
