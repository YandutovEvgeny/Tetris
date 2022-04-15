using System.Collections.Generic;

namespace Tetris
{
    class FigureFactoryTetris : IFigureFactory
    {
        List<Figure> figures;
        IFigureCreator figureFromFile;

        //TODO:Добавить больше фигур
        public FigureFactoryTetris()
        {
            figureFromFile = new FigureFromFile();
            figures = new List<Figure>()
            {
                //Создаём фигуры
                figureFromFile.CreateFigure("C:\\Users\\Admin\\TFigure.txt"),    //T-фигура
                figureFromFile.CreateFigure("C:\\Users\\Admin\\LFigure.txt"),    //L-фигура
                figureFromFile.CreateFigure("C:\\Users\\Admin\\StickFigure.txt") //Палочка
            };
        }

        public int GetCount()
        {
            return figures.Count;   //возвращаем количество фигур в фабрике
        }

        public Figure GetFigure(int num)
        {
            return figures[num % figures.Count];    //Запрашиваем фигуру
        }
    }

}
