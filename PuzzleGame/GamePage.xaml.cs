using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PuzzleGame
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {
        public GamePage(int numberOfRows)
        {
            InitializeComponent();
            InitializeGame(numberOfRows);
        }
        private void InitializeGame(int numberOfRows)
        {
            CreateImages();
        }

        private void CreateImages()
        {
            var imagesource = new UriImageSource { Uri = new Uri("https://picsum.photos/600") };
            _01.Source = imagesource;

            //var image = new Image { Source = "img01.jpg" };
            //_01.Source = image.Source;
            //_01.Source = "img01.jpg";

        }
    }
}