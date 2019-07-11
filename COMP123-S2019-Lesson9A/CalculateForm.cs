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
    public partial class CalculateForm : Form
    {
        // CLASS PROPERTIES
        public string outputString { get; set; }
        public float outputValue { get; set; }
        public bool decimalExists { get; set; }


        /// <summary>
        /// This is the Constructor Method
        /// </summary>
        public CalculateForm()
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
            //Button selectedButton = sender as Button;
            ////Button selectedButton = (Button)sender;
            ////ResultLabel.Text = selectedButton.Text;

            //try
            //{
            //    int.Parse(selectedButton.Text);
            //    ResultLabel.Text = selectedButton.Text;
            //}
            //catch (Exception)
            //{
            //    ResultLabel.Text = "Not a Number";
            //}

            ////switch (selectedButton.Text)
            ////{
            ////    case "1":
            ////        ResultLabel.Text = "1";
            ////        break;
            ////    default:
            ////        break;
            ////}
            ///

            Button TheButton = sender as Button;
            var tag = TheButton.Tag.ToString();
            int numericValue = 0;

            bool numericResult = int.TryParse(tag, out numericValue);

            if(numericResult)
            {
                int maxSize = (decimalExists) ? 5 : 3;

                if(outputString == "0")
                {
                    outputString = tag;
                }
                else
                {
                    if(outputString.Length < maxSize)
                    {
                        outputString += tag;
                    }
                }
                    

                ResultLabel.Text = outputString;
            }
            else
            {
                switch(tag)
                {
                    case "back":
                        removeLastCharacterFromResultLabel();
                        break;
                    case "done":
                        finalizeOutput();
                        break;
                    case "clear":
                        ClearNumericKeyboard();
                        break;
                    case "decimal":
                        addDecimalToResultLabel();
                        break;
                }
            }
        }

        /// <summary>
        /// This method adds a decimal point to the resultLabel
        /// </summary>
        private void addDecimalToResultLabel()
        {
            if (!decimalExists)
            {
                outputString += ".";
                decimalExists = true;
            }
        }
        /// <summary>
        /// This method finalizes and converts the outputString to a floating point 
        /// </summary>
        private void finalizeOutput()
        {
            //if (decimalExists)
            //{
            //    if (outputString.Length > 3)
            //    {
            //        int charactersToRemove = outputString.IndexOf('.') + 2;
            //        outputString = outputString.Remove(charactersToRemove);
            //    }
            //}
            outputValue = float.Parse(outputString);

            outputValue = (float)Math.Round(outputValue, 1);
            if (outputValue < 0.1f)
            {
                outputValue = 0.1f;
            }
            HeightLabel.Text = outputValue.ToString();
            ClearNumericKeyboard();
            NumberButtonTableLayoutPanel.Visible = false;
        }
        /// <summary>
        /// 
        /// </summary>
        private void removeLastCharacterFromResultLabel()
        {
            var lastChar = outputString.Substring(outputString.Length - 1);
            if (lastChar == ".")
            {
                decimalExists = false;
            }
            outputString = outputString.Remove(outputString.Length - 1);

            if (outputString.Length == 0)
            {
                outputString = "0";
            }

            ResultLabel.Text = outputString;
        }

        /// <summary>
        /// This method resets the numeric keyboard and related va
        /// </summary>
        private void ClearNumericKeyboard()
        {
            ResultLabel.Text = "0";
            outputString = "0";
            outputValue = 0.0f;
            decimalExists = false;
        }
        /// <summary>
        /// This is the event handler for the form load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculateForm_Load(object sender, EventArgs e)
        {
            ClearNumericKeyboard();
            NumberButtonTableLayoutPanel.Visible = false;
        }

        /// <summary>
        /// This is the event handler for the heightLabel click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HeightLabel_Click(object sender, EventArgs e)
        {
            NumberButtonTableLayoutPanel.Visible = true;
        }
    }
}
