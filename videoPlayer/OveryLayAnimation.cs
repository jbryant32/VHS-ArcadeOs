using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace videoPlayer
{
    public class OveryLayAnimation
    {
        MainWindow window;
        Image FlyerUiImage;
        Image BackGroundImage;
        OverlayLoad overlayLoad;
        DispatcherTimer ThemeLoadTimer;
        public event EventHandler ThemeOutEvent;
        public event EventHandler ThemeInEvent;
        int timeCount;
        public OveryLayAnimation(MainWindow mWindow)
        {
            this.window = mWindow;
            FrontEndEvents.GameChange += FrontEndEvents_GameChange;
            FlyerUiImage = window.ImageFlyer;
            BackGroundImage = window.ImageBackGround;
            ThemeLoadTimer = new DispatcherTimer();
            ThemeLoadTimer.Interval = new TimeSpan(0, 0, 1);
            ThemeLoadTimer.Tick += ThemeLoadTimer_Tick;
            ThemeOutEvent += OveryLayAnimation_ThemeOutEvent;
            ThemeInEvent += OveryLayAnimation_ThemeInEvent;
            this.overlayLoad = new OverlayLoad(mWindow);
        }

        private void ThemeLoadTimer_Tick(object sender, EventArgs e)
        {
            timeCount++;
            if (timeCount == 2)
            {
                timeCount = 0;
                ThemeLoadTimer.Stop();
                if (ThemeInEvent != null)
                {
                    ThemeInEvent(this, new EventArgs());
                }
            }
        }

        private void OveryLayAnimation_ThemeInEvent(object sender, EventArgs e)
        {
            overlayLoad.NextImageSet();
            AnimateFlyerIn();
            AnimateBackGroundIn();
        }

        private void OveryLayAnimation_ThemeOutEvent(object sender, EventArgs e)
        {
            if (ThemeLoadTimer.IsEnabled)
                ThemeLoadTimer.Stop();
           
            ThemeLoadTimer.Start();
            AnimateFlyerOut();
            AnimateBackGroundOut();
        }

        
        private void FrontEndEvents_GameChange(object sender, EventArgs e)
        {

            if (ThemeOutEvent != null)
            {
                ThemeOutEvent(this, new EventArgs());
            }

        }
        Thickness TargetMargin;
        private void AnimateFlyerIn()
        {
         
            ThicknessAnimation inanim = new ThicknessAnimation(new Thickness(399, -5, 1, 5), TimeSpan.FromSeconds(0.2f));
            FlyerUiImage.BeginAnimation(Image.MarginProperty, inanim);
        }
        private void AnimateFlyerOut()
        {

            Thickness margin = new Thickness(-500, 0, 0, 0);
            TargetMargin = FlyerUiImage.Margin;
            ThicknessAnimation outAnim = new ThicknessAnimation(margin, TimeSpan.FromSeconds(0.2f));
            FlyerUiImage.BeginAnimation(Image.MarginProperty, outAnim);

        }
       
        private void AnimateBackGroundIn()
        {
            ThicknessAnimation inanim = new ThicknessAnimation(new Thickness(0, 0, 0, 0), TimeSpan.FromSeconds(0.2f));
            BackGroundImage.BeginAnimation(Image.MarginProperty, inanim);
        }
        private void AnimateBackGroundOut()
        {
            ThicknessAnimation Outanim = new ThicknessAnimation(new Thickness(0,600, 0, 0), TimeSpan.FromSeconds(0.2f));
            BackGroundImage.BeginAnimation(Image.MarginProperty, Outanim);

        }

        private void AnimateManufactuerIn()
        {

        }
        private void AnimateManufactuerOut()
        {

        }
    }
}
