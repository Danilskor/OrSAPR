using System.Reflection;
using NUnit.Framework;
using BottleParameters;

namespace UnitTests
{
    /// <summary>
    /// Class for testing parameters
    /// </summary>
    [TestFixture]
    public class ParametersTest
    {
        [TestCase(TestName = "Positive default parameters set")]
        public void Parameters_SetDefaultValue()
        {
            //Set
            var expected = new Parameters();
            expected.SetDefaultParameters();

            //Act
            var actual = SetDefaultValues();

            //Assert
            foreach (PropertyInfo expectedProperty in 
                expected.GetType().GetProperties())
            {
                var propertyName = expectedProperty.Name;
                
                var actualValue = actual.GetType().
                    GetProperty(propertyName).GetValue(actual);
                var expectedValue = expectedProperty.GetValue(expected);
                Assert.AreEqual(expectedValue, actualValue);
            }
        }

        //TODO: RSDN
        [TestCase(201, ParameterType.CoverRadius, 
            TestName = "Positive cover radius get")]
        [TestCase(31, ParameterType.HandleBaseRadius, 
            TestName = "Positive Handle Base Radius get")]
        [TestCase(31, ParameterType.HandleRadius, 
            TestName = "Positive Handle Radius get")]
        [TestCase(21, ParameterType.HandleLength, 
            TestName = "Positive Handle Length get")]
        [TestCase(351, ParameterType.Height, 
            TestName = "Positive Height get")]
        [TestCase(201, ParameterType.Width, 
            TestName = "Positive Width get")]
        [TestCase(10, ParameterType.WallThickness, 
            TestName = "Positive Wall Thickness get")]
        public void Parameters_GetCorrectValue(double testParameterValue, 
             ParameterType parameterType)
        {
            
            var actual = SetDefaultValues();
            
            actual.SetParameterValueByType(testParameterValue, parameterType);
            //TODO: RSDN
            //TODO: дубль
            var actualPropertyObject = GetPropertyValue(actual, parameterType);

            Assert.AreEqual(testParameterValue, actualPropertyObject);
        }

        //TODO: RSDN
        [TestCase(200, ParameterType.CoverRadius, 
            TestName = "Positive cover radius set")]
        [TestCase(50, ParameterType.HandleBaseRadius, 
            TestName = "Positive Handle Base Radius set")]
        [TestCase(30, ParameterType.HandleRadius, 
            TestName = "Positive Handle Radius set")]
        [TestCase(20, ParameterType.HandleLength, 
            TestName = "Positive Handle Length set")]
        [TestCase(350, ParameterType.Height, 
            TestName = "Positive Height set")]
        [TestCase(300, ParameterType.Width, 
            TestName = "Positive Width set")]
        [TestCase(20, ParameterType.WallThickness, 
            TestName = "Positive Wall Thickness set")]
        [TestCase(true, ParameterType.IsBottleStraight, 
            TestName = "Positive Is Bottle Straight set")]
        public void Parameters_SetCorrectParameters(dynamic value, 
            ParameterType parameterType)
        {
            var testParameters = SetDefaultValues();

            var testValue = value;

            testParameters.SetParameterValueByType(testValue, parameterType);

            //TODO: дубль
            var actualValue = GetPropertyValue(testParameters, parameterType);
            Assert.AreEqual(actualValue, testValue);
        }

        //TODO: RSDN
        [TestCase(ParameterType.CoverRadius, 
            Parameters.MIN_COVER_RADIUS, 
            TestName = "Positive Minimum Value cover radius Get")]
        [TestCase(ParameterType.HandleBaseRadius, 
            Parameters.MIN_HANDLE_BASE_RADIUS, 
            TestName = "Positive Minimum Value Handle Radius Get")]
        [TestCase(ParameterType.HandleRadius, 
            Parameters.NOT_SET_MAX_OR_MIN_VALUE, 
            TestName = "Positive Minimum Value Handle Length set Get")]
        [TestCase(ParameterType.HandleLength, 
            Parameters.MIN_HANDLE_LENGTH, 
            TestName = "Positive Minimum Value Height Get")]
        [TestCase(ParameterType.Height, 
            Parameters.MIN_HEIGHT, 
            TestName = "Positive Minimum Value Width Get")]
        [TestCase(ParameterType.Width, 
            Parameters.MIN_WIDTH, 
            TestName = "Positive Minimum Value Wall Thickness Get")]
        [TestCase(ParameterType.WallThickness, 
            Parameters.MIN_WALL_THICKNESS, 
            TestName = "Positive Minimum Value Parameters Get")]
        [TestCase(ParameterType.IsBottleStraight, 
            Parameters.NOT_SET_MAX_OR_MIN_VALUE, 
            TestName = "Positive Minimum Value Is Bottle Straight Get")]
        public void Parameters_GetCorrectMinimumValues(ParameterType parameterType, 
            double minValue)
        {
            var testParameters = new Parameters();
            double expectedMinimumValue = -1;
            double actualMinimumValue = 0;
            expectedMinimumValue = minValue;
            actualMinimumValue = testParameters.GetMinimumValue(parameterType);

            Assert.AreEqual(expectedMinimumValue, actualMinimumValue);
        }

        //TODO: RSDN
        [TestCase(ParameterType.CoverRadius, 
            Parameters.MAX_COVER_RADIUS, 
            TestName = "Positive Maximum Value cover radius Get")]
        [TestCase(ParameterType.HandleBaseRadius, 
            Parameters.NOT_SET_MAX_OR_MIN_VALUE, 
            TestName = "Positive Maximum Value Handle Radius Get")]
        [TestCase(ParameterType.HandleRadius, 
            Parameters.NOT_SET_MAX_OR_MIN_VALUE, 
            TestName = "Positive Maximum Value Handle Length set Get")]
        [TestCase(ParameterType.HandleLength, 
            Parameters.MAX_HANDLE_LENGTH, 
            TestName = "Positive Maximum Value Height Get")]
        [TestCase(ParameterType.Height, 
            Parameters.MAX_HEIGHT, 
            TestName = "Positive Maximum Value Width Get")]
        [TestCase(ParameterType.Width, 
            Parameters.MAX_WIDTH, 
            TestName = "Positive Maximum Value Wall Thickness Get")]
        [TestCase(ParameterType.WallThickness, 
            Parameters.MAX_WALL_THICKNESS, 
            TestName = "Positive Maximum Value Parameters Get")]
        [TestCase(ParameterType.IsBottleStraight, 
            Parameters.NOT_SET_MAX_OR_MIN_VALUE, 
            TestName = "Positive Maximum Value Is Bottle Straight Get")]
        public void Parameters_GetCorrectMaximumValues(ParameterType parameterType, 
            double maxValue)
        {
            var testParameters = new Parameters();
            double expectedMaximumValue = -1;
            double actualMaximumValue = 0;
            expectedMaximumValue = maxValue;
            actualMaximumValue = testParameters.GetMaximumValue(parameterType);
                 
            Assert.AreEqual(expectedMaximumValue, actualMaximumValue);
        }
        
        /// <summary>
        /// Set default parameters values
        /// </summary>
        private Parameters SetDefaultValues()
        {
            return new Parameters
            {
                CoverRadius = 200,
                HandleBaseRadius = 10,
                HandleLength = 10,
                HandleRadius = 30,
                Height = 300,
                Width = 200,
                WallThickness = 7
            };
        }

        private dynamic GetPropertyValue(Parameters parameters, 
            ParameterType parameterType)
        {
            return  parameters.GetType().
                GetProperty(parameterType.ToString()).
                GetValue(parameters);
        }
    }
}
