namespace BottleParameters
{
    /// <summary>
    /// Contains the maximum and minimum parameter values
    /// </summary>
    public class BottleBoundarySize
    {
        /// <summary>
        ///Maximum value of Cover Radius
        /// </summary>
        private double _minCoverRadius = 200.0;

        /// <summary>
        ///Minimum value of Cover Radius
        /// </summary>
        private double _maxCoverRadius = 400.0;

        /// <summary>
        ///Maximum value of Handle Base Radius
        /// </summary>
        private double _minHandleBaseRadius = 10.0;

        /// <summary>
        ///Minimum value of Handle Base Radius
        /// </summary>
        private double _maxHandleBaseRadius = 50;

        /// <summary>
        ///Maximum value of Handle Radius
        /// </summary>
        private double _minHandleRadius = 20;

        /// <summary>
        ///Minimum value Handle Radius
        /// </summary>
        private double _maxHandleRadius = 40.0;

        /// <summary>
        ///Maximum value of Handle Length
        /// </summary>
        private double _minHandleLength = 10.0;

        /// <summary>
        ///Minimum value of HandleLength
        /// </summary>
        private double _maxHandleLength = 30.0;

        /// <summary>
        ///Maximum value of Height
        /// </summary>
        private double _minHeight = 300.0;

        /// <summary>
        ///Minimum value of Height
        /// </summary>
        private double _maxHeight = 650.0;

        /// <summary>
        ///Maximum value of Width
        /// </summary>
        private double _minWidth = 200.0;

        /// <summary>
        ///Minimum value of Width
        /// </summary>
        private double _maxWidth = 400.0;

        /// <summary>
        ///Maximum value of WallThickness
        /// </summary>
        private double _minWallThickness = 7.0;

        /// <summary>
        ///Minimum value of WallThickness
        /// </summary>
        private double _maxWallThickness = 20.0;

        /// <summary>
        ///Property of Maximum Handle Base Radius
        /// </summary>
        public double MaxHandleBaseRadius
        {
            set => _maxHandleBaseRadius = value;
        }
        /// <summary>
        ///Property of Minimum Handle Radius
        /// </summary>
        public double MinHandleRadius
        {
            set => _minHandleRadius = value;
        }

        /// <summary>
        /// Finds the maximum value of a parameter depending on the type of the parameter
        /// </summary>
        /// <param name="type">Type of the parameter</param>
        /// <returns>Maximum value of a parameter depending
        /// on the type of the parameter </returns>
        public double MaximumParameterValue(ParameterTypeEnum type)
        {
            switch (type)
            {
                case ParameterTypeEnum.CoverRadius:
                {
                    return _maxCoverRadius;
                }
                case ParameterTypeEnum.HandleBaseRadius:
                {
                    return _maxHandleBaseRadius;
                }
                case ParameterTypeEnum.HandleRadius:
                {
                    return _maxHandleRadius;
                }
                case ParameterTypeEnum.HandleLength:
                {
                    return _maxHandleLength;
                }
                case ParameterTypeEnum.Height:
                {
                    return _maxHeight;
                }
                case ParameterTypeEnum.Width:
                {
                    return _maxWidth;
                }
                case ParameterTypeEnum.WallThickness:
                {
                    return _maxWallThickness;
                }
                default:
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// Finds the minimum value of a parameter depending on the type of the parameter
        /// </summary>
        /// <param name="type">Type of the parameter</param>
        /// <returns>Minimum value of a parameter depending
        /// on the type of the parameter </returns>
        public double MinimumParameterValue(ParameterTypeEnum type)
        {
            switch (type)
            {
                case ParameterTypeEnum.CoverRadius:
                {
                    return _minCoverRadius;
                }
                case ParameterTypeEnum.HandleBaseRadius:
                {
                    return _minHandleBaseRadius;
                }
                case ParameterTypeEnum.HandleRadius:
                {
                    return _minHandleRadius;
                }
                case ParameterTypeEnum.HandleLength:
                {
                    return _minHandleLength;
                }
                case ParameterTypeEnum.Height:
                {
                    return _minHeight;
                }
                case ParameterTypeEnum.Width:
                {
                    return _minWidth;
                }
                case ParameterTypeEnum.WallThickness:
                {
                    return _minWallThickness;
                }
                default:
                {
                    return -1;
                }
            }
        }
    }

}
