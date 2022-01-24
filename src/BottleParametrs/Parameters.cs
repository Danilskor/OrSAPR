using System.Runtime.CompilerServices;

namespace BottleParameters
{
    /// <summary>
    /// Contain list of the parameters
    /// </summary>
    public class Parameters
    {
        //TODO: RSDN
        /// <summary>
        /// Cover radius of the bottle
        /// </summary>
        private Parameter _coverRadius = new Parameter(MIN_COVER_RADIUS, MAX_COVER_RADIUS);

        /// <summary>
        /// Handle base radius of the bottle
        /// </summary>
        private Parameter _handleBaseRadius = new Parameter(MIN_HANDLE_BASE_RADIUS, NOT_SET_MAX_OR_MIN_VALUE);

        /// <summary>
        /// Handle radius of the bottle
        /// </summary>
        private Parameter _handleRadius = new Parameter(NOT_SET_MAX_OR_MIN_VALUE, NOT_SET_MAX_OR_MIN_VALUE);

        /// <summary>
        /// Handle length of the bottle
        /// </summary>
        private Parameter _handleLength = new Parameter(MIN_HANDLE_LENGTH, MAX_HANDLE_LENGTH);

        /// <summary>
        /// HEIGHT of the bottle
        /// </summary>
        private Parameter _height = new Parameter(MIN_HEIGHT, MAX_HEIGHT);

        /// <summary>
        /// WIDTH of the bottle
        /// </summary>
        private Parameter _width = new Parameter(MIN_WIDTH, MAX_WIDTH);

        /// <summary>
        /// Wall thickness of the bottle
        /// </summary>
        private Parameter _wallThickness = new Parameter(MIN_WALL_THICKNESS, MAX_WALL_THICKNESS);

        /// <summary>
        /// Is the bottle straight of the bottle
        /// </summary>
        private bool _isBottleStraight = false;

        /// <summary>
        /// Get or set cover radius
        /// </summary>
        public double CoverRadius
        {
            get => _coverRadius.ParameterValue;

            set
            {
                _coverRadius.ParameterValue = value;

                double handleBaseRadiusMax = value / 4;
                _handleBaseRadius.MaximumValue = handleBaseRadiusMax;
            }
        }

        /// <summary>
        /// Get or set handle base radius
        /// </summary>
        public double HandleBaseRadius
        {
            get => _handleBaseRadius.ParameterValue;

            set
            {
                _handleBaseRadius.ParameterValue = value;

                double handleRadiusMin = value + 20;
                double handleRadiusMax = handleRadiusMin + 30;

                _handleRadius.MinimumValue = handleRadiusMin;
                _handleRadius.MaximumValue = handleRadiusMax;
            }
        }

        /// <summary>
        /// Get or set handle radius
        /// </summary>
        public double HandleRadius
        {
            get => _handleRadius.ParameterValue;

            set => _handleRadius.ParameterValue = value;
        }

        /// <summary>
        /// Get or set  handle length
        /// </summary>
        public double HandleLength
        {
            get => _handleLength.ParameterValue;

            set => _handleLength.ParameterValue = value;
        }

        /// <summary>
        /// Get or set HEIGHT
        /// </summary>
        public double Height
        {
            get => _height.ParameterValue;

            set => _height.ParameterValue = value;
        }

        /// <summary>
        /// Get or set WIDTH
        /// </summary>
        public double Width
        {
            get => _width.ParameterValue;

            set => _width.ParameterValue = value;
        }

        /// <summary>
        /// Get or set wall thickness
        /// </summary>
        public double WallThickness
        {
            get => _wallThickness.ParameterValue;

            set => _wallThickness.ParameterValue = value;
        }

        /// <summary>
        /// Get or set is bottle straight
        /// </summary>MIN_CoverRadius
        public bool IsBottleStraight
        {
            get => _isBottleStraight;

            set => _isBottleStraight = value;
        }
        
        /// <summary>
        ///Minimum value of Cover Radius
        /// </summary>
        public const double MIN_COVER_RADIUS = 200.0;

        /// <summary>
        ///Maximum value of Cover Radius
        /// </summary>
        public const double MAX_COVER_RADIUS = 400.0;

        /// <summary>
        ///Minimum value of Handle Base Radius
        /// </summary>
        public const double MIN_HANDLE_BASE_RADIUS = 10.0;

        /// <summary>
        ///Minimum value of Handle Length
        /// </summary>
        public const double MIN_HANDLE_LENGTH = 10.0;

        /// <summary>
        ///Maximum value of HANDLE_LENGTH
        /// </summary>
        public const double MAX_HANDLE_LENGTH = 30.0;

        /// <summary>
        ///Minimum value of HEIGHT
        /// </summary>
        public const double MIN_HEIGHT = 300.0;

        /// <summary>
        ///Maximum value of HEIGHT
        /// </summary>
        public const double MAX_HEIGHT = 650.0;

        /// <summary>
        ///Minimum value of WIDTH
        /// </summary>
        public const double MIN_WIDTH = 200.0;

        /// <summary>
        ///Maximum value of WIDTH
        /// </summary>
        public const double MAX_WIDTH = 400.0;

        /// <summary>
        ///Minimum value of WALL_THICKNESS
        /// </summary>
        public const double MIN_WALL_THICKNESS = 7.0;

        /// <summary>
        ///Maximum value of WallThickness
        /// </summary>
        public const double MAX_WALL_THICKNESS = 20.0;

        /// <summary>
        /// Not set max or min value
        /// </summary>
        public const double NOT_SET_MAX_OR_MIN_VALUE = -1;

        //TODO: Несоответствие XML-комментария сигнатуре метода 
        /// <summary>
        /// Set default parameters
        /// </summary>
        /// <param name="parametersList">Parameters List</param>
        public void SetDefaultParameters()
        {
            this.CoverRadius = 200;
            this.HandleBaseRadius = 10;
            this.HandleRadius = 30;
            this.HandleLength = 10;
            this.Height = 300;
            this.Width = 200;
            this.WallThickness = 7;
        }
    }
}
