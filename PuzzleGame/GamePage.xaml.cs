
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
            //var imagesource = new UriImageSource { Uri = new Uri("https://picsum.photos/600") };

            var grid = new Grid
            {
                RowDefinitions =
            {
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition()
            },
                ColumnDefinitions =
            {
                new ColumnDefinition (),
                new ColumnDefinition (),
                new ColumnDefinition ()
            }
            };

            int count = 0;
            for (int i = 0; i< numberOfRows; i++)
            {
                for (int j = 0; j < numberOfRows; j++)
                {
                    count++;
                    grid.Children.Add(new Image { Source = ImageSource.FromResource($"PuzzleGame.Resource.img01.0{count}.jpg") }, j, i);

                }
            }

            StackLayoutMap.Children.Add(grid);
        }
    }
}