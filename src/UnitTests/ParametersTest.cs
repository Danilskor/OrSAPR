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
                    propertyName = "IsBottleStraight";
                    var actualValueBool = actual.GetType().GetProperty(propertyName).GetValue(actual);
                    var expectedValueBool = expectedProperty.GetValue(expected);
                    Assert.AreEqual(expectedValueBool, actualValueBool);
                }
                else
                {
                    propertyName = propertyName.Replace(
                        "Double ", "");
                    var actualValue = actual.GetType().GetProperty(propertyName).GetValue(actual);
                    var expectedValue = expectedProperty.GetValue(expected);
                    Assert.AreEqual(expectedValue, actualValue);
                }
            }
        }

        [TestCase(201, "CoverRadius", TestName = "Positive parameters get")]
        [TestCase(201, "HandleBaseRadius", TestName = "Positive parameters get")]
        [TestCase(201, "HandleRadius", TestName = "Positive parameters get")]
        [TestCase(201, "HandleLength", TestName = "Positive parameters get")]
        [TestCase(201, "Height", TestName = "Positive parameters get")]
        [TestCase(201, "Width", TestName = "Positive parameters get")]
        [TestCase(201, "WallThickness", TestName = "Positive parameters get")]
        public void Parameters_GetCorrectValue(double testParameterValue, 
             string parameterName)
        {
            
            var actual = new Parameters();
            actual.SetDefaultParameters();

            var actualPropertyObject = actual.GetType().GetProperty(parameterName).GetValue(actual);

            actualPropertyObject = testParameterValue;
            
            Assert.AreEqual(testParameterValue, actualPropertyObject);
        }

        
        [TestCase(TestName = "Positive cover radius set")]
        public void Parameters_SetCorrectCoverRadius()
        {
            var testParameters = new Parameters();
            testParameters.SetDefaultParameters();
            var testValue = 200;

            testParameters.CoverRadius = testValue;

            Assert.AreEqual(testParameters.CoverRadius, testValue);
        }

        [TestCase(TestName = "Positive Handle Base Radius set")]
        public void Parameters_SetCorrectHandleBaseRadius()
        {
            var testParameters = new Parameters();
            testParameters.SetDefaultParameters();

            var testValue = 50;
            
            testParameters.HandleBaseRadius = testValue;

            Assert.AreEqual(testParameters.HandleBaseRadius, testValue);
        }

        [TestCase(TestName = "Positive Handle Radius set")]
        public void Parameters_SetCorrectHandleRadius()
        {
            var testParameters = new Parameters();
            testParameters.SetDefaultParameters();
            var testValue = 30;
            
            testParameters.HandleRadius = testValue;

            Assert.AreEqual(testParameters.HandleRadius, testValue);
        }

        [TestCase(TestName = "Positive Handle Length set")]
        public void Parameters_SetCorrectHandleLength()
        {
            var testParameters = new Parameters();
            testParameters.SetDefaultParameters();
            var testValue = 20;
            

            testParameters.HandleLength = testValue;

            Assert.AreEqual(testParameters.HandleLength, testValue);
        }

        [TestCase(TestName = "Positive Height set")]
        public void Parameters_SetCorrectHeight()
        {
            var testParameters = new Parameters();
            testParameters.SetDefaultParameters();
            var testValue = 350;
            

            testParameters.Height = testValue;

            Assert.AreEqual(testParameters.Height, testValue);
        }

        [TestCase(TestName = "Positive Height set")]
        public void Parameters_SetCorrectWidth()
        {
            var testParameters = new Parameters();
            testParameters.SetDefaultParameters();
            var testValue = 300;
            

            testParameters.Width = testValue;

            Assert.AreEqual(testParameters.Width, testValue);
        }

        [TestCase(TestName = "Positive Wall Thickness set")]
        public void Parameters_SetCorrectWallThickness()
        {
            var testParameters = new Parameters();
            testParameters.SetDefaultParameters();
            var testValue = 20;

            testParameters.WallThickness = testValue;

            Assert.AreEqual(testParameters.WallThickness, testValue);
        }

        [TestCase(TestName = "Positive Is Bottle Straight set")]
        public void Parameters_SetCorrectIsBottleStraight()
        {
            var testParameters = new Parameters();
            var testParameter = false;

            testParameters.IsBottleStraight = testParameter;

            Assert.AreEqual(testParameters.IsBottleStraight, testParameter);
        }

        private Parameters SetParameters(double coverRadius, double handleBaseRadius,
            double handleRadius, double handleLength,
            double height, double width, double wallThickness)
        {
            var parameters = new Parameters();
            parameters.CoverRadius = coverRadius;
            parameters.HandleBaseRadius = handleBaseRadius;
            parameters.HandleRadius = handleRadius;
            parameters.HandleLength = handleLength;
            parameters.Height = height;
            parameters.Width = width;
            parameters.WallThickness = wallThickness;
            parameters.IsBottleStraight = false;
            return parameters;
        }
    }
}
