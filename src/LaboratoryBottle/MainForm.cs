using BottleBuilder;
using BottleParameters;
using KompasConnector;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;
using static Validator.Validator;
using System.ComponentModel;
using System.Globalization;

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
        private Konnector _kompasConnector = new Konnector();

        /// <summary>
        /// Dictionary of Suggested Pairs (ComboBoxes, parameter name)
        /// </summary>
        private Dictionary<ComboBox, ParameterType> _comboboxDictionary;

        /*/// <summary>
        /// An array indicating that the values ​​in all comboBoxes are correct
        /// </summary>
        private Dictionary<ParameterType, > _allTextBoxesValueCorrect;*/

         /// <summary>
         /// Main form constructor
         /// </summary>
        public MainForm()
        {
            InitializeComponent();

            _comboboxDictionary = new Dictionary<ComboBox, ParameterType>
            {
                {coverRadiusComboBox, ParameterType.CoverRadius},
                {handleBaseRadiusComboBox, ParameterType.HandleBaseRadius},
                {handleRadiusComboBox, ParameterType.HandleRadius},
                {handleLengthComboBox, ParameterType.HandleLength},
                {heightComboBox, ParameterType.Height},
                {widthComboBox, ParameterType.Width},
                {wallThicknessComboBox, ParameterType.WallThickness},
            };
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
        /// Event handler "Build" button
        /// </summary>
        private void BuildButton_Click(object sender, EventArgs e)
        {
            try
            {
                _bottleParameters.CoverRadius = ConvertStringToDouble(coverRadiusComboBox.Text);
                _bottleParameters.HandleBaseRadius = ConvertStringToDouble(handleBaseRadiusComboBox.Text);
                _bottleParameters.HandleRadius = ConvertStringToDouble(handleRadiusComboBox.Text);
                _bottleParameters.HandleLength = ConvertStringToDouble(handleLengthComboBox.Text);
                _bottleParameters.Height = ConvertStringToDouble(heightComboBox.Text);
                _bottleParameters.Width = ConvertStringToDouble(widthComboBox.Text);
                _bottleParameters.WallThickness = ConvertStringToDouble(wallThicknessComboBox.Text);

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

            _bottleBuilder = new Builder();
            _bottleBuilder.BuildBottle(_kompasConnector, _bottleParameters);

        }


        /// <summary>
        ///  Event handler button for set default parameters
        /// </summary>
        private void DefaultParametersButton_Click(object sender, EventArgs e)
        {
            _bottleParameters.SetDefaultParameters();

            coverRadiusComboBox.Text =
                _bottleParameters.CoverRadius.ToString(CultureInfo.CurrentCulture);
            handleBaseRadiusComboBox.Text = 
                _bottleParameters.HandleBaseRadius.ToString();
            wallThicknessComboBox.Text = 
                _bottleParameters.WallThickness.ToString();
            heightComboBox.Text = 
                _bottleParameters.Height.ToString();
            handleRadiusComboBox.Text = 
                _bottleParameters.HandleRadius.ToString();
           handleLengthComboBox.Text = 
               _bottleParameters.HandleLength.ToString();
            widthComboBox.Text = 
                _bottleParameters.Width.ToString();

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

        //TODO: RSDN
        /// <summary>
        /// Bottle shape selection event handler
        /// </summary>
        private void StraightFlaskRadioButton_CheckedChanged(object sender, EventArgs e)
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

        //TODO: RSDN
        //TODO: дубль
        /// <summary>
        /// ComboBox validation method
        /// </summary>
        /// <param name="sender">ComboBox</param>
        private void Combobox_Validating(object sender, EventArgs e)
        {
            if (!(sender is ComboBox comboBox)) return;
            try
            {
                _comboboxDictionary.TryGetValue(comboBox,
                    out var parameterInComboboxType);
                _bottleParameters.SetParameterValueByType(double.Parse(comboBox.Text),
                    parameterInComboboxType);

                if (comboBox == coverRadiusComboBox)
                {
                    var handleBaseRadiusMaximumValue =
                        _bottleParameters.GetMaximumValue(ParameterType.HandleBaseRadius);
                    handleBaseRadiusComboBox.Enabled = true;
                    handleBaseRadiusLabel.Text = $"(10-" +
                                                 $"{handleBaseRadiusMaximumValue}) мм";
                }
                else if(comboBox == handleBaseRadiusComboBox)
                {
                    handleRadiusComboBox.Enabled = true;
                    var handleRadiusMinimumValue =
                        _bottleParameters.GetMinimumValue(ParameterType.HandleRadius);
                    var handleRadiusMaximumValue =
                        _bottleParameters.GetMaximumValue(ParameterType.HandleRadius);
                    handleRadiusLabel.Text = $"({handleRadiusMinimumValue}" +
                                             $"-{handleRadiusMaximumValue}) мм";
                }
                comboBox.BackColor = Color.White;
            }
            catch (Exception)
            {
                if (comboBox == coverRadiusComboBox)
                {
                    handleBaseRadiusComboBox.Enabled = false;
                }
                if(comboBox == handleBaseRadiusComboBox)
                {
                    handleRadiusComboBox.Enabled = false;
                }
                comboBox.BackColor = Color.Salmon;
            }
            SwitchingBuildButton();
        }


        /// <summary>
        /// Enabled BuildButton if values in all ComboBoxes are correct
        /// </summary>
        /// <returns></returns>
        private void SwitchingBuildButton()
        {
            try
            {
                foreach (var comboBoxParameterTypePair in _comboboxDictionary)
                {
                    double.TryParse(comboBoxParameterTypePair.Key.Text, 
                        out double parameterValue);
                    _bottleParameters.SetParameterValueByType(
                        parameterValue, comboBoxParameterTypePair.Value);
                }

                buildButton.Enabled = true;
            }
            catch (Exception)
            {
                buildButton.Enabled = false;
            }
        }
        
    }
}
