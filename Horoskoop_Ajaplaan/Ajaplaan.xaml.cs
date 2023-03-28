using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using TimePicker = Xamarin.Forms.TimePicker;

namespace Horoskoop_Ajaplaan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Ajaplaan : ContentPage
    {
        Label lbl;
        Image img;
        TimePicker tp;
        public Ajaplaan()
        {
            img = new Image { Source = "ggf.png" };
            lbl = new Label
            {
                Text = "Label",
                FontSize = 20,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions= LayoutOptions.Start
            };

            tp = new TimePicker
            {
                Time = new TimeSpan(0,0,0),
                TextColor = Color.Black,
                HorizontalOptions= LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start
            };
            tp.PropertyChanged += TimePicker_Click;

            var st = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = {tp,lbl,img}
            };
            Content = st;
        }

        private void TimePicker_Click(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Time")
            {
                string[] messages = { "01:00 - Magama", "06:00 - Varahommik", "07:00 - Hommikusöök", "8:30 - Õppima", "12:00 - Õppima", "13:00 - Lõunasöök", "14:00 -Õppima", "16:00 - Sport", "17:00 - Kodune aeg", "18:00 - Õhtusöök", "19:00 -Õhtu ", "22:00 - Õhtu", "23:00 -Magama" };
                string[] imageSources = { "magama.png", "sun.png", "hommikusook.png", "tool.png", "tool.png", "lounasook.png", "tool.png", "sport.png", "kodu.png", "ohtusook.png", "luna.png", "luna.png", "magama.png",  };
                TimeSpan selectedTime = tp.Time;
                int messageIndex = -1;
                for (int i = 0; i < messages.Length; i++)
                {
                    string[] timeAndMessage = messages[i].Split('-');
                    if (TimeSpan.TryParse(timeAndMessage[0].Trim(), out TimeSpan messageTime) && messageTime == selectedTime)
                    {
                        messageIndex = i;
                        break;
                    }
                }
                if (messageIndex != -1)
                {
                    string[] timeAndMessage = messages[messageIndex].Split('-');
                    lbl.Text = timeAndMessage[1].Trim();
                    img.Source = ImageSource.FromFile(imageSources[messageIndex]);
                }
            }
        }
    }
}