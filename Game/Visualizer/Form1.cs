using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ICFPC2015.GameLogic.Logic;

namespace ICFPC2015.Visualizer
{
    public partial class Form1 : Form
    {
        private readonly GameBuilder gameBuilder;
        private Timer timer;

        private Game[] games;
        private Game currentGame;
        private List<Game> gamesHistory;
        private int historyStepIndex;
        private int commandIndex;
        private string command;
        private bool needStopExecute;
        private bool executing;

        public Form1()
        {
            InitializeComponent();
            gameBuilder = new GameBuilder();
        }

        private void DrawBoard()
        {
            unitNumberLabel.Text = string.Format("{0} of {1}", currentGame.CurrentUnitNumber + 1, currentGame.UnitsSequence.Length);
            scoresLabel.Text = currentGame.Score.ToString();
            stateLabel.Text = currentGame.State.ToString();

            var multiplier = GetMultiplierValue();
            var imageBuilder = new ImageBuilder();
            var boardImage = imageBuilder.Build(currentGame, multiplier);
            boardBox.Size = new Size(boardImage.Width, boardImage.Height);
            boardBox.Image = boardImage;
        }

        private int GetMultiplierValue()
        {
            int value;
            return int.TryParse(multipierTextBox.Text, out value) ? value : 10;
        }

        private int GetGameIndexValue()
        {
            if (gameIndexCombobox.SelectedValue is int)
                return (int) gameIndexCombobox.SelectedValue;

            int value;
            return int.TryParse(gameIndexCombobox.SelectedValue.ToString(), out value) ? value : 0;
        }

        private int GetTimerValue()
        {
            int value;
            return int.TryParse(timeoutTextBox.Text, out value) ? value : 200;
        }

        private void openGameButton_Click(object sender, EventArgs e)
        {
            openGameDialog.ShowDialog();
        }

        private void openGameDialog_FileOk(object sender, CancelEventArgs e)
        {
            var fileName = openGameDialog.FileName;
            games = gameBuilder.Build(fileName);

            if (games.Length == 0)
            {
                multipierTextBox.Enabled = false;
                gameIndexCombobox.Enabled = false;
                executeComandButton.Enabled = false;
                startGameButton.Enabled = false;
            }

            gameIndexCombobox.Enabled = true;
            multipierTextBox.Enabled = true;
            executeComandButton.Enabled = true;
            startGameButton.Enabled = true;
            var gameIndices = games.Select((g, i) => i).ToArray();
            gameIndexCombobox.DataSource = gameIndices;
        }

        private void gameIndexCombobox_SelectedValueChanged(object sender, EventArgs e)
        {
            currentGame = games[GetGameIndexValue()];
            gamesHistory = new List<Game>(1000);
            DrawBoard();
        }

        private void multipierTextBox_TextChanged(object sender, EventArgs e)
        {
            DrawBoard();
        }

        private void executeComandButton_Click(object sender, EventArgs e)
        {
            executeComandButton.Enabled = false;
            openGameButton.Enabled = false;
            gameIndexCombobox.Enabled = false;
            multipierTextBox.Enabled = false;
            startGameButton.Enabled = false;
            stopExecuteCommandButton.Enabled = true;

            if (!executing)
            {
                executing = true;
                commandIndex = 0;
                historyStepIndex = 0;
                gamesHistory = new List<Game>(1000);
                command = commandsTextBox.Text;
                scoresLabel.Text = 0.ToString();
                stateLabel.Text = string.Empty;
            }

            timer = new Timer {Interval = GetTimerValue()};
            timer.Tick += ExecuteGameStep;
            timer.Start();
        }

        private void ExecuteGameStep(object sender, EventArgs eventArgs)
        {
            var commandChar = command[commandIndex];
            ExecuteGameStep(commandChar);
        }

        private void ExecuteGameStep(char commandChar)
        {
            if (gamesHistory.Count == 0)
                gamesHistory.Add(currentGame);

            currentGame = currentGame.TryMakeStep(commandChar);

            DrawBoard();

            ++historyStepIndex;
            if (gamesHistory.Count - historyStepIndex == 0)
                gamesHistory.Add(currentGame);
            else
                gamesHistory[historyStepIndex] = currentGame;

            if (needStopExecute || command != null && commandIndex + 1 == command.Length ||
                currentGame.State == GameState.Error || currentGame.State == GameState.GameOver)
            {
                timer.Stop();
                executeComandButton.Enabled = true;
                openGameButton.Enabled = true;
                gameIndexCombobox.Enabled = true;
                multipierTextBox.Enabled = true;
                startGameButton.Enabled = true;
                stopExecuteCommandButton.Enabled = false;
                needStopExecute = false;
                executing = false;
                return;
            }

            ++commandIndex;
        }

        private void startGameButton_Click(object sender, EventArgs e)
        {
            commandIndex = 0;
            command = string.Empty;
            scoresLabel.Text = 0.ToString();
            stateLabel.Text = string.Empty;
            currentGame = games[GetGameIndexValue()];
            historyStepIndex = 0;
            gamesHistory = new List<Game>(1000);
            executing = false;
            DrawBoard();
        }

        private void stopExecuteCommandButton_Click(object sender, EventArgs e)
        {
            needStopExecute = true;
        }

        private void moveWestButton_Click(object sender, EventArgs e)
        {
            ExecuteGameStep('p');
        }

        private void moveEastButton_Click(object sender, EventArgs e)
        {
            ExecuteGameStep('b');
        }

        private void moveSouthWestButton_Click(object sender, EventArgs e)
        {
            ExecuteGameStep('a');
        }

        private void moveSouthEastButton_Click(object sender, EventArgs e)
        {
            ExecuteGameStep('m');
        }

        private void turnCounterClockwiseButton_Click(object sender, EventArgs e)
        {
            ExecuteGameStep('k');
        }

        private void turnClockwiseButton_Click(object sender, EventArgs e)
        {
            ExecuteGameStep('d');
        }

        private void previousButton_Click(object sender, EventArgs e)
        {
            if (historyStepIndex == 0)
                return;

            --commandIndex;
            --historyStepIndex;
            currentGame = gamesHistory[historyStepIndex];
            DrawBoard();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (historyStepIndex + 1 == gamesHistory.Count)
                return;


            ++commandIndex;
            ++historyStepIndex;
            currentGame = gamesHistory[historyStepIndex];
            DrawBoard();
        }
    }
}
