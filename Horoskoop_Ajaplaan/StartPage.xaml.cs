using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.Mime.MediaTypeNames;

namespace Horoskoop_Ajaplaan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        List<ContentPage> contentPage = new List<ContentPage>() { new Horoskoop(),new Ajaplaan(),new Kook()};
        List<string> tekstid = new List<string> { "Horoskoop","Ajaplaan","Timer"};
        public StartPage()
        {
            StackLayout st = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.Beige,
            };
            for (int i = 0; i < contentPage.Count; i++)
            {
                Button button = new Button
                {
                    Text = tekstid[i],
                    TabIndex = i,
                    BackgroundColor = Color.MediumSpringGreen,
                    TextColor = Color.Black,
                };
                button.Clicked += Navig_funktsion;
                st.Children.Add(button);
            }

            Content = st;
        }
        private async void Navig_funktsion(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            await Navigation.PushAsync(contentPage[b.TabIndex]);
        }
    }
}