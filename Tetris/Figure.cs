using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Figure
    {
        public int[,,] Array { get; private set; }
        public int N { get; private set; }
        public int Count { get; private set; }

        public Figure(int[,,] array, int n, int count)
        {
            Array = array;  //Фигура
            N = n;          //Ширина и высота
            Count = count;  //Глубина
        }
    }
}
