using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class FigureController
    {
        GameBoy gameBoy;
        public FigureController(GameBoy gameBoy)
        {
            this.gameBoy = gameBoy;
        }
        public int CanMove(Figure figure, int Left, int Top)
        {
            int ft = figure.Top;
            int fl = figure.Left;
            int layer = figure.Layer;

            for (int i = 0; i < figure.N; i++)
            {
                for (int j = 0; j < figure.N; j++)
                {
                    if (!(ft + Top + i >= 0 && 
                        ft + Top + i < gameBoy.Height && 
                        fl + Left + j >= 0 && 
                        fl + Left + j < gameBoy.Width
                        ))
                         return -1;

                    //Если в фигуре не пустая клетка и в гейм бое не пустая клетка
                    if (figure.Array[i, j, layer] != 0 &&
                       gameBoy.Area[ft + Top, fl + Left] != 0 &&
                       gameBoy.Area[ft + Top, fl + Left] != figure.Array[i, j, layer]
                       )
                        return gameBoy.Area[ft + Top, fl + Left];
                }
            }
            return 0;
        }
        public void Move(Figure figure, int Left, int Top)
        {
            int ft = figure.Top;
            int fl = figure.Left;
            int layer = figure.Layer;

            for (int i = 0; i < figure.N; i++)
            {
                for (int j = 0; j < figure.N; j++)
                {
                    if(gameBoy.Area[i+ft,j+fl] == figure.Array[i,j,layer]
                        && figure.Array[i, j, layer]!= 0)
                    gameBoy.Area[i + ft, j + fl] = 0;
                }
            }
            figure.Left += Left;
            figure.Top += Top;
            
            for (int i = 0; i < figure.N; i++)
            {
                for (int j = 0; j < figure.N; j++)
                {
                    gameBoy.Area[i + figure.Top, j + figure.Left] = figure.Array[i,j,layer];
                }
            }
        }
    }
}
