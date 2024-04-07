using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Intel_1
{
    public partial class MainForm : Form
    {
        private Maze currentMaze;

        public MainForm()
        {
            InitializeComponent(); 
            GenerateMazeSelection();
        }

        private void GenerateMazeSelection()
        {
            mazeSelectionComboBox.Items.Add("Labirintas 1");
            mazeSelectionComboBox.Items.Add("Labirintas 2");
            mazeSelectionComboBox.Items.Add("Labirintas 3");
        }

        private void DrawMaze()
        {
            if (currentMaze == null) return;

            var bmp = new Bitmap(pictureBoxBFS.Width, pictureBoxBFS.Height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White); 

                int cellWidth = pictureBoxBFS.Width / currentMaze.Grid.GetLength(1);
                int cellHeight = pictureBoxBFS.Height / currentMaze.Grid.GetLength(0);

                for (int y = 0; y < currentMaze.Grid.GetLength(0); y++)
                {
                    for (int x = 0; x < currentMaze.Grid.GetLength(1); x++)
                    {
                        if (currentMaze.Grid[y, x] == 1) // Siena
                        {
                            g.FillRectangle(Brushes.Black, x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                        }
                        else if (currentMaze.Start.X == x && currentMaze.Start.Y == y) 
                        {
                            g.FillRectangle(Brushes.Green, x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                        }
                        else if (currentMaze.End.X == x && currentMaze.End.Y == y) 
                        {
                            g.FillRectangle(Brushes.Red, x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                        }
                    }
                }
            }
            pictureBoxBFS.Image = bmp;
        }

        private void DrawMaze2()
        {
            if (currentMaze == null) return;

            var bmp = new Bitmap(pictureBoxDFS.Width, pictureBoxDFS.Height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White); 

                int cellWidth = pictureBoxDFS.Width / currentMaze.Grid.GetLength(1);
                int cellHeight = pictureBoxDFS.Height / currentMaze.Grid.GetLength(0);

                for (int y = 0; y < currentMaze.Grid.GetLength(0); y++)
                {
                    for (int x = 0; x < currentMaze.Grid.GetLength(1); x++)
                    {
                        if (currentMaze.Grid[y, x] == 1) 
                        {
                            g.FillRectangle(Brushes.Black, x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                        }
                        else if (currentMaze.Start.X == x && currentMaze.Start.Y == y)
                        {
                            g.FillRectangle(Brushes.Green, x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                        }
                        else if (currentMaze.End.X == x && currentMaze.End.Y == y) 
                        {
                            g.FillRectangle(Brushes.Red, x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                        }
                    }
                }
            }
            pictureBoxDFS.Image = bmp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (currentMaze == null) return;

            DrawMaze();
            DrawMaze2();

            var (bfsDuration, dfsDuration, bfsPathLength, dfsPathLength) = Evaluation.CompareAlgorithms(currentMaze);

            textBox1.Text = $"{bfsDuration.TotalMilliseconds:#,0.00} ms";
            textBox2.Text = bfsPathLength.ToString();
            textBox3.Text = $"{dfsDuration.TotalMilliseconds:#,0.00} ms";
            textBox4.Text = dfsPathLength.ToString();

            var bfsPath = BFS.FindPath(currentMaze);
            HighlightPath(bfsPath, pictureBoxBFS); 

            var dfsPath = DFS.FindPath(currentMaze);
            HighlightPath2(dfsPath, pictureBoxDFS); 
        }

        private void HighlightPath(List<Point> path, PictureBox pictureBoxBFS)
        {
            if (currentMaze == null || path == null || path.Count == 0) return;

            var bmp = pictureBoxBFS.Image as Bitmap;
            using (var g = Graphics.FromImage(bmp))
            {
                int cellWidth = pictureBoxBFS.Width / currentMaze.Grid.GetLength(1);
                int cellHeight = pictureBoxBFS.Height / currentMaze.Grid.GetLength(0);

                foreach (var p in path)
                {
                    g.FillRectangle(Brushes.Blue, p.X * cellWidth, p.Y * cellHeight, cellWidth, cellHeight);
                }
            }
            pictureBoxBFS.Image = bmp;
        }

        private void HighlightPath2(List<Point> path, PictureBox pictureBoxDFS)
        {
            if (currentMaze == null || path == null || path.Count == 0) return;

            var bmp = pictureBoxDFS.Image as Bitmap;
            using (var g = Graphics.FromImage(bmp))
            {
                int cellWidth = pictureBoxDFS.Width / currentMaze.Grid.GetLength(1);
                int cellHeight = pictureBoxDFS.Height / currentMaze.Grid.GetLength(0);

                foreach (var p in path)
                {
                    g.FillRectangle(Brushes.Blue, p.X * cellWidth, p.Y * cellHeight, cellWidth, cellHeight);
                }
            }
            pictureBoxDFS.Image = bmp;
        }

        private void mazeSelectionComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            switch (mazeSelectionComboBox.SelectedIndex)
            {
                case 0:
                    currentMaze = MazeGenerator.GenerateMaze1();
                    break;
                case 1:
                    currentMaze = MazeGenerator.GenerateMaze2();
                    break;
                case 2:
                    currentMaze = MazeGenerator.GenerateMaze3();
                    break;
            }
            DrawMaze2();
            DrawMaze();
        }
    }
}