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
        /// Cover radius of the bottle
        /// </summary>
        private Parameter _coverRadius = new Parameter(minCoverRadius, maxCoverRadius);

        /// <summary>
        /// Handle base radius of the bottle
        /// </summary>
        private Parameter _handleBaseRadius = new Parameter(minHandleBaseRadius, maxHandleBaseRadius);

        /// <summary>
        /// Handle radius of the bottle
        /// </summary>
        private Parameter _handleRadius = new Parameter(minHandleRadius, maxHandleRadius);

        /// <summary>
        /// Handle length of the bottle
        /// </summary>
        private Parameter _handleLength = new Parameter(minHandleLength, maxHandleLength);

        /// <summary>
        /// Height of the bottle
        /// </summary>
        private Parameter _height = new Parameter(minHeight, maxHeight);

        /// <summary>
        /// Width of the bottle
        /// </summary>
        private Parameter _width = new Parameter(minWidth, maxWidth);

        /// <summary>
        /// Wall thickness of the bottle
        /// </summary>
        private Parameter _wallThickness = new Parameter(minWallThickness, maxWallThickness);

        /// <summary>
        /// Is the bottle straight of the bottle
        /// </summary>
        private bool _isBottleStraight = false;

        /// <summary>
        /// Get or set cover radius
        /// </summary>
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

        /// <summary>
        /// Get or set handle base radius
        /// </summary>
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

        /// <summary>
        /// Get or set handle radius
        /// </summary>
        public Parameter HandleRadius
        {
            get => _handleRadius;

            set => _handleRadius = value;
        }

        /// <summary>
        /// Get or set  handle length
        /// </summary>
        public Parameter HandleLength
        {
            get => _handleLength;

            set => _handleLength = value;
        }

        /// <summary>
        /// Get or set height
        /// </summary>
        public Parameter Height
        {
            get => _height;

            set => _height = value;
        }

        /// <summary>
        /// Get or set width
        /// </summary>
        public Parameter Width
        {
            get => _width;

            set => _width = value;
        }

        /// <summary>
        /// Get or set wall thickness
        /// </summary>
        public Parameter WallThickness
        {
            get => _wallThickness;

            set => _wallThickness = value;
        }

        /// <summary>
        /// Get or set is bottle straight
        /// </summary>
        public bool IsBottleStraight
        {
            get => _isBottleStraight;

            set => _isBottleStraight = value;
        }

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
