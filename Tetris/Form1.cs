using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Form1 : Form
    {
        GameBoy gameBoy;
        //SoundPlayer player;
        public Form1()
        {
            InitializeComponent();
            /*gameBoy = new GameBoyTetris(dataGridView1);
            dataGridView1.RowCount = gameBoy.Height;
            dataGridView1.ColumnCount = gameBoy.Width;

            for (int i = 0; i < gameBoy.Height; i++)
                dataGridView1.Rows[i].Height = 20;

            for (int i = 0; i < gameBoy.Width; i++)
                dataGridView1.Columns[i].Width = 20;*/
        }

        void ShowGrid()
        {
            for (int i = 0; i < gameBoy.Height; i++)
            {
                for (int j = 0; j < gameBoy.Width; j++)
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

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if(gameBoy != null)
                label1.Text = "Score: " + gameBoy.Score.ToString();
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
                gameBoy.Control(1);
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
                gameBoy.Control(2);
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
                gameBoy.Control(3);
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
                gameBoy.Control(4);
            if (e.KeyCode == Keys.Space)
                gameBoy.Control(5);
            
            dataGridView1.ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Focus();
            if(gameBoy != null)
            {
                gameBoy.timer.Dispose();
            }
            gameBoy = new GameBoyTetris(dataGridView1);
            dataGridView1.RowCount = gameBoy.Height;
            dataGridView1.ColumnCount = gameBoy.Width;

            for (int i = 0; i < gameBoy.Height; i++)
                dataGridView1.Rows[i].Height = 20;

            for (int i = 0; i < gameBoy.Width; i++)
                dataGridView1.Columns[i].Width = 20;
            gameBoy.StartGame();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Focus();
            if (gameBoy != null)
            {
                gameBoy.timer.Dispose();
            }
            gameBoy = new GameBoyRally(dataGridView1);
            dataGridView1.RowCount = gameBoy.Height;
            dataGridView1.ColumnCount = gameBoy.Width;

            for (int i = 0; i < gameBoy.Height; i++)
                dataGridView1.Rows[i].Height = 20;

            for (int i = 0; i < gameBoy.Width; i++)
                dataGridView1.Columns[i].Width = 20;
            gameBoy.StartGame();
        }
    }
}
