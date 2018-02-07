using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;

namespace videoPlayer
{
    public class VideoBinding : INotifyPropertyChanged
    {
        static VideoBinding _Instance;
        public static VideoBinding Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new VideoBinding();
                    return _Instance;
                }
                return _Instance;
            }
        }
        string VideosPath { get; set; }
        string CurrentHighlightedGame { get; set; }
        string ImageMarqueePath { get; set; }
        string ImageFlyerPath { get; set; }
        BitmapImage bitmapFlyer { get; set; }
        string ImageSnap { get; set; }
        string SelectedGame;
        public VideoBinding()
        {
            VideosPath = @"c:\Art\Videos";
            SelectedGame = @"c:\OScfg\FrontEndAppFiles\SelectedGame.vhs";
#if LOCALDEBUG
            VideosPath = @"D:\Art\Videos";
            SelectedGame = @"C:\OScfg\FrontEndAppFiles\SelectedGame.vhs";
#endif
            bitmapFlyer = new BitmapImage();

        }

        public Uri ChangeVideo()
        {

            var videoToload = string.Empty;
            try
            {
                using (var file = new StreamReader(File.Open(SelectedGame, FileMode.Open)))
                {
                    videoToload = file.ReadToEnd();
                    CurrentHighlightedGame = videoToload;
                }
                var videoFile = string.Format(@"{0}\\{1}.wmv", VideosPath, videoToload);
                if (!File.Exists(videoFile))//if file is not there just return the default video
                {
                    return new Uri(string.Format(@"{0}\\{1}.wmv", VideosPath, "Default"));//default video
                }
                return new Uri(videoFile);
            }
            catch (Exception)
            {
                return new Uri(string.Format(@"{0}\\{1}.wmv", VideosPath, "Default"));//need a default video
            }
        }

        public void ChangeImageFlyer(Image _image)
        {
            if (File.Exists($@"D:\Art\Flyers\{CurrentHighlightedGame}.png"))
            {
                Uri _uri;
                _uri = new Uri($@"D:\Art\Flyers\{CurrentHighlightedGame}.png");
                _image.Source = new BitmapImage(_uri);

            }

        }
        public void ChangeImageBackGround(Image _image)
        {
            if (File.Exists($@"D:\Art\Mame Snaps\{CurrentHighlightedGame}.png"))
            {
                Uri _uri;
                _uri = new Uri($@"D:\Art\Mame Snaps\{CurrentHighlightedGame}.png");
                _image.Source = new BitmapImage(_uri);
            }
            else
            {
                Uri _uri;
                _uri = new Uri($@"D:\Art\Mame Snaps\Default.png");
                _image.Source = new BitmapImage(_uri);
            }

        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }

        }

    }
}
