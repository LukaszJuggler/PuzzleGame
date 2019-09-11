
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
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
            int blankPuzzlePos = 0;
            int moves = 0;

            //mix puzzles
            Random rnd = new Random();
            int[] order = Enumerable.Range(1, 9).OrderBy(c => rnd.Next()).ToArray();

            //react to taps
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (sender, e) =>
            {
                // cast to an image
                Image theImage = (Image)sender;

                if (theImage != xamlElements[blankPuzzlePos])
                {
                    theImage.Source = xamlElements[0].Source;
                    moves++;
                }
            };


            int count = 0;
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfRows; j++)
                {
                    if (order[count] == 9)
                    {
                        blankPuzzlePos = count;
                    }
                    xamlElements[count].Source = ImageSource.FromResource($"PuzzleGame.Resource.img01.0{order[count]}.jpg");

                    xamlElements[count].GestureRecognizers.Add(tapGestureRecognizer);
                    count++;
                }
            }
        }
    }
}