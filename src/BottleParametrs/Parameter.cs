using System;

namespace BottleParameters
{   
    /// <summary>
    /// Contains property of the parameter
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// Parameter value
        /// </summary>
        private double _parameterValue;

        /// <summary>
        /// Property of the minimum parameter value
        /// </summary>
        public double MinimumValue { get; set; }

        /// <summary>
        /// Property of the maximum parameter value
        /// </summary>
        public double MaximumValue { get; set; }

        /// <summary>
        /// Property of the parameter value
        /// </summary>
        public double ParameterValue
        {
            get => _parameterValue;
            set
            {
                if (IsNumberInRange(value, MinimumValue, MaximumValue))
                {
                    _parameterValue = value;
                }
                else
                {
                    {
                        throw new ArgumentException(
                            $"{ParameterType} should be more then {MinimumValue} " +
                            $"and less then {MaximumValue} ");
                    }
                }
            }
        }
        
        /// <summary>
        /// Property of the parameter type
        /// </summary>
        public ParameterTypeEnum ParameterType { get; set; }

        /// <summary>
        /// Constructor of the parameter
        /// </summary>
        /// <param name="minimumValue"> Value of the parameter</param>
        /// <param name="maximumValue">Type of the parameter</param>
        public Parameter(double minimumValue, double maximumValue)
        {
            MinimumValue = minimumValue;
            MaximumValue = maximumValue;
        }

        public Parameter() {}

        /// <summary>
        /// Сhecks if the parameter value is included in the interval 
        /// </summary>
        /// <param name="value"> Value of the parameter</param>
        /// <param name="min">Minimal value</param>
        /// <param name="max">Maximal value</param>
        /// <returns>True if value include and false if not include</returns>
        private bool IsNumberInRange(double value, double min, double max)
        {
            return value >= min && value <= max;
        }

        
    }
}
