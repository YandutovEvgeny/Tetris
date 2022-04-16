using System.Collections.Generic;

namespace Tetris
{
    class FigureFactoryTetris : IFigureFactory
    {
        List<Figure> figures;
        IFigureCreator figureFromFile;

        //TODO:Добавить больше фигур DONE
        public FigureFactoryTetris()
        {
            figureFromFile = new FigureFromFile();
            figures = new List<Figure>()
            {
                //Создаём фигуры
                figureFromFile.CreateFigure("C:\\Users\\Admin\\Figures\\TFigure.txt"),     //T-фигура
                figureFromFile.CreateFigure("C:\\Users\\Admin\\Figures\\LFigure.txt"),     //L-фигура
                figureFromFile.CreateFigure("C:\\Users\\Admin\\Figures\\StickFigure.txt"), //Палочка
                figureFromFile.CreateFigure("C:\\Users\\Admin\\Figures\\ZFigure.txt"),     //Z-фигура
                figureFromFile.CreateFigure("C:\\Users\\Admin\\Figures\\Square.txt")       //Квадрат
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
