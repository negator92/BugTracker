using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Windows.Input;

namespace BugTracker
{
    public class ApplicationViewModel : PropertyChangedClass
    {
        public ApplicationViewModel()
        {
            DeleteFileCommand = new RelayCommand(DeleteFile,
                () => Files != null && Files.Count > 0 && !(string.IsNullOrEmpty(SelectedFile) && string.IsNullOrWhiteSpace(SelectedFile)));
            AddFileCommand = new RelayCommand(AddFile);
            CancelCommand = new RelayCommand(Cancel);
            SendReportCommand = new RelayCommand(SendReport,
                () => Files.Count > 0 && !(string.IsNullOrEmpty(Message) && string.IsNullOrWhiteSpace(Message))
                && !(string.IsNullOrEmpty(UserName) && string.IsNullOrWhiteSpace(UserName))
                && Phone > 0);
        }

        public ICommand DeleteFileCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand SendReportCommand { get; set; }
        public ICommand AddFileCommand { get; set; }

        public ObservableCollection<string> Files { get; set; } = new ObservableCollection<string>(new DirectoryInfo(Directory.GetCurrentDirectory()).GetFiles("*.jpg").Select(s => s.FullName)); // Path.GetFileName(
        public string SelectedFile { get; set; }

        public string BiosPC { get; set; } = Environment.MachineName;
        public string UserName { get; set; }
        public int Phone { get; set; }
        public string Message { get; set; }
        public string UserNamePC { get; set; } = Environment.UserName;
        public string[] IPaddressV4 { get; set; } = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(x => x.AddressFamily == AddressFamily.InterNetwork).Select(x => x.ToString()).ToArray();
        public string[] IPaddressV6 { get; set; } = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(x => x.AddressFamily == AddressFamily.InterNetworkV6).Select(x => x.ToString()).ToArray();
        public string Time { get; set; } = DateTime.Now.ToString();
        public string Timezone { get; set; } = TimeZoneInfo.Local.DisplayName;

        private void DeleteFile()
        {
            try
            {
                File.Delete(SelectedFile);
                Files.Remove(SelectedFile);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                throw ex;
            }
        }

        private void Cancel()
            => App.Current.MainWindow.Hide();

        private void AddFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Filter = $"File.jpg | *.jpg",
                Title = "Добавить файл",
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
                if (!(string.IsNullOrEmpty(openFileDialog.FileName) && string.IsNullOrWhiteSpace(openFileDialog.FileName)))
                    Files.Add(openFileDialog.FileName);
        }

        private void SendReport()
        {
        }

        public void GetScreenshot()
        {
            Files.Add(Path.GetFileName("Скриншот 1.jpg"));
        }
    }
}