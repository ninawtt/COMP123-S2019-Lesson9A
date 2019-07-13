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
        // PRIVATE DATA MEMBERS
        private Label m_activeLabel;

        // PUBLIC PROPERTIES
        public string outputString { get; set; }
        public float outputValue { get; set; }
        public bool decimalExists { get; set; }

        public Label ActiveLabel {
            get
            {
                return m_activeLabel;
            }
            set
            {
                // check if m_activeLabel is already pointing at a label
                if(m_activeLabel != null)
                {
                    m_activeLabel.BackColor = Color.White;
                }

                m_activeLabel = value;

                // check if m_activeLabel has not been set to null
                if(m_activeLabel != null)
                {
                    m_activeLabel.BackColor = Color.LightBlue;
                }
                
            }
        }

        /// <summary>
        /// This is the Constructor Method
        /// </summary>
        public CalculateForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This is the event handler for the form load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculateForm_Load(object sender, EventArgs e)
        {
            this.Size = new Size(320, 480);

            clearNumericKeyboard();
            NumberButtonTableLayoutPanel.Visible = false;
        }

        /// <summary>
        /// Rhis is the event handler for the form click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculatorForm_Click(object sender, EventArgs e)
        {
            NumberButtonTableLayoutPanel.Visible = false;

            ActiveLabel = null;
            clearNumericKeyboard();
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
                else if (outputString.Length < maxSize)   //???? if outputstring !=0 and outputstring.length < maxSize?
                {
                        outputString += tag;
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
                        clearNumericKeyboard();
                        break;
                    case "decimal":
                        addDecimalToResultLabel();
                        break;
                }
            }
        }

        /// <summary>
        /// This method adds a decimal point to the ResultLabel
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
        /// This method finalizes and converts the outputString to a floating point values
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

            outputValue = (float)(Math.Round(outputValue, 1));
            if (outputValue < 0.1f)
            {
                outputValue = 0.1f;
            }
            ActiveLabel.Text = outputValue.ToString();
            clearNumericKeyboard();
            NumberButtonTableLayoutPanel.Visible = false;
            ActiveLabel.BackColor = Color.White;
            ActiveLabel = null;
        }
        /// <summary>
        ///  This method removes the last character from the ResultLabel  
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
        /// This method resets the numeric keyboard and related variables 
        /// </summary>
        private void clearNumericKeyboard()
        {
            ResultLabel.Text = "0";
            outputString = "0";
            outputValue = 0.0f;
            decimalExists = false;
        }
        

        /// <summary>
        /// This is the event handler for the HeightLabel click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActiveLabel_Click(object sender, EventArgs e)
        {
            if(ActiveLabel != null)
            {
                clearNumericKeyboard();
            }

            ActiveLabel = sender as Label;
            ActiveLabel.BackColor = Color.LightBlue;

            AnimationTimer.Enabled = true;
            //NumberButtonTableLayoutPanel.Location = new Point(12, ActiveLabel.Location.Y + 55);
            NumberButtonTableLayoutPanel.BringToFront();

            if (ActiveLabel.Text != "0")          //if the label already has the value, when the user click again, it should display the same value on the keyboard
            {
                outputString = ActiveLabel.Text;
                ResultLabel.Text = ActiveLabel.Text;
            }

            NumberButtonTableLayoutPanel.Visible = true;
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            var currentLocation = NumberButtonTableLayoutPanel.Location;
            NumberButtonTableLayoutPanel.Location = new Point(currentLocation.X, currentLocation.Y - 20);
            if (NumberButtonTableLayoutPanel.Location.Y <= ActiveLabel.Location.Y + 55)
            {
                AnimationTimer.Enabled = false;
                if(NumberButtonTableLayoutPanel.Location.Y < ActiveLabel.Location.Y + 55)
                {
                    NumberButtonTableLayoutPanel.Location = new Point(currentLocation.X, ActiveLabel.Location.Y + 55);
                }
            }
        }
    }
}
