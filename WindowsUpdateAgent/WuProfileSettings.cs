using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;


namespace WindowsUpdateAgent
{
    class WuProfileSettings
    {
        private static readonly string mWuGPO = @"SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate";

        public enum AUOptions
        {
            Default = 0, // Automatic
            Disabled = 1,
            Notification = 2,
            Download = 3,
            //Scheduled = 4,
            ManagedByAdmin = 5
        }

        public void ConfigAU(AUOptions option)
        {
            try
            {
                var subKey = Registry.LocalMachine.CreateSubKey(mWuGPO + @"\AU");
                switch (option)
                {
                    case AUOptions.Default: //Automatic(default)
                        subKey.DeleteValue("NoAutoUpdate", false);
                        subKey.DeleteValue("AUOptions", false);
                        break;
                    case AUOptions.Disabled: //Disabled
                        subKey.SetValue("NoAutoUpdate", 1);
                        subKey.DeleteValue("AUOptions", false);
                        break;
                    case AUOptions.Notification: //Notification only
                        subKey.SetValue("NoAutoUpdate", 0);
                        subKey.SetValue("AUOptions", 2);
                        break;
                    case AUOptions.Download: //Download only
                        subKey.SetValue("NoAutoUpdate", 0);
                        subKey.SetValue("AUOptions", 3);
                        break;
                    //case AUOptions.Scheduled: //Scheduled Installation
                    //    subKey.SetValue("NoAutoUpdate", 0);
                    //    subKey.SetValue("AUOptions", 4);
                    //    break;
                    case AUOptions.ManagedByAdmin: //Managed by Admin
                        subKey.SetValue("NoAutoUpdate", 0);
                        subKey.SetValue("AUOptions", 5);
                        break;
                }

                //if (option == AUOptions.Scheduled)
                //{
                //    if (day != -1) subKey.SetValue("ScheduledInstallDay", day);
                //    if (time != -1) subKey.SetValue("ScheduledInstallTime", time);
                //}
                //else
                //{
                //    subKey.DeleteValue("ScheduledInstallDay", false);
                //    subKey.DeleteValue("ScheduledInstallTime", false);
                //}
            }
            catch { }
        }

        public void HideUpdatePage(bool hide = true)
        {
            try
            {
                var subKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer");
                if (hide)
                    subKey.SetValue("SettingsPageVisibility", "hide:windowsupdate");
                else
                    subKey.DeleteValue("SettingsPageVisibility", false);
            }
            catch { }
        }

        public bool IsUpdatePageHidden()
        {
            try
            {
                var subKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer");
                string value = subKey?.GetValue("SettingsPageVisibility", "").ToString();
                return value.Contains("hide:windowsupdate");
            }
            catch { }
            return false;
        }



    }
}
