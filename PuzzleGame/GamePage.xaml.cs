
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Image = Xamarin.Forms.Image;

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
            CreateImages(numberOfRows);
        }

        private void CreateImages(int numberOfRows)
        {
            Image[] xamlElements = new[] { _01, _02, _03, _04, _05, _06, _07, _08, _09 };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (sender, e) =>
            {
                // cast to an image
                Image theImage = (Image)sender;

                // now you have a reference to the image
                theImage.Source = xamlElements[0].Source;
            };


            int count = 0;
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfRows; j++)
                {
                    xamlElements[count].Source = ImageSource.FromResource($"PuzzleGame.Resource.img01.0{count+1}.jpg");

                    xamlElements[count].GestureRecognizers.Add(tapGestureRecognizer);
                    count++;
                }
            }
        }
    }
}