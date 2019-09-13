
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
        public GamePage(int numberOfRows, int level)
        {
            InitializeComponent();
            InitializeGame(numberOfRows, level);
        }

        private void InitializeGame(int numberOfRows, int level)
        {
            bool gameMode = false;
            Image[] xamlElements = new[] { _01, _02, _03, _04, _05, _06, _07, _08, _09 };
            int blankPuzzlePos = xamlElements.Length-1;
            int moves = 0;
            Random rnd = new Random();
            int[] order = new int[] { 0,1,2,3,4,5,6,7,8};
            int[] difficulty = new int[] { 10, 20, 40 };
            string[] storageKeys = new string[] { "lvl1", "lvl2", "lvl3" };

            //react to taps
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (sender, e) => { if (gameMode) { OnTap(sender); } };


            for (int i = 0; i < xamlElements.Length; i++)
            {
                xamlElements[i].Source = ImageSource.FromResource($"PuzzleGame.Resource.img01.0{i + 1}.jpg");

                xamlElements[i].GestureRecognizers.Add(tapGestureRecognizer);
            }

            ShuffleTiles(difficulty[level]);
            gameMode = true;

            bool OnTap(object sender)
            {
                bool isSwapped = true;
                // cast to an image
                Image clickedImage = (Image)sender;

                if (clickedImage != xamlElements[blankPuzzlePos])
                {
                    if (blankPuzzlePos + numberOfRows < numberOfRows * numberOfRows && clickedImage == xamlElements[blankPuzzlePos + numberOfRows]) //clicked under the blank puzzle
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
                    else if (blankPuzzlePos % numberOfRows > 0 && clickedImage == xamlElements[blankPuzzlePos - 1]) //clicked right to the blank puzzle
                    {
                        var temp = order[blankPuzzlePos];
                        order[blankPuzzlePos] = order[blankPuzzlePos - 1];
                        order[blankPuzzlePos - 1] = temp;

                        MoveTile(clickedImage);
                        blankPuzzlePos--;
                    }
                    else if (blankPuzzlePos % numberOfRows < numberOfRows - 1 && clickedImage == xamlElements[blankPuzzlePos + 1]) //clicked left to the blank puzzle
                    {
                        var temp = order[blankPuzzlePos];
                        order[blankPuzzlePos] = order[blankPuzzlePos + 1];
                        order[blankPuzzlePos + 1] = temp;

                        MoveTile(clickedImage);
                        blankPuzzlePos++;
                    } else
                    {
                        isSwapped = false;
                    }
                } else
                {
                    isSwapped = false;
                }

                if (gameMode && IsSolved()) //when you win
                {
                    //display message
                    movesLabel.Text = $"You solved the puzzle in {moves} moves!";

                    //save the result
                    SaveRecord();

                    //block interaction with tiles
                    gameMode = false;
                }
                return isSwapped;
            }
            async void SaveRecord()
            {
                if (Application.Current.Properties.ContainsKey(storageKeys[level]))
                {
                    int previousRecord = int.Parse(Application.Current.Properties[storageKeys[level]].ToString());
                    if (previousRecord > moves)
                    {
                        Application.Current.Properties[storageKeys[level]] = moves.ToString();
                        await Application.Current.SavePropertiesAsync();
                    }
                }
                else
                {
                    Application.Current.Properties.Add(storageKeys[level], moves.ToString());
                    await Application.Current.SavePropertiesAsync();
                }
            }

            void ShuffleTiles(int numberOfMoves)
            {
                int currentTry = 0;
                while (currentTry < numberOfMoves)
                {
                    int randomNum = rnd.Next(0, xamlElements.Length);
                    if (OnTap(xamlElements[randomNum])) { currentTry++; }
                }

                orderLabel.Text = $"{order[0]} {order[1]} {order[2]} {order[3]} {order[4]} {order[5]} {order[6]} {order[7]} {order[8]}";

            }

            void MoveTile(Image clickedImage)
            {
                var temp = xamlElements[blankPuzzlePos].Source;
                xamlElements[blankPuzzlePos].Source = clickedImage.Source;
                clickedImage.Source = temp;

                if (gameMode)
                {
                    moves++;
                    movesLabel.Text = $"Moves: {moves}";
                    orderLabel.Text = $"{order[0]} {order[1]} {order[2]} {order[3]} {order[4]} {order[5]} {order[6]} {order[7]} {order[8]}";
                }
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