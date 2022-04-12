using System;
using System.IO;

namespace Tetris
{
    class FigureFromFile : IFigureCreator
    {
        public Figure CreateFigure(string filePath)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
            StreamReader reader = new StreamReader(fileStream);
            
            int n = Convert.ToInt32(reader.ReadLine());
            int count = Convert.ToInt32(reader.ReadLine()); //ReadLine - читает строку
            
            int[,,] buffer = new int[n, n, count];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    buffer[i, j, 0] = reader.Read();    //Read - читает символ
        }

        public static int[,] Rotate(int[,] m, int n)    //Метод поворотa фигуры
        {
            int[,] result = new int[n,n];
            
            for (int i = 0; i < n; i++) 
                for (int j = 0; j < n; j++)
                    result[i, j] = m[n - j - 1, i]; 

            return result;
        }
    }
}
