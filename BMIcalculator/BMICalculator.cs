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
    public partial class BMICalculator : Form
    {
        // CLASS PROPERTIES
        // PRIVATE DATA MEMBERS
        private TextBox m_activeLabel;

        ///CLASS PROPERTIES
        public string outputString { get; set; }
        public float outputValue { get; set; }
        public bool decimalExists { get; set; }
        public double Bmiresult { get; set; }

        /// <summary>
        /// This is the constructor method
        /// </summary>
        public BMICalculator()
        {
            InitializeComponent();
        }

        public TextBox ActiveLabel
        {
            get
            {
                return m_activeLabel;
            }

            set
            {
                // check if m_activeLabel is already pointing at a label
                if (m_activeLabel != null)
                {
                    m_activeLabel.BackColor = Color.White;
                }

                m_activeLabel = value;

                // check if m_activeLabel has not been set to null
                if (m_activeLabel != null)
                {
                    m_activeLabel.BackColor = Color.LightBlue;
                }

            }
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
                ActiveLabel.Text = outputValue.ToString();
                clearNumericKeyboard();
                NumberButtonTableLayoutPanel.Visible = false;
            
        }

        /// <summary>
        /// This is the event handler for the ActiveLabel click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActiveLabel_Click(object sender, EventArgs e)
        {

            if (ActiveLabel != null)
            {
                clearNumericKeyboard();
            }

            ActiveLabel = sender as TextBox;
            ActiveLabel.BackColor = Color.LightBlue;

            NumberButtonTableLayoutPanel.BringToFront();

            if (ActiveLabel.Text != "0")
            {
                outputString = ActiveLabel.Text;
                ResultLabel.Text = ActiveLabel.Text;
            }


            NumberButtonTableLayoutPanel.Visible = true;
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

            ActiveLabel.Text = outputString;
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
        /// This is the event handler for the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            clearNumericKeyboard();
            NumberButtonTableLayoutPanel.Visible = false;
        }

        /// <summary>
        /// This is the event handler for the Heightlabel click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HeightLabel_Click(object sender, EventArgs e)
        {
            NumberButtonTableLayoutPanel.Visible = true;
        }

        /// <summary>
        /// This is the event handler for the WeightLabel click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WeightLabel_Click(object sender, EventArgs e)
        {
            NumberButtonTableLayoutPanel.Visible = true;
        }

        /// <summary>
        /// This event handler will calculate the BMI as per entered values 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BMIButton_Click(object sender, EventArgs e)
        {
            double Height = Convert.ToDouble(HeightLabel.Text);
            double Weight = Convert.ToDouble(WeightLabel.Text);

            //Will Calculate BMI
            if (ImperialButton.Checked)
            {
                Bmiresult = Weight * 703 / (Math.Pow(Height, 2));
                BMIResult.Text = $"Your BMI : {Math.Round(Bmiresult,5).ToString()}";
                
            }
            else if (MetricButton.Checked)
            {
                Bmiresult = Weight / (Math.Pow(Height, 2));
                BMIResult.Text = $"Your BMI : {Math.Round(Bmiresult, 5).ToString()}";
            }

            //For BMI Scale
            double bmi = Math.Round(Bmiresult, 2);
            if (bmi < 18.5)
            {
                BMIScale.Text = "You are UnderWeight";
            }
            else if (bmi >= 18.5 && bmi <= 24.9)
            {
                BMIScale.Text = "You are Normal";
            }
            else if (bmi >= 25 && bmi <= 29.9)
            {
                BMIScale.Text = "You are OverWeight";
            }
            else if (bmi >= 30)
            {
                BMIScale.Text = "You are Obese";
            }
        }

        /// <summary>
        /// This is the event handler for the Metric Radio Button Checked event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MetricButton_CheckedChanged(object sender, EventArgs e)
        {
            if(MetricButton.Checked)
            {
                HeightLabelM.Text = "M";
                WeightLabelM.Text = "Kg";
            }
        }

        /// <summary>
        /// This is the event handler for the Imperial Radio Button Checked event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImperialButton_CheckedChanged(object sender, EventArgs e)
        {
            if(ImperialButton.Checked)
            {
                HeightLabelM.Text = "Inch";
                WeightLabelM.Text = "Pounds";
            }
        }

        /// <summary>
        /// This is the event handler for the ResetButton Click event, It will reset the whole page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetButton_Click(object sender, EventArgs e)
        {
            clearNumericKeyboard();
            NumberButtonTableLayoutPanel.Visible = false;
            HeightLabel.Text = "";
            WeightLabel.Text = "";
            BMIResult.Text = "Enter Your Height and Weight";
            BMIScale.Text = "BMI Scale";
        }

    }
}
