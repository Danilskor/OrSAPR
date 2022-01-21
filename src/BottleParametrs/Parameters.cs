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
        /// 
        /// </summary>
        private bool _isBottleStraight = false;
        /// <summary>
        /// 
        /// </summary>
        private Parameter _coverRadius = new Parameter(minCoverRadius, maxCoverRadius);

        /// <summary>
        /// 
        /// </summary>
        private Parameter _handleBaseRadius = new Parameter(minHandleBaseRadius, maxHandleBaseRadius);

        /// <summary>
        /// 
        /// </summary>
        private Parameter _handleRadius = new Parameter(minHandleRadius, maxHandleRadius);

        /// <summary>
        /// 
        /// </summary>
        private Parameter _handleLength = new Parameter(minHandleLength, maxHandleLength);

        /// <summary>
        /// 
        /// </summary>
        private Parameter _height = new Parameter(minHeight, maxHeight);

        /// <summary>
        /// 
        /// </summary>
        private Parameter _width = new Parameter(minWidth, maxWidth);

        /// <summary>
        /// 
        /// </summary>
        private Parameter _wallThickness = new Parameter(minWallThickness, maxWallThickness);

        //TODO: Переписать класс, используя инкапсуляцию на список параметров.
        /// <summary>
        ///Minimum value of Cover Radius
        /// </summary>
        public const double minCoverRadius = 200.0;

        /// <summary>
        ///Maximum value of Cover Radius
        /// </summary>
        public const double maxCoverRadius = 400.0;

        /// <summary>
        ///Minimum value of Handle Base Radius
        /// </summary>
        public const double minHandleBaseRadius = 10.0;

        /// <summary>
        ///Maximum value of Handle Base Radius
        /// </summary>
        public const double maxHandleBaseRadius = 50;

        /// <summary>
        ///Minimum value of Handle Radius
        /// </summary>
        public const double minHandleRadius = 20;

        /// <summary>
        ///Maximum value Handle Radius
        /// </summary>
        public const double maxHandleRadius = 40.0;

        /// <summary>
        ///Minimum value of Handle Length
        /// </summary>
        public const double minHandleLength = 10.0;

        /// <summary>
        ///Maximum value of HandleLength
        /// </summary>
        public const double maxHandleLength = 30.0;

        /// <summary>
        ///Minimum value of Height
        /// </summary>
        public const double minHeight = 300.0;

        /// <summary>
        ///Maximum value of Height
        /// </summary>
        public const double maxHeight = 650.0;

        /// <summary>
        ///Minimum value of Width
        /// </summary>
        public const double minWidth = 200.0;

        /// <summary>
        ///Maximum value of Width
        /// </summary>
        public const double maxWidth = 400.0;

        /// <summary>
        ///Minimum value of WallThickness
        /// </summary>
        public const double minWallThickness = 7.0;

        /// <summary>
        ///Maximum value of WallThickness
        /// </summary>
        public const double maxWallThickness = 20.0;

        public Parameter CoverRadius
        {
            get => _coverRadius;

            set
            {
                _coverRadius.ParameterValue = value.ParameterValue;

                double handleBaseRadiusMax = value.ParameterValue / 4;
                _handleBaseRadius.MaximumValue = handleBaseRadiusMax;
            }
        }

        public Parameter HandleBaseRadius
        {
            get => _handleBaseRadius;

            set
            {
                _handleBaseRadius = value;

                double handleRadiusMin = value.ParameterValue + 20;
                double handleRadiusMax = handleRadiusMin + 30;

                _handleRadius.MinimumValue = handleRadiusMin;
                _handleRadius.MaximumValue = handleRadiusMax;
            }
        }

        public Parameter HandleRadius
        {
            get => _handleRadius;

            set => _handleRadius = value;
        }

        public Parameter HandleLength
        {
            get => _handleLength;

            set => _handleLength = value;
        }

        public Parameter Height
        {
            get => _height;

            set => _height = value;
        }

        public Parameter Width
        {
            get => _width;

            set => _width = value;
        }

        public Parameter WallThickness
        {
            get => _wallThickness;

            set => _wallThickness = value;
        }

        public bool IsBottleStraight
        {
            get => _isBottleStraight;

            set => _isBottleStraight = value;
        }

        /// <summary>
        /// Set default parameters
        /// </summary>
        /// <param name="parametersList">Parameters List</param>
        public void SetDefaultParameters()
        {
            this.CoverRadius.ParameterValue = 200;
            this.HandleBaseRadius.ParameterValue = 10;
            this.HandleRadius.ParameterValue = 30;
            this.HandleLength.ParameterValue = 10;
            this.Height.ParameterValue = 300;
            this.Width.ParameterValue = 200;
            this.WallThickness.ParameterValue = 7;
        }
    }
}
