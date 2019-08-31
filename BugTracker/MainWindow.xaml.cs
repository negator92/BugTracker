using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace BugTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ShowInTaskbar = true;
            WindowState = System.Windows.WindowState.Minimized;
            Visibility = System.Windows.Visibility.Hidden;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = false;
            Visibility = Visibility.Hidden;
        }

        private void CreateReport_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState != WindowState.Minimized)
            {
                NotifyIcon notifyIcon = new NotifyIcon();
                notifyIcon.Icon = SystemIcons.Exclamation;
                notifyIcon.BalloonTipTitle = "Внимание";
                notifyIcon.BalloonTipText = "Программа уже запущена";
                notifyIcon.BalloonTipIcon = ToolTipIcon.Warning;
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(1000);
                notifyIcon.Dispose();
            }
            else
            {
                WindowState = WindowState.Normal;
                Visibility = Visibility.Visible;
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            if (WindowState != WindowState.Minimized)
            {
                e.Cancel = false;
                Visibility = Visibility.Hidden;
                WindowState = WindowState.Minimized;
            }
            else
                Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
            => Close();
    }
}