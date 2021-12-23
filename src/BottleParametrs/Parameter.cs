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

        private double _minimumValue;

        private double _maximumValue;

        /// <summary>
        /// Property of the parameter value
        /// </summary>
        public double ParameterValue
        {
            get => _parameterValue;
            set
            {
                if (IsNumberInRange(value, _minimumValue, _maximumValue))
                {
                    _parameterValue = value;
                }
                else
                {
                    {
                        throw new ArgumentException(
                            $"{ParameterType} should be more then {_minimumValue} " +
                            $"and less then {_maximumValue} ");
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
        /// <param name="parameterValue"> Value of the parameter</param>
        /// <param name="parameterType">Type of the parameter</param>
        public Parameter(double parameterValue, ParameterTypeEnum parameterType, 
           BottleBoundarySize bottleBoundarySize)
        {
            _minimumValue = bottleBoundarySize.MinimumParameterValue(parameterType);
            _maximumValue = bottleBoundarySize.MaximumParameterValue(parameterType);
            ParameterType = parameterType;
            ParameterValue = parameterValue;
        }

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
