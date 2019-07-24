using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMIcalculator
{
    public partial class BMICalc : Form
    {
       
        ///CLASS PROPERTIES
        public string outputString { get; set; }
        public float outputValue { get; set; }
        public bool decimalExists { get; set; }

        /// <summary>
        /// This is the constructor method
        /// </summary>
        public BMICalc()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This is the shared event handler for all of the calculator buttons' click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculatorButton_Click(object sender, EventArgs e)
        {
            Button TheButton = sender as Button;
            var tag = TheButton.Tag.ToString();
            int numericValue = 0;

            bool numericresult = int.TryParse(tag, out numericValue);

            if (numericresult)
            {
                int maxSixe = (decimalExists) ? 5 : 3;
                if (outputString == "0")
                {
                    outputString = tag;
                }
                else
                {
                    if (outputString.Length < maxSixe)
                    {
                        outputString += tag;
                    }
                }
                ResultLabel.Text = outputString;
            }

            else
            {
                switch (tag)
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
        /// This method adds a decimal point to the resultlabel
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
        /// This method finalizes and converts the outputString to a floating point value
        /// </summary>
        private void finalizeOutput()
        {
            outputValue = float.Parse(outputString);

            outputValue = (float)Math.Round(outputValue, 1);
            if (outputValue < 0.1f)
            {
                outputValue = 0.1f;
            }
            Height.Text = outputValue.ToString();
            clearNumericKeyboard();
            NumberButtonTableLayoutPanel.Visible = false;
        }

        /// <summary>
        /// This method removes the last character from the Result Label
        /// </summary>
        private void removeLastCharacterFromResultLabel()
        {
            var LastChar = outputString.Substring(outputString.Length - 1);
            if (LastChar == ".")
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
        /// This is the event handler for the force
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            clearNumericKeyboard();

            NumberButtonTableLayoutPanel.Visible = false;
        }

        /// <summary>
        /// This is the event handler for the hieght label click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HeightLabel_Click(object sender, EventArgs e)
        {
            NumberButtonTableLayoutPanel.Visible = true;
        }
    }
}
