using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PuzzleGame
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecordsPage : ContentPage
    {
        public RecordsPage()
        {
            InitializeComponent();
            DisplayRecords();
        }
        public void DisplayRecords()
        {
            string[] storageKeys = new string[] { "lvl1", "lvl2", "lvl3" };
            Label[] labels = new Label[] { Easy, Medium, Hard};
            string[] levelNames = new string[] { "Easy", "Medium", "Difficult"};

            for (int i = 0; i < labels.Length; i++)
            {
                if (Application.Current.Properties.ContainsKey(storageKeys[i]))
                {
                    labels[i].Text = $"{levelNames[i]}: { Application.Current.Properties[storageKeys[i]] }";
                }
                else
                {
                    labels[i].Text = $"{levelNames[i]}: ---";
                }
            }
        }
    }
}