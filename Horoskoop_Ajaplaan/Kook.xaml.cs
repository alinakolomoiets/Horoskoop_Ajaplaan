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
        readonly Image toitImage;
        readonly Button alertButton;
        readonly Button timerButton;
        bool timerRunning = false;
        DateTime endTime;

        public Kook()
        {
            toitImage = new Image { Source = "ggf.png" };
            InitializeComponent();
            alertButton = new Button
            {
                Text = "Toote nimi",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            alertButton.Clicked += AlertButton_Clicked;
            timerButton = new Button
            {
                Text = "Määra taimer",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            //timerButton.Clicked += TimerButton_Clicked;
            Content = new StackLayout
            {
                Children = {
                    alertButton,
                    timerButton
                }
            };
        }

        int[] minutes = { 10, 20, 30, 40, 50 };
        string[] toites = { "Kana", "Sealiha", "Pitza", "Kalkun", "Veseliha" };
        string[] images = { "kana.png", "sealiha.png", "pitza.png", "kalkun.png", "veseliha.png" };

        private async void AlertButton_Clicked(object sender, EventArgs e)
        {
            string[] timeChoices = Array.ConvertAll(minutes, x => x.ToString() + " minutit");
            string timeChoice = await DisplayActionSheet("Valige aeg:", "Tühista", null, timeChoices);

            string toitChoice = await DisplayActionSheet("Valige toit:", "Tühista", null, toites);

            if (timeChoice != "Tühista")
            {
                int index = Array.IndexOf(timeChoices, timeChoice);
                endTime = DateTime.Now.AddMinutes(minutes[index]);

                if (toitChoice != "Tühista")
                {
                    int index1 = Array.IndexOf(toites, toitChoice);
                    string imageSource = images[index1].ToLower();
                    toitImage.Source = ImageSource.FromFile(imageSource);
                }

                timerRunning = true;
                timerButton.Text = "Tühista taimer";
                Device.StartTimer(TimeSpan.FromSeconds(1), OnTimerTick);

                await Task.Delay(TimeSpan.FromMinutes(minutes[index]));

                await DisplayAlert("Taimer lõppes", "Valitud toit: " + toitChoice, "OK");
            }
        }

        private bool OnTimerTick()
        {
            if (DateTime.Now >= endTime)
            {
                timerButton.Text = "Alusta taimerit";
                toitImage.Source = null;
                timerRunning = false;
                return false;
            }
            else
            {
                TimeSpan remainingTime = endTime - DateTime.Now;
                timerButton.Text = string.Format("{0}:{1:00}", (int)remainingTime.TotalMinutes, remainingTime.Seconds);
                return true;
            }
        }

    }
}