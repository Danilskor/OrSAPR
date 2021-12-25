using System;
using System.Collections.Generic;
using NUnit.Framework;
using BottleParameters;

namespace UnitTests
{
    [TestFixture]
    public class ParametersTest
    {
        //TODO:
        private Parameters _parameters = new Parameters();

        [TestCase(TestName = "Positive parameters setter")]
        public void Parameters_SetCorrectValue()
        {
            //Setup
            _parameters.AddAllParameters(200, 10, 30,
                10, 300, 200, 7,
                _parameters.ParametersList); ;
            var expected = _parameters.ParametersList;

            //Act
            var actual =  new List<double>
            {
                200,
                10,
                30,
                10,
                300,
                200,
                7
            };

            //Assert
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].ParameterValue, actual[i]);
            }
        }

        [TestCase(ParameterTypeEnum.HandleBaseRadius, 0, 
            TestName =
            "Negative HandleBaseRadius setter test, values less then minimum")]
        [TestCase(ParameterTypeEnum.WallThickness, 0, 
            TestName =
            "Negative WallThickness setter test, values less then minimum")]
        [TestCase(ParameterTypeEnum.Height, 0,
            TestName =
            "Negative Height setter test, values less then minimum")]
        [TestCase(ParameterTypeEnum.HandleRadius, 0,
            TestName =
            "Negative HandleRadius setter test, values less then minimum")]
        [TestCase(ParameterTypeEnum.HandleLength, 0,
            TestName =
            "Negative HandleLength setter test, values less then minimum")]
        [TestCase(ParameterTypeEnum.Width, 0,
            TestName =
            "Negative Width setter test, values less then minimum")]
        [TestCase(ParameterTypeEnum.CoverRadius, 0,
            TestName =
            "Negative CoverRadius setter test, values less then minimum")]
        [TestCase(ParameterTypeEnum.HandleBaseRadius, 1000,
            TestName =
                "Negative HandleBaseRadius setter test, values more then maximum")]
        [TestCase(ParameterTypeEnum.WallThickness, 1000,
            TestName =
                "Negative WallThickness setter test, values more then maximum")]
        [TestCase(ParameterTypeEnum.Height, 1000,
            TestName =
                "Negative Height setter test, values more then maximum")]
        [TestCase(ParameterTypeEnum.HandleRadius, 1000,
            TestName =
                "Negative HandleRadius setter test, values more then maximum")]
        [TestCase(ParameterTypeEnum.HandleLength, 1000,
            TestName =
                "Negative HandleLength setter test, values more then maximum")]
        [TestCase(ParameterTypeEnum.Width, 1000,
            TestName =
                "Negative Width setter test, values more then maximum")]
        [TestCase(ParameterTypeEnum.CoverRadius, 1000,
            TestName =
                "Negative CoverRadius setter test, values less then minimum")]
        public void Parameters_SetOutOfRange(ParameterTypeEnum parameterType,
            double value)
        {
            //Assert
            Assert.Throws<ArgumentException>(
                () =>
                {
                   _parameters.AddParameter(value, parameterType, _parameters.ParametersList);
                },
                "Exception: argument out of range");
        }
    }
}
