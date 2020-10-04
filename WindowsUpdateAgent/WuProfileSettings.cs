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
                var subKey = Registry.LocalMachine.CreateSubKey(mWuGPO + @"\AU", true);
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
        public AUOptions GetAU()
        {
            AUOptions option = AUOptions.Default;
            try
            {
                var subKey = Registry.LocalMachine.OpenSubKey(mWuGPO + @"\AU", false);
                object value_no = subKey == null ? null : subKey.GetValue("NoAutoUpdate");
                if (value_no == null || (int)value_no == 0)
                {
                    object value_au = subKey == null ? null : subKey.GetValue("AUOptions");
                    switch (value_au == null ? 0 : (int)value_au)
                    {
                        case 0: option = AUOptions.Default; break;
                        case 2: option = AUOptions.Notification; break;
                        case 3: option = AUOptions.Download; break;
                        //case 4: option = AUOptions.Scheduled; break;
                        case 5: option = AUOptions.ManagedByAdmin; break;
                    }
                }
                else
                {
                    option = AUOptions.Disabled;
                }

                //object value_day = subKey.GetValue("ScheduledInstallDay");
                //day = value_day != null ? (int)value_day : 0;
                //object value_time = subKey.GetValue("ScheduledInstallTime");
                //time = value_time != null ? (int)value_time : 0;
            }
            catch { }
            return option;
        }

        public void ConfigDriverAU(int option)
        {
            try
            {
                var subKey = Registry.LocalMachine.CreateSubKey(mWuGPO, true);
                switch (option)
                {
                    case 0: // CheckState.Unchecked:
                        subKey.SetValue("ExcludeWUDriversInQualityUpdate", 1);
                        break;
                    case 2: // CheckState.Indeterminate:
                        subKey.DeleteValue("ExcludeWUDriversInQualityUpdate", false);
                        break;
                    case 1: // CheckState.Checked:
                        subKey.SetValue("ExcludeWUDriversInQualityUpdate", 0);
                        break;
                }
            }
            catch { }
        }

         public int GetDriverAU()
        {
            try
            {
                var subKey = Registry.LocalMachine.OpenSubKey(mWuGPO, false);
                object value_drv = subKey == null ? null : subKey.GetValue("ExcludeWUDriversInQualityUpdate");

                if (value_drv == null)
                    return 2; // CheckState.Indeterminate;
                else if ((int)value_drv == 1)
                    return 0; // CheckState.Unchecked;
                else //if ((int)value_drv == 0)
                    return 1; // CheckState.Checked
            }
            catch { }
            return 2;
        }
        public void HideUpdatePage(bool hide)
        {
            try
            {
                var subKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer", true);
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
                string value = subKey == null ? null : subKey.GetValue("SettingsPageVisibility", "").ToString();
                return value.Contains("hide:windowsupdate");
            }
            catch { }
            return false;
        }


    }
}
