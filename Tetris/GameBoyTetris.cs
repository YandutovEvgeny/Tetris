using System;
using System.Windows.Forms;

namespace Tetris
{
    class GameBoyTetris : GameBoy
    {
        Random random;
        Timer timer;
        //Наследуем конструктор класса GameBoy, base - позволяет унаследовать конструктор
        public GameBoyTetris(DataGridView dataGridView, int height = 22, int width = 12) 
            : base(height, width, dataGridView) 
        {
            timer = new Timer();
            timer.Interval = 500;
            timer.Tick += Timer_Tick;
            random = new Random();
            FigureFactory = new FigureFactoryTetris();
            Figures.Add(FigureFactory.GetFigure(random.Next(FigureFactory.GetCount())));
            //Фигура появляется 
            Figures[0].Left = Width / 2 - Figures[0].N / 2; //посередине
            Figures[0].Top = 1; //вверху
            figureController.Move(Figures[0], 0, 0);
            //ShowInGrid.ShowGrid(dataGridView1, this);
            timer.Enabled = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (figureController.CanMove(Figures[0], 0, 1) == 0)
                figureController.Move(Figures[0], 0, 1);
            else
            {
                for (int i = 0; i < Figures[0].N; i++)
                    for (int j = 0; j < Figures[0].N; j++)
                    {
                        if (Figures[0].Array[i, j, Figures[0].Layer] != 0)
                            Area[Figures[0].Top + i, Figures[0].Left + j] = 10; //Фигура упала, ей присвоилось значение 10
                    }
                Figures[0] = FigureFactory.GetFigure(random.Next(FigureFactory.GetCount()));
                Figures[0].Left = Width / 2 - Figures[0].N / 2;
                Figures[0].Top = 1;
                figureController.Move(Figures[0], 0, 0);
            }
            ShowInGrid.ShowGrid(dataGridView1, this);   //Обновление таблицы
        }
        
        private void Rotate()
        {
            Figures[0].NextLayer();
            if (figureController.CanMove(Figures[0], 0, 0) == 0)
            {
                Clear();
                figureController.CanMove(Figures[0], 0, 0);
            }
            else
            {
                for (int i = 0; i < Figures[0].Count - 1; i++)
                {
                    Figures[0].NextLayer();
                }
            }
        }

        private void Clear()
        {
            for (int i = 0; i < Figures[0].Count - 1; i++)
            {
                Figures[0].NextLayer();
            }
            for (int i = 0; i < Figures[0].N; i++)
                for (int j = 0; j < Figures[0].N; j++)
                {
                    if (Figures[0].Array[i, j, Figures[0].Layer] != 0)
                        Area[Figures[0].Top + i, Figures[0].Left + j] = 0;
                }
            Figures[0].NextLayer();
        }

        public override void Control(int key)
        {
            switch(key)
            {
                //Rotate
                case 1: Rotate(); break;
                //Down
                case 2:
                    if (figureController.CanMove(Figures[0], 0, 1) == 0)
                        figureController.Move(Figures[0], 0, 1); break;
                //Right
                case 3:
                    if (figureController.CanMove(Figures[0], 1, 0) == 0)
                        figureController.Move(Figures[0], 1, 0); break;
                //Left
                case 4:
                    if (figureController.CanMove(Figures[0], -1, 0) == 0)
                        figureController.Move(Figures[0], -1, 0); break;
            }
            ShowInGrid.ShowGrid(dataGridView1, this);
        }
    }
}
