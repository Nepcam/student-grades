using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace student_grades
{
    public partial class Form1 : Form
    {
        //Name: Cameron Nepe
        //ID  : 1262199

        public Form1()
        {
            InitializeComponent();
        }

        //DECLARE variables

        //The width of a bar in the bar graph
        const int BAR_WIDTH = 25;
        //SET the array variables
        string[] idArray = new string[TOTAL_STUDENTS];
        string[] marksArray = new string[TOTAL_STUDENTS];
        //TOTAL number of students (size of the idArray and marksArray)
        const int TOTAL_STUDENTS = 100;
        //CALCULATE the bar height
        int mark = 0;
        int barHeight = 0;
        int x = 0;
        int y = 0;
        int length = 0;

        private void clearGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBoxGraph.Refresh();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Draws a vertical bar that is part of a bar graph.
        /// i.e. It fills a rectangle at position (x,y) with the specified colour.
        /// Then draws a black outline for the rectangle.
        /// Uses the BAR_WIDTH constant for the size of the rectangle.
        /// </summary>
        /// <param name="paper">The Graphics object to draw on.</param>
        /// <param name="x">The x position of the top left corner of the rectangle.</param>
        /// <param name="y">The y position of the top left corner of the rectangle.</param>
        /// <param name="colour">The colour to fill the background of the rectangle with.</param>
        private void DrawABar(Graphics paper, int x, int y, int length, Color colour)
        {
            //create a brush of specified colour and fill background with this colour 
            SolidBrush brush = new SolidBrush(colour);
            paper.FillRectangle(brush, x, y, BAR_WIDTH, length);

            //draw outline in black
            paper.DrawRectangle(Pens.Black, x, y, BAR_WIDTH, length);
        }

        private void loadMarksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string FILTER = "CSV FIles|*.csv|ALL Files|*.*";
            //GET the file reader 
            StreamReader reader;

            string line = "", objectType = "";

            List<int> mark = new List<int>();

            //SET the filter for the dialog control
            openFileDialog1.Filter = FILTER;
            //IF(user selects a file) THEN
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open input file
                reader = File.OpenText(openFileDialog1.FileName);
                //WHILE not end of file
                int count = 0;
                while (!reader.EndOfStream)
                {
                    try
                    {
                        //READ a whole csv line from the file
                        line = reader.ReadLine();
                        //SPLIT the values from the line using an array
                        idArray = line.Split(',');
                        marksArray = line.Split(',');

                        //EXTRACT values into separate variable
                        objectType = idArray[0];
                        mark.Add(int.Parse(marksArray[1]));

                        //DISPLAY the values into the listbox neatly padded out
                        listBoxDisplay.Items.Add(objectType.PadRight(10) + mark[count].ToString().PadRight(5));
                        count++;
                    }
                    catch
                    {
                        Console.WriteLine("Error " + line);
                    }
                }
                reader.Close();
            }
            else
            {
                MessageBox.Show("Error " + line);
            }
        }

        /// <summary>
        /// This is passed a mark and returns the height of the bar for the graph as an integer.
        /// </summary>
        /// <param name="mark"></param>
        /// <param name="barHeight"></param>
        /// <returns></returns>
        private int CalculateBarHeight(int mark, int barHeight)
        {
            barHeight = pictureBoxGraph.Height * mark;
            return barHeight;
        }

        private void graphMarksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics canvas = pictureBoxGraph.CreateGraphics();
            DrawABar(canvas, x, y, length, Color.Blue);
        }

        //private int CalcuLetterGrade(int mark)
        //{
        //    //CHECK marks and retrun a letter grade
        //    if (mark >= 80 && mark <= 100)
        //    {
        //        //Return a A Grade
        //    }
        //    else if (mark >= 65 && mark <= 79)
        //    {
        //        //Return a B Grade
        //    }
        //    else if (mark >= 50 && mark <= 64)
        //    {
        //        //Return a C Grade
        //    }
        //    else if (mark >= 35 && mark <= 49)
        //    {
        //        //Return a D Grade
        //    }
        //    else if (mark <= 34)
        //    {
        //        //Return a E Grade
        //    }
        //}

        /// <summary>
        /// This method will write to a textfile the id, mark and letter grade of each student
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void generateReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //GET Writer
            StreamWriter writer;
            string filename = "Report.txt";
            //CREATE text file
            writer = File.CreateText(filename);
            //WRITE the header line
            writer.WriteLine("This is a test");
            writer.Close();
            MessageBox.Show("File \"" + filename + "\" test.");
        }
    }
}
