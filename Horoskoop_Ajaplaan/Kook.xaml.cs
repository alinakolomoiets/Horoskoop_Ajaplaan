using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Horoskoop_Ajaplaan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Kook : ContentPage
    {
        StackLayout st;
        Picker  minutePicker;
        Label lbl;
        Button start, stop;
        int remainingSeconds;
        string currentDish;

        Dictionary<string, int> dishes = new Dictionary<string, int>
    {
        { "Chicken", 20 },
        { "Steak", 30 },
        { "Fish", 15 },
    };

        public Kook()
        {
            lbl = new Label
            {
                BackgroundColor = Color.Gray,
                Text = "Sinu horoskoop",
                FontSize = 15,
            };

            minutePicker = new Picker
            {
                Title = "Minutid",
                BackgroundColor = Color.LightGray,
            };
            for (int i = 0; i <= 59; i++)
            {
                minutePicker.Items.Add(i.ToString("D2"));
            }

            start = new Button
            {
                Text = "Start",
                BackgroundColor = Color.Green,
                TextColor = Color.White,
            };

            stop = new Button
            {
                Text = "Stop",
                BackgroundColor = Color.Red,
                TextColor = Color.White,
                IsEnabled = false,
            };

            start.Clicked += OnStartClicked;
            stop.Clicked += OnStopClicked;

            var st = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = { lbl, minutePicker, start, stop }
            };
            Content = st;
        }

        private void OnStopClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnStartClicked(object sender, EventArgs e)
        {
            currentDish = ""; // Сброс текущего блюда
            remainingSeconds = 0; // Сброс оставшегося времени
            stop.IsEnabled = true; // Включение кнопки Stop
            start.IsEnabled = false; // Отключение кнопки Start
            lbl.Text = "Cooking in progress...";
            var selectedMinutes = int.Parse(minutePicker.SelectedItem.ToString());
            var now = DateTime.Now;
            foreach (var dish in dishes)
            {
                if (remainingSeconds >= dish.Value * 60)
                {
                    currentDish = dish.Key;
                    break;
                }
            }
            if (currentDish == "")
            {
                lbl.Text = "No dish selected for cooking";
                stop.IsEnabled = false;
                start.IsEnabled = true;
            }
            else
            {
                var timer = new Timer(state =>
                {
                    remainingSeconds--;
                    if (remainingSeconds == 0)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            lbl.Text = $"{currentDish} on valmis!";
                            stop.IsEnabled = false;
                            start.IsEnabled = true;
                        });
                        (state as Timer).Dispose();
                    }
                    else
                    {
                        var remainingTimeSpan = TimeSpan.FromSeconds(remainingSeconds);
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            lbl.Text = $"{currentDish} saab sisse {remainingTimeSpan.Hours}:{remainingTimeSpan.Minutes}:{remainingTimeSpan.Seconds}";
                        });
                    }
                }, null, 0, 1000);
            }
        }
    }
}