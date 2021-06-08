using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MatchGame2._1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer timer = new DispatcherTimer();
        int secondsElapsed;
        int matchesFound;

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            secondsElapsed++;
            timeTextBlock.Text = (secondsElapsed).ToString();
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " Play again?";
            }
        }

        private void SetUpGame()
        {
            
            List<string> emoji = new List<string>()
            {
                "🍕", "🍕",
                "🍔", "🍔",
                "🍟", "🍟",
                "🌭", "🌭",
                "🍒", "🍒",
                "🍆", "🍆",
                "🥦", "🥦",
                "🥑", "🥑",
            };

            Random random = new Random();

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if(textBlock.Name != "timeTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(emoji.Count);
                    textBlock.Text = emoji[index];
                    emoji.RemoveAt(index);
                }
            }
            timer.Start();
            secondsElapsed = 0;
            matchesFound = 0;
        }

        bool findingMatch = false;
        TextBlock lastClickedTextBlock;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;

            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastClickedTextBlock = textBlock;
                findingMatch = true;
            }
            else if (lastClickedTextBlock.Text == textBlock.Text)
            {
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
                matchesFound++;

            }
            else
            {
                lastClickedTextBlock.Visibility = Visibility.Visible;
                findingMatch = false;
            }

        }

        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}
