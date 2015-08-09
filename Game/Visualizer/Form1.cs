using System;
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
            currentGame = currentGame.TryMakeStep(commandChar);

            unitNumberLabel.Text = string.Format("{0} of {1}", currentGame.CurrentUnitNumber + 1, currentGame.UnitsSequence.Length);
            scoresLabel.Text = currentGame.Score.ToString();
            stateLabel.Text = currentGame.State.ToString();

            DrawBoard();

            if (needStopExecute || commandIndex + 1 == command.Length ||
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
            executing = false;
            DrawBoard();
        }

        private void stopExecuteCommandButton_Click(object sender, EventArgs e)
        {
            needStopExecute = true;
        }
    }
}
