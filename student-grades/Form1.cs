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
        //int mark = 0;
        //int barHeight = 0;
        //int x = 0;
        //int y = 0;
        //int length = 0;

        /// <summary>
        /// CLEAR the picture box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBoxGraph.Refresh();
        }

        /// <summary>
        /// CLOSES the application 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// CLICK event, read in a line from the file and then extract the id and mark and store the values into two separate arrays.One array for all the ids and one array for all the marks.It
        /// will also display the id and mark of the student in a listbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadMarksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string FILTER = "CSV FIles|*.csv|ALL Files|*.*";
            //GET the file reader 
            StreamReader reader;

            string line = "";

            List<int> markList = new List<int>();
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
                        markList.Add(int.Parse(marksArray[count]));

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

        /// <summary>
        /// CLICK event, goes through each mark that has been stored in the arrays and draws it's bar on the bar graph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        }

        /// <summary>
        /// CALCULATES the grade for each mark
        /// </summary>
        /// <param name="mark"></param>
        /// <returns></returns>
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
            //WRITE to a textfile the id, mark and letter grade of each student

            //GET Writer
            StreamWriter writer;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //OPEN the selected file
                writer = File.CreateText(saveFileDialog1.FileName);

                //WRITE headers
                writer.WriteLine("ID".PadRight(10) + "MARK".PadRight(10) + "GRADE".PadRight(10));

                //FOR each student in the array
                for (int i = 0; i < idArray.Length; i++)
                {
                    //WRITE the student id, mark and grade
                    writer.WriteLine(idArray[i].PadRight(10) + marksArray[i].PadRight(10) + CalcuLetterGrade(int.Parse(marksArray[i])).PadRight(10));
                }
                writer.WriteLine(TOTAL_STUDENTS + " students in the report");
                writer.Close();
            }
        }
    }
}
