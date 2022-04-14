using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Form1 : Form
    {
        Figure figure;
        int count = 0;
        GameBoy gameBoy;
        FigureController figureController;
        public Form1()
        {
            InitializeComponent();
            IFigureCreator figureFromFile = new FigureFromFile();
            figure = figureFromFile.CreateFigure("C:\\Users\\Admin\\TFigure.txt");

            gameBoy = new GameBoy(10, 10);
            figureController = new FigureController(gameBoy);
            dataGridView1.RowCount = 10;
            dataGridView1.ColumnCount = 10;

            for (int i = 0; i < 10; i++)
            {
                dataGridView1.Rows[i].Height = 20;
                dataGridView1.Columns[i].Width = 20;
            }
            figure.Left = 3;
            figure.Top = 3;
        }

        void ShowGrid()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (gameBoy.Area[i, j] == -1)
                        dataGridView1[j, i].Style.BackColor = Color.Black;
                    else if (gameBoy.Area[i, j] == 0)
                        dataGridView1[j, i].Style.BackColor = Color.White;
                    else
                        dataGridView1[j, i].Style.BackColor = Color.Red;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string result = "";
            for (int i = 0; i < figure.N; i++)
            {
                for (int j = 0; j < figure.N; j++)
                {
                    result += figure.Array[i, j, figure.Layer].ToString();
                }
                result += "\n";
            }
            figure.NextLayer();
            MessageBox.Show(result);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (figureController.CanMove(figure, 0, -1) == 0)
                figureController.Move(figure, 0, -1);
            ShowGrid();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (figureController.CanMove(figure, 0, 1) == 0)
                figureController.Move(figure, 0, 1);
            ShowGrid();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (figureController.CanMove(figure, -1, 0) == 0)
                figureController.Move(figure, -1, 0);
            ShowGrid();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (figureController.CanMove(figure, 1, 0) == 0)
                figureController.Move(figure, 1, 0);
            ShowGrid();
        }
    }
}
