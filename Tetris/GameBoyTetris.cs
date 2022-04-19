using System;
using System.Media;
using System.Windows.Forms;

namespace Tetris
{
    class GameBoyTetris : GameBoy
    {
        Random random;
        //SoundPlayer player;
        //Наследуем конструктор класса GameBoy, base - позволяет унаследовать конструктор
        public GameBoyTetris(DataGridView dataGridView, int height = 22, int width = 12) 
            : base(height, width, dataGridView) 
        {
            timer = new Timer();
            timer.Interval = 500;
            timer.Tick += Timer_Tick;
            //player = new SoundPlayer("C:\\Users\\Admin\\Figures\\tetris - gameboy - 02.wav");
            //player.PlayLooping();
            random = new Random();
            FigureFactory = new FigureFactoryTetris();
            Figures.Add(FigureFactory.GetFigure(random.Next(FigureFactory.GetCount())));
            //Фигура появляется 
            Figures[0].Left = Width / 2 - Figures[0].N / 2; //посередине
            Figures[0].Top = 1; //вверху
            figureController.Move(Figures[0], 0, 0);
            //ShowInGrid.ShowGrid(dataGridView1, this);
            //timer.Enabled = true;
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
                for (int i = Figures[0].Top; i < Figures[0].Top + Figures[0].N; i++)
                {
                    if (CheckLine(i))
                    {
                        Score++;
                        if (500 - Score * 50 > 30)
                            timer.Interval = 500 - Score * 50;
                        else
                            timer.Interval = 10;
                        DestroyLine(i);
                    }    
                }

                Figures[0] = FigureFactory.GetFigure(random.Next(FigureFactory.GetCount()));
                Figures[0].Left = Width / 2 - Figures[0].N / 2;
                Figures[0].Top = 1;
                if(figureController.CanMove(Figures[0],0,0) == 0)
                    figureController.Move(Figures[0], 0, 0);
                else
                {
                    timer.Stop();
                    MessageBox.Show("Вы проиграли! Ваш счёт " + Score.ToString());
                }
            }
            ShowInGrid.ShowGrid(dataGridView1, this);   //Обновление таблицы
        }
        
        bool CheckLine(int n)
        {
            if (n > Height - 1) //Если за пределами массива не проверяем
                return false;
            for (int i = 1; i < Width - 1; i++) //идём от границы до границы
            {
                if (Area[n, i] == 0 || Area[n,i] == -1)    //если линия заполнена не нулями "взрываем" линию
                    return false;
            }
            return true;
        }

        void DestroyLine(int n)
        {
            //Если лини заполнена, линию, которая выше на одну позицию опускаем на взорвавшуюся линию
            for (int i = n; i > 1; i--)
                for (int j = 1; j < Width - 1; j++)
                {
                    Area[i, j] = Area[i - 1, j];
                }
            ShowInGrid.ShowGrid(dataGridView1, this);
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
                //Space
                case 5:
                    int c = 1;
                    while (figureController.CanMove(Figures[0], 0, c) == 0) //Пока фигура может падать
                        c++;    //Считаем c
                    figureController.Move(Figures[0], 0, c-1);
                    break;
            }
            ShowInGrid.ShowGrid(dataGridView1, this);
        }

        public override void StartGame()
        {
            timer.Enabled = true;
        }

        public override void PauseGame()
        {
            timer.Enabled = false;
        }
    }
}
