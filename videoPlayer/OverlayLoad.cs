using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace videoPlayer
{
    public class OverlayLoad
    {
        Image FlyerImage;
        Image ManufactuerImage;
        Image BackGroundImage;
        ImageBrush MarqueeImage;
        MediaElement PreviewVideo;
        private string ArtRoot
        {
            get
            {

#if LOCALDEBUG
                return @"D:\Art";
#endif
                return @"C:\Art";
            }
        }
        public OverlayLoad(MainWindow mWindow)
        {
            FlyerImage = mWindow.ImageFlyer;
            ManufactuerImage = mWindow.ImageManufactuer;
            BackGroundImage = mWindow.ImageBackGround;
            MarqueeImage = mWindow.MarqueeImageBrush;
            PreviewVideo = mWindow.VideoPLayer;
        }

        /// <summary>
        /// Loads The next set of overlayImages
        /// </summary>
        public void NextImageSet()
        {
            var ToLoad = File.ReadAllText(@"C:\OSCfg\FrontEndAppFiles\SelectedGame.vhs");
            var FlyerPath = new StringBuilder(ArtRoot).Append("\\Flyers\\").Append(ToLoad).Append(".png").ToString();
            var BackgroundPath = new StringBuilder(ArtRoot).Append("\\Mame Snaps\\").Append(ToLoad).Append(".png").ToString();
            var MarqueePath = new StringBuilder(ArtRoot).Append("\\Marquee\\").Append(ToLoad).Append(".png").ToString();
            var VideoPath = new StringBuilder(ArtRoot).Append("\\Videos\\").Append(ToLoad).Append(".wmv").ToString();

            if (!File.Exists(BackgroundPath))
            {
                BackgroundPath = new StringBuilder(ArtRoot).Append("\\Mame Snaps\\").Append("Default").Append(".png").ToString();
            }
            else
            {
                BackGroundImage.Source = new BitmapImage(new Uri(BackgroundPath));
            }
            if (!File.Exists(MarqueePath))
            { }
            else
            {
                MarqueeImage.ImageSource = new BitmapImage(new Uri(MarqueePath));
            }
            if (!File.Exists(FlyerPath))
            { }
            else
            {
                FlyerImage.Source = new BitmapImage(new Uri(FlyerPath));
            }
            if (!File.Exists(VideoPath))
            { }
            else {
                PreviewVideo.Source = new Uri(VideoPath);
            }
            //  var ManufactuerPath = new StringBuilder(ArtRoot).Append("\\Manufactuers\\")
        }
    }
}
