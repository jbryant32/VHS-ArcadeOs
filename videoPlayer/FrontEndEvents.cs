using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace videoPlayer
{
    public class FrontEndEvents
    {
        public static event EventHandler GameChange;

        public static void OnGameChanged(object sender ,EventArgs args)
        {
            if (GameChange != null)
            {
                GameChange(sender, args);
            }
        }
    }
}
