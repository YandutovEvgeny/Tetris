using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class GameBoy  
    {
        public int[,] Area { get; set; }  //Поле
        public int Score { get; protected set; }    //Очки
        public int Level { get; protected set; }    //Уровень сложности
        public int Width { get; private set; }      //Ширина
        public int Height { get; private set; }     //Высота

        protected List<Figure> Figures { get; set; }

        public GameBoy(int height, int width)
        {
            Area = new int[height, width];
            Height = height;
            Width = width;
            Score = 0;
            Level = 1;

            //Ограничиваем игровое поле стенками и полом
            for (int i = 0; i < height; i++)
            {
                Area[i, width - 1] = -1;
                Area[i, 0] = -1;
            }
            for (int i = 0; i < width; i++)
            {
                Area[height-1, i] = -1;
                Area[0,i] = -1;
            }
        }

        //public abstract void Control(int key);
    }
}
