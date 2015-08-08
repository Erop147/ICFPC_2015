using System.Drawing;
using System.Windows.Forms;
using ICFPC2015.GameLogic.Logic;

namespace ICFPC2015.Visualizer
{
    public partial class Form1 : Form
    {
        private Game game;

        public Form1()
        {
            InitializeComponent();
//            InitializeBoard();
        }

        private void InitializeBoard()
        {
            var games = new GameBuilder().Build(@"..\..\..\..\Problems\problem_1.json");
            game = games[0];
            DrowBoard(game);
        }

        private void DrowBoard(Game game)
        {
            const int multiplier = 10;
            var imageBuilder = new ImageBuilder();
            var boardImage = imageBuilder.Build(game, multiplier);
            boardBox.Size = new Size(boardImage.Width, boardImage.Height);
            boardBox.Image = boardImage;
        }

        private void openGameButton_Click(object sender, System.EventArgs e)
        {
            openGameDialog.ShowDialog();
        }

        private void openGameDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var fileName = openGameDialog.FileName;
        }
    }
}
