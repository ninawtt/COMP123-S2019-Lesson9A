using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMP123_S2019_Lesson9A
{
    public partial class Lesson9Form : Form
    {
        /// <summary>
        /// This is the Constructor Method
        /// </summary>
        public Lesson9Form()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This is the shared Event Handler for all of  the Calculator Buttons 'Click Event"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculatorButtons_Click(object sender, EventArgs e)
        {
            Button selectedButton = sender as Button;
            //Button selectedButton = (Button)sender;
            //ResultLabel.Text = selectedButton.Text;

            try
            {
                int.Parse(selectedButton.Text);
                ResultLabel.Text = selectedButton.Text;
            }
            catch (Exception)
            {
                ResultLabel.Text = "Not a Number";
            }

            //switch (selectedButton.Text)
            //{
            //    case "1":
            //        ResultLabel.Text = "1";
            //        break;
            //    default:
            //        break;
            //}
        }
    }
}
