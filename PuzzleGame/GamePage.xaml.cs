
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
            Image[] xamlElements = new[] { _01, _02, _03, _04, _05, _06, _07, _08, _09 };
            int blankPuzzlePos = 0;
            int moves = 0;

            //mix puzzles
            Random rnd = new Random();
            int[] order = Enumerable.Range(0, 9).OrderBy(c => rnd.Next()).ToArray();

            //react to taps
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (sender, e) =>
            {
                // cast to an image
                Image clickedImage = (Image)sender;

                if (clickedImage != xamlElements[blankPuzzlePos])
                {
                    if (blankPuzzlePos + numberOfRows < numberOfRows*numberOfRows && clickedImage == xamlElements[blankPuzzlePos+ numberOfRows]) //clicked under the blank puzzle
                    {
                        var temp = order[blankPuzzlePos];
                        order[blankPuzzlePos] = order[blankPuzzlePos + numberOfRows];
                        order[blankPuzzlePos + numberOfRows] = temp;

                        MoveTile(clickedImage);
                        blankPuzzlePos += numberOfRows;
                    }
                    else if (blankPuzzlePos - numberOfRows >= 0 && clickedImage == xamlElements[blankPuzzlePos - numberOfRows]) //clicked over the blank puzzle
                    {
                        var temp = order[blankPuzzlePos];
                        order[blankPuzzlePos] = order[blankPuzzlePos - numberOfRows];
                        order[blankPuzzlePos - numberOfRows] = temp;

                        MoveTile(clickedImage);
                        blankPuzzlePos -= numberOfRows;
                    }
                    else if (blankPuzzlePos % numberOfRows >0 && clickedImage == xamlElements[blankPuzzlePos-1]) //clicked right to the blank puzzle
                    {
                        var temp = order[blankPuzzlePos];
                        order[blankPuzzlePos] = order[blankPuzzlePos-1];
                        order[blankPuzzlePos-1] = temp;

                        MoveTile(clickedImage);
                        blankPuzzlePos --;
                    }
                    else if (blankPuzzlePos % numberOfRows < numberOfRows-1 && clickedImage == xamlElements[blankPuzzlePos + 1]) //clicked left to the blank puzzle
                    {
                        var temp = order[blankPuzzlePos];
                        order[blankPuzzlePos] = order[blankPuzzlePos + 1];
                        order[blankPuzzlePos + 1] = temp;

                        MoveTile(clickedImage);
                        blankPuzzlePos++;
                    }

                    if (IsSolved())
                    {
                        for (int i = 0; i < xamlElements.Length; i++)
                        {
                            xamlElements[i].Source = ImageSource.FromResource($"PuzzleGame.Resource.img01.01.jpg");
                        }
                    }
                }
            };

            int count = 0;
            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfRows; j++)
                {
                    if (order[count] == 8)
                    {
                        blankPuzzlePos = count;
                    }
                    xamlElements[count].Source = ImageSource.FromResource($"PuzzleGame.Resource.img01.0{order[count]+1}.jpg");

                    xamlElements[count].GestureRecognizers.Add(tapGestureRecognizer);
                    count++;
                }
            }

            void MoveTile(Image clickedImage)
            {
                var temp = xamlElements[blankPuzzlePos].Source;
                xamlElements[blankPuzzlePos].Source = clickedImage.Source;
                clickedImage.Source = temp;

                moves++;
                movesLabel.Text = $"Moves: {moves}";
                orderLabel.Text = $"{order[0]} {order[1]} {order[2]} {order[3]} {order[4]} {order[5]} {order[6]} {order[7]} {order[8]}";
            }
            bool IsSolved()
            {
                bool allSolved = true;

                for (int i = 0; i < order.Length; i++)
                {
                    if (order[i] != i)
                    {
                        allSolved = false;
                    }
                }
                return allSolved;
            }
        }
    }
}