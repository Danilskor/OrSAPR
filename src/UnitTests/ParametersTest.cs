using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using BottleParameters;
using NUnit.Framework.Internal;

namespace UnitTests
{
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
            var actual = new Parameters();
            actual = SetDefaultValues(actual);

            //TODO: дубль
            //Assert
            foreach (PropertyInfo expectedProperty in expected.GetType().GetProperties())
            {
                var propertyName = expectedProperty.Name;
                
                var actualValue = actual.GetType().GetProperty(propertyName).GetValue(actual);
                var expectedValue = expectedProperty.GetValue(expected);
                Assert.AreEqual(expectedValue, actualValue);
            }
        }

        //TODO: RSDN
        [TestCase(201, ParameterType.CoverRadius, TestName = "Positive cover radius get")]
        [TestCase(31, ParameterType.HandleBaseRadius, TestName = "Positive Handle Base Radius get")]
        [TestCase(31, ParameterType.HandleRadius, TestName = "Positive Handle Radius get")]
        [TestCase(21, ParameterType.HandleLength, TestName = "Positive Handle Length get")]
        [TestCase(351, ParameterType.Height, TestName = "Positive Height get")]
        [TestCase(201, ParameterType.Width, TestName = "Positive Width get")]
        [TestCase(10, ParameterType.WallThickness, TestName = "Positive Wall Thickness get")]
        public void Parameters_GetCorrectValue(double testParameterValue, 
             ParameterType parameterType)
        {
            
            var actual = new Parameters();
            actual = SetDefaultValues(actual);

            //TODO: дубль
            actual.SetParameterValueByType(testParameterValue, parameterType);
            var actualPropertyObject = actual.GetType().GetProperty(parameterType.ToString()).GetValue(actual);
            
            Assert.AreEqual(testParameterValue, actualPropertyObject);
        }

        //TODO: дубль
        [TestCase(200, ParameterType.CoverRadius, TestName = "Positive cover radius set")]
        [TestCase(50, ParameterType.HandleBaseRadius, TestName = "Positive Handle Base Radius set")]
        [TestCase(30, ParameterType.HandleRadius, TestName = "Positive Handle Radius set")]
        [TestCase(20, ParameterType.HandleLength, TestName = "Positive Handle Length set")]
        [TestCase(350, ParameterType.Height, TestName = "Positive Height set")]
        [TestCase(300, ParameterType.Width, TestName = "Positive Width set")]
        [TestCase(20, ParameterType.WallThickness, TestName = "Positive Wall Thickness set")]
        [TestCase(true, ParameterType.IsBottleStraight, TestName = "Positive Is Bottle Straight set")]
        public void Parameters_SetCorrectParameters(dynamic value, ParameterType parameterType)
        {
            var testParameters = new Parameters();
            testParameters = SetDefaultValues(testParameters);

            var testValue = value;

            testParameters.SetParameterValueByType(testValue, parameterType);

            //TODO: дубль
            var actualValue = testParameters.GetType().
                GetProperty(parameterType.ToString()).
                GetValue(testParameters);
            Assert.AreEqual(actualValue, testValue);
        }

        [TestCase(ParameterType.CoverRadius, TestName = "Positive Minimum Value cover radius Get")]
        [TestCase(ParameterType.HandleBaseRadius, TestName = "Positive Minimum Value Handle Radius Get")]
        [TestCase(ParameterType.HandleRadius, TestName = "Positive Minimum Value Handle Length set Get")]
        [TestCase(ParameterType.HandleLength, TestName = "Positive Minimum Value Height Get")]
        [TestCase(ParameterType.Height, TestName = "Positive Minimum Value Width Get")]
        [TestCase(ParameterType.Width, TestName = "Positive Minimum Value Wall Thickness Get")]
        [TestCase(ParameterType.WallThickness, TestName = "Positive Minimum Value Parameters Get")]
        [TestCase(ParameterType.IsBottleStraight, TestName = "Positive Minimum Value Is Bottle Straight Get")]
        public void Parameters_GetCorrectMinimumValues(ParameterType parameterType)
        {
            var testParameters = new Parameters();
            double expectedMinimumValue = -1;
            double actualMinimumValue = 0;
            switch (parameterType)
            {
                //TODO: дубль
                case ParameterType.CoverRadius:
                {
                    expectedMinimumValue = Parameters.MIN_COVER_RADIUS;
                    actualMinimumValue = testParameters.GetMinimumValue(parameterType);
                    break;
                }
                case ParameterType.HandleBaseRadius:
                {
                    expectedMinimumValue = Parameters.MIN_HANDLE_BASE_RADIUS;
                    actualMinimumValue = testParameters.GetMinimumValue(parameterType);
                    break;
                }
                case ParameterType.HandleRadius:
                {
                    expectedMinimumValue = Parameters.NOT_SET_MAX_OR_MIN_VALUE;
                    actualMinimumValue = testParameters.GetMinimumValue(parameterType);
                    break;
                }
                case ParameterType.HandleLength:
                {
                    expectedMinimumValue = Parameters.MIN_HANDLE_LENGTH;
                    actualMinimumValue = testParameters.GetMinimumValue(parameterType);
                    break;
                }
                case ParameterType.Height:
                {
                    expectedMinimumValue = Parameters.MIN_HEIGHT;
                    actualMinimumValue = testParameters.GetMinimumValue(parameterType);
                    break;
                }
                case ParameterType.Width:
                {
                    expectedMinimumValue = Parameters.MIN_WIDTH;
                    actualMinimumValue = testParameters.GetMinimumValue(parameterType);
                    break;
                }
                case ParameterType.WallThickness:
                {
                    expectedMinimumValue = Parameters.MIN_WALL_THICKNESS;
                    actualMinimumValue = testParameters.GetMinimumValue(parameterType);
                    break;
                }
                case ParameterType.IsBottleStraight:
                {
                    expectedMinimumValue = -1;
                    actualMinimumValue = testParameters.GetMinimumValue(parameterType);
                    break;
                }
            }
            Assert.AreEqual(expectedMinimumValue, actualMinimumValue);
        }

        [TestCase(ParameterType.CoverRadius, TestName = "Positive Maximum Value cover radius Get")]
        [TestCase(ParameterType.HandleBaseRadius, TestName = "Positive Maximum Value Handle Radius Get")]
        [TestCase(ParameterType.HandleRadius, TestName = "Positive Maximum Value Handle Length set Get")]
        [TestCase(ParameterType.HandleLength, TestName = "Positive Maximum Value Height Get")]
        [TestCase(ParameterType.Height, TestName = "Positive Maximum Value Width Get")]
        [TestCase(ParameterType.Width, TestName = "Positive Maximum Value Wall Thickness Get")]
        [TestCase(ParameterType.WallThickness, TestName = "Positive Maximum Value Parameters Get")]
        [TestCase(ParameterType.IsBottleStraight, TestName = "Positive Maximum Value Is Bottle Straight Get")]
        public void Parameters_GetCorrectMaximumValues(ParameterType parameterType)
        {
            var testParameters = new Parameters();
            double expectedMaximumValue = -1;
            double actualMaximumValue = 0;
            switch (parameterType)
            {
                //TODO: дубль
                case ParameterType.CoverRadius:
                {
                    expectedMaximumValue = Parameters.MAX_COVER_RADIUS;
                    actualMaximumValue = testParameters.GetMaximumValue(parameterType);
                    break;
                }
                case ParameterType.HandleBaseRadius:
                {
                    expectedMaximumValue = Parameters.NOT_SET_MAX_OR_MIN_VALUE;
                    actualMaximumValue = testParameters.GetMaximumValue(parameterType);
                    break;
                }
                case ParameterType.HandleRadius:
                {
                    expectedMaximumValue = Parameters.NOT_SET_MAX_OR_MIN_VALUE;
                    actualMaximumValue = testParameters.GetMaximumValue(parameterType);
                    break;
                }
                case ParameterType.HandleLength:
                {
                    expectedMaximumValue = Parameters.MAX_HANDLE_LENGTH;
                    actualMaximumValue = testParameters.GetMaximumValue(parameterType);
                    break;
                }
                case ParameterType.Height:
                {
                    expectedMaximumValue = Parameters.MAX_HEIGHT;
                    actualMaximumValue = testParameters.GetMaximumValue(parameterType);
                    break;
                }
                case ParameterType.Width:
                {
                    expectedMaximumValue = Parameters.MAX_WIDTH;
                    actualMaximumValue = testParameters.GetMaximumValue(parameterType);
                    break;
                }
                case ParameterType.WallThickness:
                {
                    expectedMaximumValue = Parameters.MAX_WALL_THICKNESS;
                    actualMaximumValue = testParameters.GetMaximumValue(parameterType);
                    break;
                }
                case ParameterType.IsBottleStraight:
                {
                    expectedMaximumValue = -1;
                    actualMaximumValue = testParameters.GetMaximumValue(parameterType);
                    break;
                }
            }
            Assert.AreEqual(expectedMaximumValue, actualMaximumValue);
        }

        //TODO:
        /// <summary>
        /// Set default parameters values
        /// </summary>
        /// <param name="parameters">Parameters</param>
        private Parameters SetDefaultValues(Parameters parameters)
        {
            return parameters = new Parameters
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
    }
}
