using System;
using System.Drawing;
using System.Reflection;
using BottleBuilder;
using BottleParameters;
using System.Windows.Forms;
using KompasConnector;
using static System.Reflection.Pointer;
using static Validator.Validator;

namespace LaboratoryBottle
{
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
        /// Bottle Boundary Size
        /// </summary>
        private BottleBoundarySize bottleBoundarySize;

        public Konnector _kompasConnector = new Konnector();

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
                AssertNumberOnRange(value, 0, max, fieldName);
                
                
                if (CalculateValueDigitsNumber(value)
                    == CalculateValueDigitsNumber(min) &&
                    value < min)
                {
                    comboBox.BackColor = Color.Salmon;
                    buildButton.Enabled = false;
                }
                else
                {
                    comboBox.BackColor = Color.White;
                    buildButton.Enabled = true;
                }
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

        private void MainForm_Load(object sender, EventArgs e)
        {
            bottleBoundarySize = _bottleParameters.bottleBoundarySize;
        }

        private void coverRadiusComboBox_TextUpdate(object sender, EventArgs e)
        {

            ComboboxInputError(coverRadiusComboBox,"cover radius", 
                bottleBoundarySize.MinimumParameterValue(ParameterTypeEnum.CoverRadius),
                bottleBoundarySize.MaximumParameterValue(ParameterTypeEnum.CoverRadius));

            var value = ConvertStringToDouble(coverRadiusComboBox.Text);

            if (value >= bottleBoundarySize.MinimumParameterValue(ParameterTypeEnum.CoverRadius) && 
                value <= bottleBoundarySize.MaximumParameterValue(ParameterTypeEnum.CoverRadius))
            {
                double handleBaseRadiusMax = value / 4;
                handleBaseRadiusLabel.Text = $"(10-{handleBaseRadiusMax}) мм";
                bottleBoundarySize.MaxHandleBaseRadius = handleBaseRadiusMax;
                handleBaseRadiusComboBox.Enabled = true;
            }
            else
            {
                handleBaseRadiusComboBox.Enabled = false;
            }
        }

        private void handleBaseRadiusComboBox_TextUpdate(object sender, EventArgs e)
        {
            ComboboxInputError(handleBaseRadiusComboBox, "handle radius", 
                bottleBoundarySize.MinimumParameterValue(ParameterTypeEnum.HandleBaseRadius),
                bottleBoundarySize.MaximumParameterValue(ParameterTypeEnum.HandleBaseRadius));

            var value = ConvertStringToDouble(handleBaseRadiusComboBox.Text);
            if (value >= bottleBoundarySize.MinimumParameterValue(ParameterTypeEnum.HandleBaseRadius) &&
                value <= bottleBoundarySize.MaximumParameterValue(ParameterTypeEnum.HandleBaseRadius))
            {
                double handleRadiusMin = value;
                handleBaseRadiusLabel.Text = $"({handleRadiusMin}-40) мм";
                bottleBoundarySize.MinHandleRadius = handleRadiusMin;
                handleRadiusComboBox.Enabled = true;
            }
            else
            {
                handleRadiusComboBox.Enabled = false;
            }
        }

        private void handleRadiusComboBox_TextUpdate(object sender, EventArgs e)
        {
            ComboboxInputError(handleRadiusComboBox, "handle radius", 
                bottleBoundarySize.MinimumParameterValue(ParameterTypeEnum.HandleRadius),
                bottleBoundarySize.MaximumParameterValue(ParameterTypeEnum.HandleRadius));
        }

        private void handleLengthComboBox_TextUpdate(object sender, EventArgs e)
        {

            double.TryParse(handleLengthComboBox.Text, out double numberResult);

            ComboboxInputError(handleLengthComboBox, "handle length",
                bottleBoundarySize.MinimumParameterValue(ParameterTypeEnum.HandleLength),
                bottleBoundarySize.MaximumParameterValue(ParameterTypeEnum.HandleLength));
        }

        private void heightComboBox_TextUpdate(object sender, EventArgs e)
        {
            ComboboxInputError(heightComboBox, "height",
                bottleBoundarySize.MinimumParameterValue(ParameterTypeEnum.Height),
                bottleBoundarySize.MaximumParameterValue(ParameterTypeEnum.Height));
        }

        private void widthComboBox_TextUpdate(object sender, EventArgs e)
        {
            ComboboxInputError(widthComboBox, "width",
                bottleBoundarySize.MinimumParameterValue(ParameterTypeEnum.Width),
                bottleBoundarySize.MaximumParameterValue(ParameterTypeEnum.Width));
        }

        private void wallThicknessComboBox_TextUpdate(object sender, EventArgs e)
        {
            ComboboxInputError(wallThicknessComboBox, "handle radius",
                bottleBoundarySize.MinimumParameterValue(ParameterTypeEnum.WallThickness),
                bottleBoundarySize.MaximumParameterValue(ParameterTypeEnum.WallThickness));
        }

        private void buildButton_Click(object sender, EventArgs e)
        {
            _bottleParameters.bottleBoundarySize = bottleBoundarySize;
            try
            {
                _kompasConnector.OpenKompas();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }

            try
            {
                _bottleParameters = new Parameters();

                double coverRadius = ConvertStringToDouble(coverRadiusComboBox.Text);
                double handleBaseRadius = ConvertStringToDouble(handleBaseRadiusComboBox.Text);
                double handleRadius = ConvertStringToDouble(handleRadiusComboBox.Text);
                double handleLength = ConvertStringToDouble(handleLengthComboBox.Text);
                double height = ConvertStringToDouble(heightComboBox.Text);
                double width = ConvertStringToDouble(widthComboBox.Text);
                double wallThickness = ConvertStringToDouble(wallThicknessComboBox.Text);

                _bottleParameters.AddAllParameters(coverRadius,
                    handleBaseRadius, handleRadius, handleLength,
                    height, width, wallThickness, _bottleParameters.ParametersList);
            }
            catch (Exception exception)
            {
                buildButton.Enabled = false;
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OKCancel);
            }
            var builder = new Builder();
            builder.BuildBottle(_kompasConnector, _bottleParameters);

        }
    }
}
