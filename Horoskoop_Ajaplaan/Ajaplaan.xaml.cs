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
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions= LayoutOptions.Center
            };

            tp = new TimePicker
            {
                Time = new TimeSpan(12,0,0),
                TextColor = Color.Black,
            };
            tp.PropertyChanged += TimePicker_Click;

            var st = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children = {tp,lbl,img}
            };
            Content = st;
        }

        private void TimePicker_Click(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Time")
            {
                string[] messages = {  "01:00 - Magama","06:00 - Varahommik","07:00 - Hommilusöök" ,"9:00 - Õppima", "12:00 - Õppima", "13:00 - Lõunasöök", "14:00 -Õppima", "16:00 - Päevane aeg", "17:00 - Päevane aeg", "18:00 - Õhtu", "19:00 - Õhtusöök", "20:00 - Õhtul", "21:00 - Õhtul", "22:00 - Magama", "23:00 -Magama" };
                string[] imageSources = { "magama.png", "image2.png", "image3.png", "image4.png", "image5.png", "image6.png", "image7.png", "image8.png", "image9.png", "image10.png", "image11.png", "image12.png", "image13.png", "image14.png" };
                TimeSpan selectedTime = tp.Time;
                string message = "";
                foreach (string msg in messages)
                {
                    if (selectedTime >= TimeSpan.Parse(msg.Substring(0, 5)))
                    {
                        message = msg;
                    }
                    else
                    {
                        break;
                    }
                }
                if (!string.IsNullOrEmpty(message))
                {
                    lbl.Text = message.Split('-')[1].Trim();
                }
            }
        }
    }
}