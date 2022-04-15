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
                    if (figure.Array[i, j, layer] != 0) //Если у фигуры пустота
                    {
                        if (!(ft + Top + i >= 0 &&  //Проверяем на выход из массива
                            ft + Top + i < gameBoy.Height &&
                            fl + Left + j >= 0 &&
                            fl + Left + j < gameBoy.Width
                            ))
                            return -1;

                        //Если в фигуре не пустая клетка и в гейм бое не пустая клетка
                        if (gameBoy.Area[ft + i + Top, fl + j + Left] != 0 &&   //Если есть препятствие
                            gameBoy.Area[ft + i +Top, fl + j + Left] != figure.Array[i, j, layer]   //и оно не фигура
                            )
                        {
                            return gameBoy.Area[ft + i + Top, fl + j + Left];   //Возвращает значение той фигуры в которую мы врезались 
                        }
                    }
                }
            }
            return 0;   //Можем подвинуть фигуру
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
                    if (figure.Array[i, j, layer] != 0)
                        if (gameBoy.Area[i+ft,j+fl] == figure.Array[i,j,layer]
                        && figure.Array[i, j, layer]!= 0)
                    gameBoy.Area[i + ft, j + fl] = 0; //Удаляем предыдущее положение массива
                }
            }
            figure.Left += Left;    //Двигаем фигуру куда хотели
            figure.Top += Top;
            
            for (int i = 0; i < figure.N; i++)
            {
                for (int j = 0; j < figure.N; j++)
                {
                    if(figure.Array[i,j,layer] != 0)
                    gameBoy.Area[i + figure.Top, j + figure.Left] = figure.Array[i,j,layer]; //Заного отрисовали подвинутую фигуру
                }
            }
        }
    }
}
