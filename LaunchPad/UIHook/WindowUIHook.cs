using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LaunchPad.UIHook
{
    public class WindowUIHook
    {
        static List<Button> UiButtons;
        static int CurrentButtonIndex;

        public static void Attach(Window window,StackPanel _Container)
        {
            var CollectionUI = _Container.Children;
            var Temp = new List<Button>();
            foreach (var uiItem in CollectionUI)
            {

                if (uiItem is Button)
                {
                    var uiButton = uiItem as Button;
                    Temp.Add(uiButton);
                }
            }
            UiButtons = Temp;
            window.KeyUp += Window_KeyUp;
        }

        private static void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                CurrentButtonIndex--;
                if (CurrentButtonIndex > -1)
                {


                    var Cbutton = UiButtons[CurrentButtonIndex];
                    Cbutton.Background = Brushes.SkyBlue;
                }
                else { CurrentButtonIndex = 0; }
            }
            for (int i = 0; i < UiButtons.Count; i++)//turn off all buttons not currently selected
            {
                if (i != CurrentButtonIndex)
                {
                    UiButtons[i].Background = Brushes.White;
                }
            }
            if (e.Key == Key.Down)
            {
                CurrentButtonIndex++;
                if (CurrentButtonIndex <= UiButtons.Count - 1)
                {


                    var Cbutton = UiButtons[CurrentButtonIndex];
                    Cbutton.Background = Brushes.SkyBlue;
                }
                else { CurrentButtonIndex = UiButtons.Count - 1; }
            }
            for (int i = 0; i < UiButtons.Count; i++)//turn off all buttons not currently selected
            {
                if (i != CurrentButtonIndex)
                {
                    UiButtons[i].Background = Brushes.White;
                }
            }
            if (e.Key == Key.LeftCtrl)
            {
                UiButtons[CurrentButtonIndex].RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

            }

        }
    }
}
