using BottleBuilder;
using BottleParameters;
using KompasConnector;
using System;
using System.Drawing;
using System.Windows.Forms;
using static Validator.Validator;

namespace LaboratoryBottle
{
    /// <summary>
    /// A class that stores and processes the user interface of the plugin
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// BottleBuilder
        /// </summary>
        private Builder _bottleBuilder;

        /// <summary>
        /// Parameters
        /// </summary>
        private Parameters _bottleParameters = new Parameters();

        /// <summary>
        /// Variable for connecting with Kompas
        /// </summary>
        public Konnector _kompasConnector = new Konnector();

         /// <summary>
         /// 
         /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// if an incorrect input was made in the combobox,
        /// the input field is colored red, and the build button becomes inactive
        /// </summary>
        /// <param name="comboBox">Combobox in which the input is made</param>
        /// <param name="fieldName">Name of input field</param>
        /// <param name="min">Minimum value of the input field</param>
        /// <param name="max">Maximum value of the input field</param>
        private void ComboboxInputError(ComboBox comboBox, string fieldName, double min, double max)
        {
            int maximumNumbersOfValue = CalculateValueDigitsNumber(max);
            try
            {
                AssertStringOnLength(comboBox.Text, maximumNumbersOfValue, fieldName);
                AssertStringOnNumbers(comboBox.Text, fieldName);

                var value = ConvertStringToDouble(comboBox.Text);
                AssertNumberOnRange(value, min, max, fieldName);
               
                comboBox.BackColor = Color.White;
                buildButton.Enabled = true;

            }
            catch (ArgumentException exception)
            {
                comboBox.BackColor = Color.Salmon;
                buildButton.Enabled = false;
            }
        }

        /// <summary>
        /// Сonverts a string to a number
        /// </summary>
        /// <param name="text">Input string</param>
        /// <returns name="numberResult">Output double</returns>
        private double ConvertStringToDouble(string text)
        {
            double.TryParse(text, out double numberResult);
            return numberResult;
        }

        /// <summary>
        /// Сalculates the number of digits in the integer part of a number
        /// </summary>
        /// <param name="value">Input double</param>
        /// <returns name="valueDigitsNumber">
        /// Output number of digits integer part of double</returns>
        private int CalculateValueDigitsNumber(double value)
        {
            int valueDigitsNumber = 0;
            while (value > 0.99)
            {
                valueDigitsNumber++;
                value /= 10;
            }

            return valueDigitsNumber;
        }

        //TODO: Убрать проверки из mainform
        /// <summary>
        /// Event handler cover radius combobox
        /// </summary>
        private void coverRadiusComboBox_TextUpdate(object sender, EventArgs e)
        {
            var value = ConvertStringToDouble(coverRadiusComboBox.Text);

            ComboboxInputError(coverRadiusComboBox, "cover radius",
                _bottleParameters.CoverRadius.MinimumValue,
                _bottleParameters.CoverRadius.MaximumValue);

            try
            {
                var newParameter = _bottleParameters.CoverRadius;
                newParameter.ParameterValue = value;
                _bottleParameters.CoverRadius = newParameter;

                AssertNumberOnRange(value, _bottleParameters.CoverRadius.MinimumValue,
                    _bottleParameters.CoverRadius.MaximumValue, "cover radius");
                handleBaseRadiusComboBox.Enabled = true;
                handleBaseRadiusLabel.Text = $"(10-" +
                                             $"{_bottleParameters.HandleBaseRadius.MaximumValue.ToString()}) мм";
            }
            catch (Exception exception)
            {
                handleBaseRadiusComboBox.Enabled = false;
            }


        }
        /// <summary>
        /// Event handler handle base radius combobox
        /// </summary>
        private void handleBaseRadiusComboBox_TextUpdate(object sender, EventArgs e)
        {
            var value = ConvertStringToDouble(handleBaseRadiusComboBox.Text);

            ComboboxInputError(handleBaseRadiusComboBox, "handle base radius",
                _bottleParameters.HandleBaseRadius.MinimumValue,
                _bottleParameters.HandleBaseRadius.MaximumValue);

            try
            {
                var newParameter = _bottleParameters.HandleBaseRadius;
                newParameter.ParameterValue = value;
                _bottleParameters.HandleBaseRadius = newParameter;

                AssertNumberOnRange(value, _bottleParameters.HandleBaseRadius.MinimumValue,
                    _bottleParameters.HandleBaseRadius.MaximumValue, "handle base radius");
                handleRadiusLabel.Text = $"({_bottleParameters.HandleRadius.MinimumValue}-" +
                                         $"{_bottleParameters.HandleRadius.MaximumValue}) мм";
                handleRadiusComboBox.Enabled = true;
            }
            catch (Exception exception)
            {
                handleRadiusComboBox.Enabled = false;
            }
        }

        /// <summary>
        /// Event handler handle radius combobox
        /// </summary>
        private void handleRadiusComboBox_TextUpdate(object sender, EventArgs e)
        {
            ComboboxInputError(handleRadiusComboBox, "handle radius",
                _bottleParameters.HandleRadius.MinimumValue,
                _bottleParameters.HandleRadius.MaximumValue);
        }

        /// <summary>
        /// Event handler handle length combobox
        /// </summary>
        private void handleLengthComboBox_TextUpdate(object sender, EventArgs e)
        {
            
            ComboboxInputError(handleLengthComboBox, "handle length",
                _bottleParameters.HandleLength.MinimumValue,
                _bottleParameters.HandleLength.MaximumValue);
        }

        /// <summary>
        /// Event handler height combobox
        /// </summary>
        private void heightComboBox_TextUpdate(object sender, EventArgs e)
        {
            ComboboxInputError(heightComboBox, "height",
                _bottleParameters.Height.MinimumValue,
                _bottleParameters.Height.MaximumValue);
        }

        /// <summary>
        /// Event handler width combobox
        /// </summary>
        private void widthComboBox_TextUpdate(object sender, EventArgs e)
        {
            ComboboxInputError(widthComboBox, "width",
                _bottleParameters.Width.MinimumValue,
                _bottleParameters.Width.MaximumValue);
        }

        /// <summary>
        /// Event handler wall thickness combobox
        /// </summary>
        private void wallThicknessComboBox_TextUpdate(object sender, EventArgs e)
        {
            ComboboxInputError(wallThicknessComboBox, "wall thickness",
                _bottleParameters.WallThickness.MinimumValue,
                _bottleParameters.WallThickness.MaximumValue);
        }

        /// <summary>
        /// Event handler "Build" button
        /// </summary>
        private void buildButton_Click(object sender, EventArgs e)
        {
            
            try
            {
                _bottleParameters.CoverRadius.ParameterValue = ConvertStringToDouble(coverRadiusComboBox.Text);
                _bottleParameters.HandleBaseRadius.ParameterValue = ConvertStringToDouble(handleBaseRadiusComboBox.Text);
                _bottleParameters.HandleRadius.ParameterValue = ConvertStringToDouble(handleRadiusComboBox.Text);
                _bottleParameters.HandleLength.ParameterValue = ConvertStringToDouble(handleLengthComboBox.Text);
                _bottleParameters.Height.ParameterValue = ConvertStringToDouble(heightComboBox.Text);
                _bottleParameters.Width.ParameterValue = ConvertStringToDouble(widthComboBox.Text);
                _bottleParameters.WallThickness.ParameterValue = ConvertStringToDouble(wallThicknessComboBox.Text);

            }
            catch (Exception exception)
            {
                buildButton.Enabled = false;
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OKCancel);
                return;
            }
            try
            {
                _kompasConnector.OpenKompas();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
                return;
            }
            var builder = new Builder();
            builder.BuildBottle(_kompasConnector, _bottleParameters);

        }

        /// <summary>
        ///  Event handler button for set default parameters
        /// </summary>
        private void defaultParametersButton_Click(object sender, EventArgs e)
        {
            _bottleParameters.SetDefaultParameters();

            coverRadiusComboBox.Text =
                _bottleParameters.CoverRadius.ParameterValue.ToString();
            handleBaseRadiusComboBox.Text = 
                _bottleParameters.HandleBaseRadius.ParameterValue.ToString();
            wallThicknessComboBox.Text = 
                _bottleParameters.WallThickness.ParameterValue.ToString();
            heightComboBox.Text = 
                _bottleParameters.Height.ParameterValue.ToString();
            handleRadiusComboBox.Text = 
                _bottleParameters.HandleRadius.ParameterValue.ToString();
           handleLengthComboBox.Text = 
               _bottleParameters.HandleLength.ParameterValue.ToString();
            widthComboBox.Text = 
                _bottleParameters.Width.ParameterValue.ToString();

            handleBaseRadiusComboBox.Enabled = true;
            handleRadiusComboBox.Enabled = true;
            buildButton.Enabled = true;

            coverRadiusComboBox.BackColor = Color.White;
            handleBaseRadiusComboBox.BackColor = Color.White;
            handleRadiusComboBox.BackColor = Color.White;
            handleLengthLabel.BackColor = Color.White;
            heightComboBox.BackColor = Color.White;
            widthComboBox.BackColor = Color.White;
            wallThicknessComboBox.BackColor = Color.White;
        }

        /// <summary>
        /// Bottle shape selection handler
        /// </summary>
        private void straightFlaskRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            _bottleParameters.IsBottleStraight = 
                straightFlaskRadioButton.Checked;

            straightFlaskHandlePictureBox.Visible = straightFlaskRadioButton.Checked;
            aboveStraightFlaskHandlePictureBox.Visible = straightFlaskRadioButton.Checked;
            straightFlaskPictureBox.Visible = straightFlaskRadioButton.Checked;

            flaskHandlePictureBox.Visible = !straightFlaskRadioButton.Checked;
            aboveFlaskHandlePictureBox.Visible = !straightFlaskRadioButton.Checked;
            flaskPictureBox.Visible = !straightFlaskRadioButton.Checked;
        }
    }
}
