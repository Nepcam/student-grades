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
        const int BAR_WIDTH = 5;
        //SET the array variables
        string[] idArray = new string[TOTAL_STUDENTS];
        string[] marksArray = new string[TOTAL_STUDENTS];
        //const int ID_SIZE = 100;
        //const int MARKS_SIZE = 100;
        //TOTAL number of students
        const int TOTAL_STUDENTS = 100;
        //CALCULATE the bar height
        int mark = 0;
        //int barHeight = 0;
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
            string[] lineArray;

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
                        lineArray = line.Split(',');

                        //EXTRACT values into separate variable                                      
                        idArray[count] = lineArray[0];
                        marksArray[count] = lineArray[1];
                        mark.Add(int.Parse(marksArray[count]));

                        //DISPLAY the values into the listbox neatly padded out
                        listBoxDisplay.Items.Add(idArray[count].PadRight(10) + marksArray[count].PadRight(5));
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
        private int CalculateBarHeight(int mark)
        {
            int barHeight = pictureBoxGraph.Height * mark / 100;
            return barHeight;
        }

        private void graphMarksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Variables
            int barHeight = 0;
            int x = 0;
            int y = 0;
            Graphics paper = pictureBoxGraph.CreateGraphics();

            for (int i = 0; i < idArray.Length; i++)
            {
                barHeight = CalculateBarHeight(int.Parse(marksArray[i]));
                y = pictureBoxGraph.Height - barHeight;
                DrawABar(paper, x, y, barHeight, Color.Red);
                x += BAR_WIDTH;
            }
        

            //const string FILTER = "CSV FIles|*.csv|ALL Files|*.*";
            ////GET the file reader
            //StreamReader marksReader;

            //string line = "";
            //List<int> finalMark = new List<int>();
            ////SET the filter for the dialog control
            //openFileDialog1.Filter = FILTER;
            ////IF user selects a file, THEN
            //if (openFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    //OPEN input file
            //    marksReader = File.OpenText(openFileDialog1.FileName);
            //    //WHILE not end of file
                //int i = 0;
                //while(!marksReader.EndOfStream)
                //{
                //    try
                //    {
                //        //READ a whole csv line from the file
                //        line = marksReader.ReadLine();
                //        //SPLIT the values from the line using an array
                //        idArray = line.Split(',');
                //        marksArray = line.Split(',');
                //        //EXTRACT values into separate variable
                //        finalMark.Add(int.Parse(marksArray[1]));


                //        //DRAW the values into a bar graph
                //        Graphics canvas = pictureBoxGraph.CreateGraphics();
                //        DrawABar(canvas, x, y, (int)finalMark[i], Color.Red);
                //        i++;
                //    }
                //    catch
                //    {

                //    }
                //}
                //marksReader.Close();
            }


        //Graphics canvas = pictureBoxGraph.CreateGraphics();
        //mark = int.Parse(marksArray[1]);
        //y = pictureBoxGraph.Height;
        //DrawABar(canvas, x, y, mark, Color.Blue);
        //x += BAR_WIDTH;


        private string CalcuLetterGrade(int mark)
        {
            //CHECK marks and retrun a letter grade
            if (mark >= 80)
            {
                //Return a A Grade
                return "A";
            }
            else if (mark >= 65)
            {
                //Return a B Grade
                return "B";
            }
            else if (mark >= 50)
            {
                //Return a C Grade
                return "C";
            }
            else if (mark >= 35)
            {
                //Return a D Grade
                return "D";
            }
            else
            {
                //Return a E Grade
                return "E";
            }
        }

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

            //for everthing in the marksArray 

            //WRITE the header line
            writer.WriteLine("This is a test");
            writer.Close();
            MessageBox.Show("File \"" + filename + "\" test.");
        }
    }
}
