using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BugTracker
{
    public class ApplicationViewModel : PropertyChangedClass
    {
        public TextBox TextBoxProcess { get; set; }

        public ApplicationViewModel()
        {
            KillProcessCommand = new RelayCommand(KillProcess,
                () => !(string.IsNullOrEmpty(BiosPC) && string.IsNullOrWhiteSpace(BiosPC)));
        }

        public ICommand KillProcessCommand { get; set; }

        public string BiosPC { get; set; } = Environment.MachineName;
        public string UserName { get; set; }
        public int Phone { get; set; }
        public string Message { get; set; }
        public string UserNamePC { get; set; } = Environment.UserName;
        public string[] IPaddressV4 { get; set; } = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(x => x.AddressFamily == AddressFamily.InterNetwork).Select(x => x.ToString()).ToArray();
        public string[] IPaddressV6 { get; set; } = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(x => x.AddressFamily == AddressFamily.InterNetworkV6).Select(x => x.ToString()).ToArray();
        public string Time { get; set; } = DateTime.Now.ToString();
        public string Timezone { get; set; } = TimeZoneInfo.Local.DisplayName;

        //string ip()
        //{
        //    var host = Dns.GetHostEntry(Dns.GetHostName());
        //    foreach (var ip in host.AddressList)
        //    {
        //        if (ip.AddressFamily == AddressFamily.InterNetwork)
        //        {
        //            return ip.ToString();
        //        }
        //    }
        //    throw new Exception("Local IP Address Not Found!");
        //}

        private void KillProcess()
        {
            if (BiosPC.Contains(".exe"))
                BiosPC.Replace(".exe", "");
            try
            {
                Process[] processNames = Process.GetProcessesByName(BiosPC);

                if (processNames.Length == 0)
                    MessageBox.Show($"{BiosPC} not found!");
                else
                {
                    foreach (Process process in processNames)
                        process.Kill();
                    MessageBox.Show($"{BiosPC} killed!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error, {ex.Message}, {ex.StackTrace}");
            }
            finally
            {
                TextBoxProcess.SelectAll();
            }
        }
    }
}