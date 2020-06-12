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

        //The width of a bar in the bar graph
        const int BAR_WIDTH = 25;

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
            //Set filter of dialog control
            const string FILTER = "CSV FIles|*.csv|ALL Files|*.*";
            //GET the file reader 
            StreamReader reader;

            string line = "", objectType = "";
            // SET the array variables
            string[] idArray;
            string[] marksArray;

            List<int> id = new List<int>();
            List<int> mark = new List<int>()

                //IF(user selects a file) THEN
                    //Open input file
                    //WHILE not end of file
                    //Your pseudo - code goes here
                    //ENDWHILE
                    //Close file
                    //Display message that file loaded
                //ENDIF
        }
    }
}
