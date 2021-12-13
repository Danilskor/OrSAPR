using System.Collections.Generic;
using System.Linq;

namespace BottleParameters
{
    /// <summary>
    /// Contain list of the parameters
    /// </summary>
    public class Parameters
    {
        /// <summary>
        /// Parameters list
        /// </summary>
        public List<Parameter> ParametersList { get; set; } = new List<Parameter>();
        
        /// <summary>
        /// Bottle Boundary Size
        /// </summary>
        public BottleBoundarySize bottleBoundarySize = new BottleBoundarySize();

        /// <summary>
        /// Add one parameter in list
        /// </summary>
        /// <param name="value">Value of th parameter</param>
        /// <param name="parameterType">Type of the parameter</param>
        /// <param name="parameters">Lis of the parameters</param>
        private void AddParameter(double value, ParameterTypeEnum parameterType, List<Parameter> parameters)
        {
            var newParameter = new Parameter(value, parameterType, bottleBoundarySize);
            if (parameters.Any(parameter => newParameter.ParameterType == parameter.ParameterType))
            {
                return;
            }
            parameters.Add(newParameter);
        }

        public double FindParameter(ParameterTypeEnum parameterType, List<Parameter> parameters)
        {
            foreach (var parameter in parameters)
            {
                if (parameter.ParameterType == parameterType)
                {
                    return parameter.ParameterValue;
                }
            }

            return -1;
        }

        /// <summary>
        /// List of the parameters contains all parameters
        /// </summary>
        /// <param name="coverRadius">Cover radius</param>
        /// <param name="handleBaseRadius">Handle Base Radius</param>
        /// <param name="handleRadius">Handle Radius</param>
        /// <param name="handleLength">Handle Length</param>
        /// <param name="height">Height</param>
        /// <param name="width">Width</param>
        /// <param name="wallThickness">Wall Thickness</param>
        /// <param name="parametersList">Parameters List</param>
        public void AddAllParameters(double coverRadius, double handleBaseRadius, double handleRadius,
            double handleLength, double height, double width, double wallThickness, List<Parameter> parametersList)
        {
            AddParameter(coverRadius, ParameterTypeEnum.CoverRadius, parametersList);
            AddParameter(handleBaseRadius, ParameterTypeEnum.HandleBaseRadius, parametersList);
            AddParameter(handleRadius, ParameterTypeEnum.HandleRadius,  parametersList);
            AddParameter(handleLength, ParameterTypeEnum.HandleLength, parametersList);
            AddParameter(height, ParameterTypeEnum.Height, parametersList);
            AddParameter(width, ParameterTypeEnum.Width, parametersList);
            AddParameter(wallThickness, ParameterTypeEnum.WallThickness, parametersList);
        }
    }
}
