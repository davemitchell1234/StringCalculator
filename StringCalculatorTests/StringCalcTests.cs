using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace StringCalculator.Tests
{
    [TestClass()]
    public class StringCalcTests
    {
        private StringCalc _sut;

        [TestInitialize]
        public void Initialise()
        {
            _sut = new StringCalc();
        }

        [TestMethod()]
        public void CallAddWithEmptyStringReturnsZero()
        {
            // Arrange
            var args = "";

            // Act
            var result = _sut.Add(args);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod()]
        public void CallAddWithOneParameterReturnsZero()
        {
            // Arrange
            var args = "1";

            // Act
            var result = _sut.Add(args);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod()]
        public void CallAddWithTwoParametersReturnsZero()
        {
            // Arrange
            var args = "1,2";

            // Act
            var result = _sut.Add(args);

            // Assert
            Assert.AreEqual(0, result);
        }

        [DataTestMethod()]
        [DataRow("1,2,3", 6)]
        [DataRow("4,5,6", 15)]
        public void CallAddWithThreeParametersReturnsCorrectSum(string inputNumbers, int expectedResult)
        {
            // Arrange

            // Act
            var result = _sut.Add(inputNumbers);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod()]
        public void CallAddWithLargeNumberOfParametersReturnsCorrectSum()
        {
            // Arrange
            var args = "1,2,3,6,7,12,15,36,45,67";

            // Act
            var result = _sut.Add(args);

            // Assert
            Assert.AreEqual(194, result);
        }

        [TestMethod()]
        public void CallAddWithNewlineDelimiterReturnsCorrectSum()
        {
            // Arrange
            var args = "1\n2,3";

            // Act
            var result = _sut.Add(args);

            // Assert
            Assert.AreEqual(6, result);
        }

        [TestMethod()]
        public void CallAddWithCustomDelimiterReturnsCorrectSum()
        {
            // Arrange
            var args = "//;\n1;2;3";

            // Act
            var result = _sut.Add(args);

            // Assert
            Assert.AreEqual(6, result);
        }

        [TestMethod()]
        public void CallAddWithNegativeNumbersThrowsException()
        {
            // Arrange
            var args = "1,2,3,-4,5,-6";

            // Act & Assert
            var ex = Assert.ThrowsException<ApplicationException>(() => _sut.Add(args));

            Assert.AreEqual("negatives not allowed - -4,-6", ex.Message);
        }

        [TestMethod()]
        public void CallAddWithNumbersGreaterThan1000AreIgnored()
        {
            // Arrange
            var args = "1,2,3,1000,1001";

            // Act
            var result = _sut.Add(args);

            // Assert
            Assert.AreEqual(1006, result);
        }

        [TestMethod()]
        public void CallAddWithCustomDelimiterAnyLengthReturnsCorrectSum()
        {
            // Arrange
            var args = "//[***]\n1***2***3";

            // Act
            var result = _sut.Add(args);

            // Assert
            Assert.AreEqual(6, result);
        }

        [TestMethod()]
        public void CallAddWithMultipleSingleCharacterCustomDelimitersReturnsCorrectSum()
        {
            // Arrange
            var args = "//[*][%]\n1*2%3";

            // Act
            var result = _sut.Add(args);

            // Assert
            Assert.AreEqual(6, result);
        }

        [TestMethod()]
        public void CallAddWithMultipleCustomDelimitersOfVaryingLengthsReturnsCorrectSum()
        {
            // Arrange
            var args = "//[**][%%%]\n1**2%%%3";

            // Act
            var result = _sut.Add(args);

            // Assert
            Assert.AreEqual(6, result);
        }
    }
}
