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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace TicTacToe
{
    using System.Threading;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isPlayer1Turn = true; //This makes it Players 1 turn to start.
        private Button[,] buttons;
        TextBlock turnBlock, winBlock, playBlock;
        int count = 0;

        public MainWindow()
        {
            InitializeComponent();
            buttons = new Button[,] { { btn00, btn01, btn02 }, { btn10, btn11, btn12 }, { btn20, btn21, btn22 } };//How we access the buttons
            turnBlock = textBlock1;//The textBlock that displays who's turn it is on the game
            winBlock = winnerBlock;
            playBlock = playerBlock;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;//Gets the button that was clicked

            if (button.Content.ToString() != "") return;    //Checks if the button is empty, returns if it's not

            //Updates the button clicked and the textBlock based off of who's turn it is 
            if (isPlayer1Turn)
            {
                button.Content = "X";
                turnBlock.Text = "       It's Player 2 Turn!";
            }
            else
            {
                button.Content = "O";
                turnBlock.Text = ("       It's Player 1 Turn!");
            }
            isPlayer1Turn = !isPlayer1Turn;//Changes who's turn it is 
            CheckForWinner();
            count++;

            if (count > 3)
            {
                winBlock.Text = " "; 
                playBlock.Text = " ";
            }
        }

        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            //Resets each of the buttons
            foreach (Button button in buttons)
            {
                button.Content = "";
            }
            isPlayer1Turn = true; //Changes it to Player 1 turn for new game
            turnBlock.Text = "New Game! Player 1 starts."; //Resets the text block
            count = 0;

        }


        private void CheckForWinner()
        {
            //Each of these loops check to see if there are three in a row that are the same symbol. If a win is found, it displays a MessageBox stating who
            //won, starts a new game using the btnNewGame_Click method, and then returns. 

            //Checks each row/column for a win by..
            for (int i = 0; i < 3; i++)
            {
                //each row, left to right
                if (buttons[i, 0].Content.ToString() != "" && buttons[i, 0].Content == buttons[i, 1].Content && buttons[i, 1].Content == buttons[i, 2].Content)
                {
                    //MessageBox.Show($"{buttons[i, 0].Content} wins!");
                    string play = buttons[i, 0].Content.ToString();
                    playBlock.Text = ($"{play} won!");
                    winBlock.Text = "There's a winner!!";
                    btnNewGame_Click(null, null);
                   
                    return;
                }
                //each column, up and down
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
            //Checks for a tie (if all of the buttons are full and there was no win)
            if (buttons.Cast<Button>().All(b => b.Content.ToString() != ""))
            {
                MessageBox.Show("It's a tie!");
                btnNewGame_Click(null, null);
                return;
            }
        }
    }
}
