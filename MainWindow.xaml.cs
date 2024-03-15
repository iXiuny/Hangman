using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using static System.Net.Mime.MediaTypeNames;

namespace Hangman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game _game;
        public MainWindow()
        {
            InitializeComponent();
            _game = new Game(ReadTxtFile());
            lbl_hiddenword.Content = string.Empty; //Da portare HiddenWord da Public game
            txtb_choice.Visibility = Visibility.Collapsed;
            lbl_lives.Visibility= Visibility.Collapsed;
            lbl_lives.Content = _game.showlives;
            btn_playagain.Visibility = Visibility.Collapsed;
        }

        public int ReadTxtFile()
        {
            int txtcount=0;
            using (StreamReader TxtRead = new StreamReader("parole.txt"))
            {
                string txtonread;
                while ((txtonread = TxtRead.ReadLine()) != null)
                {
                    txtcount++;
                }
                //lblTestOutput.Content = txtcount.ToString();
            }
            return txtcount;
        }

        private void btn_play_Click(object sender, RoutedEventArgs e)
        {
            lbl_hiddenword.Content = _game.HiddenExtraction;
            txtb_choice.Visibility=Visibility.Visible;
            lbl_lives.Visibility = Visibility.Visible;
            btn_play.Visibility = Visibility.Collapsed;   
        }
        private void btn_playAgain_Click(object sender, RoutedEventArgs e)
        {
            _game = new Game(ReadTxtFile());
            _game.RestartGame();
            lbl_lives.Content = _game.showlives;
            lbl_hiddenword.Content= _game.HiddenExtraction;
            txtb_choice.Visibility = Visibility.Visible;
            btn_playagain.Visibility= Visibility.Collapsed;
            background.Source = new BitmapImage(new Uri("Images/Hangman" + _game.showlives + "livesleft.png", UriKind.Relative));

        }


        private void txtb_choice_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _game.ExtractionGuess();
                lbl_lives.Content = _game.showlives;
                lbl_hiddenword.Content = _game.HiddenExtraction;
                if (_game.HiddenExtraction == _game.ExtractionKeeper)
                {
                    lbl_hiddenword.Content = "You Won!";
                    txtb_choice.Visibility = Visibility.Collapsed;
                    btn_playagain.Visibility = Visibility.Visible;
                    btn_playagain.Background = Brushes.Green;
                    btn_play.Content = "Play Again";
                }
                else if (_game.showlives == 0)
                {
                    lbl_hiddenword.Content = "You Lost!";
                    txtb_choice.Visibility = Visibility.Collapsed;
                    background.Source = new BitmapImage(new Uri("Images/Hangman0livesleft.png", UriKind.Relative));
                    btn_playagain.Background = Brushes.Red;
                    btn_playagain.Visibility= Visibility.Visible;
                    btn_play.Content = "Retry";
                }
                else
                {
                    background.Source = new BitmapImage(new Uri("Images/Hangman" + _game.showlives + "livesleft.png", UriKind.Relative)) ;
                }
            }
        }
        private void txtb_choice_TextChanged(object sender, TextChangedEventArgs e) {
            lbl_Error.Visibility = Visibility.Collapsed;
            try
            {
                _game.PlayerChoice = Convert.ToChar(txtb_choice.Text);
            }
            catch
            {
                txtb_choice.Text = "";
                lbl_Error.Visibility = Visibility.Visible;
                lbl_Error.Content = "A character is required in order to play";
            }

        }
    }
}
