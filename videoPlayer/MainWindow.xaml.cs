using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VhsFileWatcher;

namespace videoPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        public MainWindow()
        {

            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            VideoPLayer.Loaded += VideoPLayer_Loaded;
            VideoPLayer.MediaEnded += VideoPLayer_MediaEnded;
            var watcher = new ObserverFile(@"C:\OScfg\FrontEndAppFiles\SelectedGame.vhs");
#if DEBUG
            //ideoPLayer.Source = new Uri(@"C:\Art\Videos\Default.wmv");
#endif

#if LOCALDEBUG
            VideoPLayer.Source = new Uri(@"D:\Art\Videos\Default.wmv");
#endif

            watcher.FileChanged += Watcher_FileChanged;
            DataContext = new VideoBinding();
            OveryLayAnimation overyLayAnimation = new OveryLayAnimation(this);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
          
            this.Width = 800;
            this.Height = 600;
            this.Topmost = true;
            var width = this.ActualWidth;
            var height = this.ActualHeight;
           
        }

        private void Watcher_FileChanged(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                VideoPLayer.Stop();
               
                FrontEndEvents.OnGameChanged(this, new EventArgs());
            });
          
        }

        private void VideoPLayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            VideoPLayer.Position = new TimeSpan(0, 0, 0);
            VideoPLayer.Play();
        }

        private void VideoPLayer_Loaded(object sender, RoutedEventArgs e)
        {
            VideoPLayer.Source = VideoBinding.Instance.ChangeVideo();
            //VideoBinding.Instance.ChangeImageFlyer(ImageFlyer);
            //VideoBinding.Instance.ChangeImageBackGround(ImageBackGround);
           
        }
    }
}
