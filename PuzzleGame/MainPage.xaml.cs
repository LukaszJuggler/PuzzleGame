using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PuzzleGame
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked_Easy(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GamePage(3));
        }

        private async void Button_Clicked_Medium(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GamePage(3));
        }

        private async void Button_Clicked_Hard(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GamePage(3));
        }

        private async void Button_Clicked_Records(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecordsPage());
        }
    }
}
