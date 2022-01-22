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
        //TODO:

        [TestCase(TestName = "Positive default parameters set")]
        public void Parameters_SetDefaultValue()
        {
            //Set
            var expected = new Parameters();
            expected.SetDefaultParameters();

            //Act
            var actual = new Parameters();
            actual = SetParameters(200, 10, 30,
                10, 300, 200, 7);

            //Assert
            foreach (PropertyInfo expectedProperty in expected.GetType().GetProperties())
            {
                var propertyName = expectedProperty.ToString();
                if (propertyName == "Boolean IsBottleStraight")
                {
                    propertyName = propertyName.Replace("Boolean ", "");
                    var actualPropertyBool = actual.GetType().GetProperty(propertyName);
                    var expectedValueBool = expectedProperty.GetValue(expected);
                    var actualValueBool = actualPropertyBool.GetValue(actual);
                    Assert.AreEqual(expectedValueBool, actualValueBool);
                }
                else
                {
                    propertyName = propertyName.Replace("BottleParameters.Parameter ", "");
                    var actualProperty = actual.GetType().GetProperty(propertyName);
                    dynamic expectPropertyObject = expectedProperty.GetValue(expected);
                    dynamic actualPropertyObject = actualProperty.GetValue(actual);
                    var expectedValue = expectPropertyObject.ParameterValue;
                    var actualValue = actualPropertyObject.ParameterValue;
                    Assert.AreEqual(expectedValue, actualValue);
                }
            }
        }

        [TestCase(201, 200, 300, "CoverRadius", TestName = "Positive parameters get")]
        [TestCase(201, 200, 300, "HandleBaseRadius", TestName = "Positive parameters get")]
        [TestCase(201, 200, 300, "HandleRadius", TestName = "Positive parameters get")]
        [TestCase(201, 200, 300, "HandleLength", TestName = "Positive parameters get")]
        [TestCase(201, 200, 300, "Height", TestName = "Positive parameters get")]
        [TestCase(201, 200, 300, "Width", TestName = "Positive parameters get")]
        [TestCase(201, 200, 300, "WallThickness", TestName = "Positive parameters get")]
        public void Parameters_GetCorrectValue(double testParameterValue, double minimumValue, double maximumValue,
            string parameterName)
        {
            //Act
            var actual = new Parameters();
            actual = SetParameters(200, 10, 30,
                10, 300, 200, 7);
            dynamic actualPropertyObject = actual.GetType().GetProperty(parameterName).GetValue(actual);
            actualPropertyObject.MinimumValue = minimumValue;
            actualPropertyObject.MaximumValue = maximumValue;
            actualPropertyObject.ParameterValue = testParameterValue;
            //Assert
            Assert.AreEqual(testParameterValue, actualPropertyObject.ParameterValue);
        }

        
        [TestCase(TestName = "Positive cover radius set")]
        public void Parameters_SetCorrectCoverRadius()
        {
            var testParameters = new Parameters();
            var testParameter = new Parameter(10, 50);
            testParameter.ParameterValue = 30;

            testParameters.CoverRadius = testParameter;

            Assert.AreEqual(testParameters.CoverRadius, testParameter);
        }

        [TestCase(TestName = "Positive Handle Base Radius set")]
        public void Parameters_SetCorrectHandleBaseRadius()
        {
            var testParameters = new Parameters();
            var testParameter = new Parameter(10, 50);
            testParameter.ParameterValue = 30;

            testParameters.HandleBaseRadius = testParameter;

            Assert.AreEqual(testParameters.HandleBaseRadius, testParameter);
        }

        [TestCase(TestName = "Positive Handle Radius set")]
        public void Parameters_SetCorrectHandleRadius()
        {
            var testParameters = new Parameters();
            var testParameter = new Parameter(10, 50);
            testParameter.ParameterValue = 30;

            testParameters.HandleRadius = testParameter;

            Assert.AreEqual(testParameters.HandleRadius, testParameter);
        }

        [TestCase(TestName = "Positive Handle Length set")]
        public void Parameters_SetCorrectHandleLength()
        {
            var testParameters = new Parameters();
            var testParameter = new Parameter(10, 50);
            testParameter.ParameterValue = 30;

            testParameters.HandleLength = testParameter;

            Assert.AreEqual(testParameters.HandleLength, testParameter);
        }

        [TestCase(TestName = "Positive Height set")]
        public void Parameters_SetCorrectHeight()
        {
            var testParameters = new Parameters();
            var testParameter = new Parameter(10, 50);
            testParameter.ParameterValue = 30;

            testParameters.Height = testParameter;

            Assert.AreEqual(testParameters.Height, testParameter);
        }

        [TestCase(TestName = "Positive Height set")]
        public void Parameters_SetCorrectWidth()
        {
            var testParameters = new Parameters();
            var testParameter = new Parameter(10, 50);
            testParameter.ParameterValue = 30;

            testParameters.Width = testParameter;

            Assert.AreEqual(testParameters.Width, testParameter);
        }

        [TestCase(TestName = "Positive Wall Thickness set")]
        public void Parameters_SetCorrectWallThickness()
        {
            var testParameters = new Parameters();
            var testParameter = new Parameter(10, 50);
            testParameter.ParameterValue = 30;

            testParameters.WallThickness = testParameter;

            Assert.AreEqual(testParameters.WallThickness, testParameter);
        }

        [TestCase(TestName = "Positive Is Bottle Straight set")]
        public void Parameters_SetCorrectIsBottleStraight()
        {
            var testParameters = new Parameters();
            var testParameter = false;

            testParameters.IsBottleStraight = testParameter;

            Assert.True(testParameters.IsBottleStraight == testParameter);
            Assert.AreEqual(testParameters.IsBottleStraight, testParameter);
        }

        private Parameters SetParameters(double coverRadius, double handleBaseRadius,
            double handleRadius, double handleLength,
            double height, double width, double wallThickness)
        {
            var parameters = new Parameters();
            parameters.CoverRadius.ParameterValue = coverRadius;
            parameters.HandleBaseRadius.MaximumValue = 10;
            parameters.HandleBaseRadius.ParameterValue = handleBaseRadius;
            parameters.HandleRadius.MinimumValue = 30;
            parameters.HandleRadius.MaximumValue = 60;
            parameters.HandleRadius.ParameterValue = handleRadius;
            parameters.HandleLength.ParameterValue = handleLength;
            parameters.Height.ParameterValue = height;
            parameters.Width.ParameterValue = width;
            parameters.WallThickness.ParameterValue = wallThickness;
            parameters.IsBottleStraight = false;
            return parameters;
        }
    }
}
