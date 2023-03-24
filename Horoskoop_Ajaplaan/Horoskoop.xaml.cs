using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Horoskoop_Ajaplaan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Horoskoop : ContentPage
    {
        StackLayout at;
        DatePicker dp ;
        Label lbl;
        Image img = new Image();
        Entry entry;
        Button submit;
        public Horoskoop()
        {
            Title = "Horoskoop";
            img = new Image { Source = "zodiac.png" };
            lbl = new Label
            {

                BackgroundColor = Color.Gray,
                Text = "Sinu horoskoop",
                FontSize= 15,
            };
            dp = new DatePicker
            {
                Format = "dd-MM-yyyy",
                TextColor = Color.Black,
                BackgroundColor = Color.LightGray,
            };
            at = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.Black,
            };
            entry = new Entry
            {
                Placeholder = "Zodiac",
                PlaceholderColor = Color.Gray,
                TextColor = Color.Black
            };

            submit = new Button
            {
                Text = "Submit",
                TextColor = Color.White,
                BackgroundColor = Color.Blue,
                FontSize = 20,
                Margin = new Thickness(20, 0)
            };
            dp.DateSelected += Dp_DateSelected;
            StackLayout st = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.Lavender,
                Children = { at,dp,lbl,img},

            };
            Content = st;
        }
        private void Dp_DateSelected(object sender, DateChangedEventArgs e)
        {
            var kuu = e.NewDate.Month;
            var paev = e.NewDate.Month;
            string[] zodiac = { "kaljukits.png", "veevalaline.png", "kalad.png" , "jaar.png", "sonn.png", "kaksikud.png", "vahk.png", "leo.png", "virgin.png", "kaalu.png", "skorpion.png", "amburs.png",  };
            string[] kirjeldus ={
            "Kaljukits - hoolikas, tark, aktiivne. Maise elemendi ja Kaljukitse märk on kingitus, et mitte unustada põhieesmärki ja elada kaua. Tseloveness, vastupanu raskustes, vastutus on selle märgi esindajate tugevad omadused. Kaljukits ei karda üksindust, valmis taluma igapäevaseid raskusi, ületama takistusi.",
            "Veevalaja on andekas kujutlusvõimega, idealistlik, intuitiivne. Õhu elementide fikseeritud risti märk - Veevalaja muutub mitterahaliselt, kuid see ei meeldi muutustele, mis on täis vastuolusid. Õiglane individualist, Veevalaja kaldub meeleolumuutusi, seejärel elegantset, seejärel lohakat, kannatab puuduse tõttu isedistsipliinist, otsustav ja hele temperament.",
            "Kala on loominguline, tundlik, kunstiline. Kalad sulgevad sodiaagi ringi, esindades vee elementide märki. Need on targad ja vastuvõtlikud inimesed, kelle reageerimisvõime viib nad sageli manipulaatoritega suhtlemisele. Kellegi teise mõju kasvanud up, kõrgeim sodiaagimärgid, võime kohaneda Mis tahes ümbritsev olukord ja vastupanu igapäevastele raskustele eristab tüüpilisi kalu.",
            "Jäär on ambitsioonikas, sõltumatu, kannatamatu. Jäär, tule elementide märk, avab uue sodiaagi tsükli, kuulub tule elementidesse, tal on avastaja, algatuse ja sihikindluse spetsiaalne karisma (kvaliteet). Samuti on rahulik temperament, Jäär ei unusta kunagi oma eesmärgid ja reeglina jõuavad varem või hiljem soovitud.",
            "Sõnn on põhjalik, muusikaline, praktiline. Maa, looja ja gurmee elementide fikseeritud märk Sõnn kehastab elu armastuse põhimõtet ja selle eeliseid ning sellel on ka visaduse ja praktilisuse omadused. Sõnn teab, kuidas ja armastab töötada, loob kannatlikult mugavad tingimused elu.",
            "Kaksikud - uudishimulik, õrn, lahke. Õhu elementide liikuva risti märk. Blondidel on tugev iseloom, energiline, iseseisv ja seltskondlik. Need on suhtlevad, rõõmsameelse iseloomu ja temperamentse uudishimuga. Kaksikud loovad hõlpsalt sidemeid paljude erinevate inimestega.",
            "Vähk on intuitiivne, emotsionaalne, tark, kirglik. Vee elemendi märk on öövalguse egiidi all. Kuu juhtimine mõjutab selle märgi esindajate olemust, muutes nad haavatavate ja tundlike inimeste. Märgi kuu ja vee element annavad vähile empaatiavõime, võimaluse kohe arvata teiste inimeste mõtteid ja püüdlusi. ",
            "Leo on uhke, iseenda konfident. Tule elementide fikseeritud märk, lõvil on loomise ja visaduse kingitus oma eesmärkide saavutamisel. See on aktiivne inimene, kes püüdleb edu ja populaarsuse poole. Silia, tundlik ja armastav loomus, kuulub sageli emotsioonide mõju alla ja sageli kuulub tunded. See on helde, otsustav ja vapper.",
            "Virgin on elegantne, organiseeritud, lahke. Neitsi on maa elementide teine ​​märk, õigluse ja puhtuse isikupärastamine. Tüdruk kehastab korra põhimõtet, mõistuse võidu tunnete üle, võimet näha tervikut üksikasjalikult. Puu on rohkem kui muud märgid Täiskuju, kes püüdleb täiuslikkuse poole kõiges, õpib kogu oma elu, kuid õpetab ka teisi.",
            "Kaalud on diplomaatiline, kunstiline, intelligentne. Ainus elutu sümbol, sodiaagi ringis, kaalud on õhu elementide teine ​​märk. Selle märgi esindajate laialdaselt tunnusjoon on soov kõiges harmoonia järele. Vaimu ja ebameeldiva tahte võidule mis tahes rivaalitsemise korral toimivad skaalad sageli nii kohtunike rolli kui ka juristidena kõigil tasanditel.",
            "Skorpion - lummav, kirglik, iseseisv. Scorpio is a fixed sign of the elements of water. Scorpio has natural magnetism and strong character. Disperial, restrained in words and emotions, Scorpio knows how to keep secrets and appreciates loyalty. Scorpio is a sign of internal changes, overcoming weakness, struggle to võidukas lõpp.",
            "Ambur - seiklushimuline, loominguline, tugev -tallatud. Ambur on märk tule elementidest, tal on juhi väljendunud karisma, püüdleb hariduse poole, energilise ja kirgliku idee poole kogu maailma muutmise idee poole. Amburi Vosya Life püüdleb populaarsuse poole, et tema kõrgele hinnata Lähedaste inimeste töö ja saavutused. Tulistaja saavutab peaaegu alati edu vähemalt ühes paljudes tegevustes."
            };
            int[] zodiacD = { 21, 20, 21, 22, 23, 23, 23, 23, 22, 22, 20, 19 };
            int index = (paev <= zodiacD[kuu-1])? kuu - 1 : (kuu +10)%12;
            lbl.Text = kirjeldus[index];
            img.Source = ImageSource.FromFile(zodiac[index]);
        }
    }
}