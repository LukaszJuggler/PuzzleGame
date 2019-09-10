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

            
        }
    }
}