using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Drawing;
using System.Drawing.Imaging;
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
                //try to use windows.form feature
                //NotifyIcon notifyIcon = new NotifyIcon();
                //notifyIcon.Icon = SystemIcons.Exclamation;
                //notifyIcon.BalloonTipTitle = "Внимание";
                //notifyIcon.BalloonTipText = "Программа уже запущена";
                //notifyIcon.BalloonTipIcon = ToolTipIcon.Warning;
                //notifyIcon.Visible = true;
                //notifyIcon.ShowBalloonTip(2000, "Внимание", "Программа уже запущена", ToolTipIcon.Warning);
                
                //try to use NotifyIcon from nuget feature
                var ni = (TaskbarIcon)FindName("notifyIcon");
                ni?.ShowBalloonTip("Внимание", "Программа уже запущена", BalloonIcon.Warning);
            }
            else
            {
                WindowState = WindowState.Normal;
                Visibility = Visibility.Visible;
                Rectangle bounds = Screen.GetBounds(System.Drawing.Point.Empty);
                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(System.Drawing.Point.Empty, System.Drawing.Point.Empty, bounds.Size);
                    }
                    bitmap.Save($"Скриншот {DateTime.Now.Ticks}.jpg", ImageFormat.Jpeg);
                }
            }
            Activate();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (WindowState != WindowState.Minimized)
            {
                e.Cancel = true;
                Visibility = Visibility.Hidden;
                WindowState = WindowState.Minimized;
            }
            else
            {
                e.Cancel = false;
            base.OnClosing(e);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
            => Close();
    }
}