using System;
using System.IO;

namespace Tetris
{
    class FigureFromFile : IFigureCreator
    {
        public Figure CreateFigure(string filePath)
        {
            //Объект для работы с файлами
            FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
            //Считывает поток
            StreamReader reader = new StreamReader(fileStream);
            //Считываем данные с блокнота
            int n = Convert.ToInt32(reader.ReadLine());
            int count = Convert.ToInt32(reader.ReadLine()); //ReadLine - читает строку
            
            int[,,] buffer = new int[n, n, count];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    buffer[i, j, 0] = reader.Read() - 48;    //Read - читает символ

            for(int i= 0;i<count - 1;i++)
                Rotate(buffer, n, i);

            fileStream.Close(); //Закрываем поток
            return new Figure(buffer, n, count);
        }

        void Rotate(int[,,] m, int n, int layer)    //Метод поворотa фигуры
        {
            for (int i = 0; i < n; i++) 
                for (int j = 0; j < n; j++)
                    m[i, j, layer + 1] = m[n - j - 1, i, layer];
        }
    }
}
