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
using System.Threading;



namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isPlayer1Turn = true, isRandomGame = false, winner = false; //This makes it Players 1 turn to start.
        private readonly Button[,] buttons;
        private int count = 0;
        private readonly TextBlock turnBlock, playBlock, modeBlock;
        readonly Random random = new();


        public MainWindow()
        {
            InitializeComponent();
            buttons = new Button[,] { { btn00, btn01, btn02 }, { btn10, btn11, btn12 }, { btn20, btn21, btn22 } };//How we access the buttons
            turnBlock = textBlock1;//The textBlock that displays who's turn it is on the game
            playBlock = PlayerBlock;//The block that displays when someone has won
            modeBlock = PlayerModeBlock;//Shows the players which mode the game is currently in
            modeBlock.Text = "PVP Mode";//Displays that the game starts in Player VS Player mode
            playBlock.Foreground = Brushes.Green;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;//Gets the button that was clicked

            if (button.Content.ToString() != "") return;//Checks if the button is empty, returns if it's not

            if (!isRandomGame)//If the game is set to PVP mode
            {
                //Updates the button clicked and the textBlock based off of who's turn it is 
                if (isPlayer1Turn)
                {
                    button.Content = "X";
                    turnBlock.Text = "      It's Player 2 Turn!";
                }
                else
                {
                    button.Content = "O";
                    turnBlock.Text = ("      It's Player 1 Turn!");
                }
                isPlayer1Turn = !isPlayer1Turn;//Changes who's turn it is 
            }
            else//PVComp mode 
            {
                if (isPlayer1Turn)
                {
                    button.Content = "X";
                    turnBlock.Text = "     Computer Thinking..";
                    isPlayer1Turn =false;
                    CheckForWinner();//Checks to see if Player1 had just won or not
                        
                    await Task.Delay(750);
                    if (winner == false)
                    {
                        PlayRandom();
                        turnBlock.Text = ("      It's Player 1 Turn!");
                        isPlayer1Turn = true;
                       
                    }
                }
            }

            CheckForWinner();
            //winner = false;
            count++;


            if (count > 3)
            {
                playBlock.Text = " ";
            }
        }

        private void PlayRandom()
        {
            int x = random.Next(0, 3), y = random.Next(0, 3);//Uses random numbers to pick a button
            if (buttons[x,y].Content.ToString() != "") {//If that button is already used it calls this method again
                PlayRandom();
            }
            else {
                buttons[x,y].Content = "O"; //When it finds a button to play on, places their turn
            } 
        }

        private void PlayerButton_Click(object? sender, RoutedEventArgs? e)
        {
            //Changes the game mode to PVP instead of PVComp
            BtnNewGame_Click(null, null);
            isRandomGame = false;
            modeBlock.Text = "PVP Mode";
        }

        private void ComputerButton_Click(object? sender, RoutedEventArgs? e)
        {
            //Changes to PVComp mode instead of PVP
            BtnNewGame_Click(null, null);
            isRandomGame = true;
            modeBlock.Text = "PVComp Mode";
        }

        private void BtnNewGame_Click(object? sender, RoutedEventArgs? e)
        {
            //Resets each of the buttons to be empty and using the color black
            foreach (Button button in buttons)
            {
                button.IsEnabled = true; 
                button.Content = "";
                button.Foreground = Brushes.Black;
            }
            isPlayer1Turn = true; //Changes it to Player 1 turn for new game
            turnBlock.Text = "New Game! Player 1 starts."; //Resets the text block
            count = 0;//Starts over counter
            winner = false;

        }

        private async void DisplayWin(string play)
        {
            foreach(Button button in buttons)
            {
                button.IsEnabled = false;
            }
            await Task.Delay(750);
            playBlock.Text = ($"{play} won!");
            winner = true;
            BtnNewGame_Click(null, null);
        }



        private async void CheckForWinner()
        {
            //If any of these loops finds a win, it changes the colors of the plays to green, so the win is easier to see. Then it waits a second, displays
            //which player had won, and then starts a new game by using the btnNewGame_Click method

            string play; 
            //Checks each row/column for a win by..
            for (int i = 0; i < 3; i++)
            {
                //each row, left to right
                if (buttons[i, 0].Content.ToString() != "" && buttons[i, 0].Content == buttons[i, 1].Content && buttons[i, 1].Content == buttons[i, 2].Content)
                {
                    //MessageBox.Show($"{buttons[i, 0].Content} wins!"); 
                    play = buttons[i, 0].Content.ToString();
                    buttons[i, 0].Foreground = Brushes.ForestGreen;
                    buttons[i, 1].Foreground = Brushes.ForestGreen;
                    buttons[i, 2].Foreground = Brushes.ForestGreen;
                    DisplayWin(play);
                    return;
                }
                //each column, up and down
                if (buttons[0, i].Content.ToString() != "" && buttons[0, i].Content == buttons[1, i].Content && buttons[1, i].Content == buttons[2, i].Content)
                {
                    play = buttons[0, i].Content.ToString();
                    buttons[0, i].Foreground = Brushes.ForestGreen;
                    buttons[1, i].Foreground = Brushes.ForestGreen;
                    buttons[2, i].Foreground = Brushes.ForestGreen;
                    DisplayWin(play);
                    return;
                }
            }
            //Checks diagonal bottom left to top right
            if (buttons[0, 0].Content.ToString() != "" && buttons[0, 0].Content == buttons[1, 1].Content && buttons[1, 1].Content == buttons[2, 2].Content)
            {
                play = buttons[1, 1].Content.ToString();
                buttons[0, 0].Foreground = Brushes.ForestGreen;
                buttons[1, 1].Foreground = Brushes.ForestGreen;
                buttons[2, 2].Foreground = Brushes.ForestGreen;
                DisplayWin(play);
                return;
            }
            //Diagonal top left to bottom right
            if (buttons[0, 2].Content.ToString() != "" && buttons[0, 2].Content == buttons[1, 1].Content && buttons[1, 1].Content == buttons[2, 0].Content)
            {
                play = buttons[2, 0].Content.ToString();
                buttons[0, 2].Foreground = Brushes.ForestGreen;
                buttons[1, 1].Foreground = Brushes.ForestGreen;
                buttons[2, 0].Foreground = Brushes.ForestGreen;
                DisplayWin(play);
                return;
            }
            //Checks for a tie (if all of the buttons are full and there was no win)
            if (buttons.Cast<Button>().All(b => b.Content.ToString() != ""))
            {
                winner = true; 
                await Task.Delay(750);
                playBlock.Text = ("Tie!!");
                BtnNewGame_Click(null, null);
                return;
            }
        }
    }
}
