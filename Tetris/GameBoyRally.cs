using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    class GameBoyRally : GameBoy
    {
        public GameBoyRally(DataGridView dataGridView, int height = 22, int width = 12)
            : base(height, width, dataGridView)
        {
            FigureController figureController = new FigureController(this);
            FigureFromFile figureFromFile = new FigureFromFile();
            //timer = new Timer();
            Figures = new List<Figure>();
            Figures.Add(figureFromFile.CreateFigure("C:\\Users\\Admin\\Figures\\Car.txt"));
            timer.Interval = 500;
            Figures[0].Top = Height - 5;
            Figures[0].Left = Width / 2 - Figures[0].N / 2;
            figureController.Move(Figures[0], 0, 0);
        }
        public override void Control(int key)
        {
            switch(key)
            {
                //Up
                case 1: if (figureController.CanMove(Figures[0], 0, -1) == 0)
                        figureController.Move(Figures[0], 0, -1); break;
                //Down
                case 2: if (figureController.CanMove(Figures[0], 0, 1) == 0)
                        figureController.Move(Figures[0], 0, 1); break;
                //Right
                case 3: if (figureController.CanMove(Figures[0], 1, 0) == 0)
                        figureController.Move(Figures[0], 1, 0); break;
                //Left
                case 4: if (figureController.CanMove(Figures[0], -1, 0) == 0)
                        figureController.Move(Figures[0], -1, 0); break;
            }
            ShowInGrid.ShowGrid(dataGridView1, this);
        }

        public override void PauseGame()
        {
            timer.Enabled = false;
        }

        public override void StartGame()
        {
            timer.Enabled = true;
        }
    }
}