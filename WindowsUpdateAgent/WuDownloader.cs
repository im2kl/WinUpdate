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

        public void Download(List<WuModel> updateList)
        {

            for (int i = 0; i < updateList.Count; i++ )
            {
                Console.WriteLine(updateList.Count);
            }


        }





    }
}
