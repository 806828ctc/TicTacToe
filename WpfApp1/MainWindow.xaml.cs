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

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isPlayer1Turn = true;
        private Button[,] buttons;

        public MainWindow()
        {
            InitializeComponent();
            buttons = new Button[,] { { btn00, btn01, btn02 }, { btn10, btn11, btn12 }, { btn20, btn21, btn22 } };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (button.Content.ToString() != "") return;    //Only allows is the space is empty

            //Changes the players
            if (isPlayer1Turn)
            {
                button.Content = "X";
            }
            else
            {
                button.Content = "O";
            }
            isPlayer1Turn = !isPlayer1Turn;
            CheckForWinner();
        }

        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            foreach (Button button in buttons)
            {
                button.Content = "";
            }
            isPlayer1Turn = true;
        }

        private void CheckForWinner()
        {
            //Checks each row/column for
            for (int i = 0; i < 3; i++)
            {
                //the left to right
                if (buttons[i, 0].Content.ToString() != "" && buttons[i, 0].Content == buttons[i, 1].Content && buttons[i, 1].Content == buttons[i, 2].Content)
                {
                    MessageBox.Show($"{buttons[i, 0].Content} wins!");
                    btnNewGame_Click(null, null);
                    return;
                }
                //the up and down
                if (buttons[0, i].Content.ToString() != "" && buttons[0, i].Content == buttons[1, i].Content && buttons[1, i].Content == buttons[2, i].Content)
                {
                    MessageBox.Show($"{buttons[0, i].Content} wins!");
                    btnNewGame_Click(null, null);
                    return;
                }
            }
            //Checks diagonal bottom left to top right
            if (buttons[0, 0].Content.ToString() != "" && buttons[0, 0].Content == buttons[1, 1].Content && buttons[1, 1].Content == buttons[2, 2].Content)
            {
                MessageBox.Show($"{buttons[0, 0].Content} wins!");
                btnNewGame_Click(null, null);
                return;
            }
            //Diagonal top left to bottom right
            if (buttons[0, 2].Content.ToString() != "" && buttons[0, 2].Content == buttons[1, 1].Content && buttons[1, 1].Content == buttons[2, 0].Content)
            {
                MessageBox.Show($"{buttons[0, 2].Content} wins!");
                btnNewGame_Click(null, null);
                return;
            }
            //Checks for tie
            if (buttons.Cast<Button>().All(b => b.Content.ToString() != ""))
            {
                MessageBox.Show("It's a tie!");
                btnNewGame_Click(null, null);
                return;
            }
        }
    }
}
