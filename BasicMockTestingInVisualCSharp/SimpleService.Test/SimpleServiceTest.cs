using Moq;
using NUnit.Framework;
using static SimpleService.StringCalculator;

namespace SimpleService.Test
{
    /// <summary>
    /// Mock tests for the Calculator service class.
    /// </summary>
    public class SimpleServiceTest
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleServiceTest"/> class.
        /// </summary>
        public SimpleServiceTest()
        { }

        /// <summary>
        /// Terminates an instance of the <see cref="SimpleServiceTest"/> class.
        /// </summary>
        ~SimpleServiceTest()
        { }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// The calculator service should not register when called with null.
        /// </summary>
        [Test]
        public void RegisterCalculator_Should_NotRegister_When_Called_With_Null()
        {
            // Arrange.
            CalculatorService calculatorService = new CalculatorService();

            // Act.
            calculatorService.RegisterCalculator(null);

            // Assert.
            Assert.AreNotEqual(calculatorService.Calculators[0], new Mock<IStringCalculator>().Object);
        }

        /// <summary>
        /// The calculator service should register when called with a proper mock object.
        /// </summary>
        [Test]
        public void RegisterCalculator_Should_Register_When_Called_With_CalculatorObject()
        {
            // Arrange.
            CalculatorService calculatorService = new CalculatorService();
            Mock<IStringCalculator> calculatorMock = new Mock<IStringCalculator>();

            // Act.
            calculatorService.RegisterCalculator(calculatorMock.Object);

            // Assert.
            Assert.AreEqual(calculatorService.Calculators[0], calculatorMock.Object);
        }

        /// <summary>
        /// The calculator service should call the calculators when called with the supported operator.
        /// </summary>
        [Test]
        public void Calculator_Should_Call_Calculators_When_Called_With_SupportedOperatorType()
        {
            // Arrange.
            CalculatorService calculatorService = new CalculatorService();
            Mock<IStringCalculator> calculatorMock = new Mock<IStringCalculator>();
            calculatorMock.Setup(l => l.SupportsOperatorType(It.Is<OperatorTypes>(level => level == OperatorTypes.Addition))).Returns(true);
            calculatorService.RegisterCalculator(calculatorMock.Object);

            // Act.
            calculatorService.Calculate("//+\n1+2+3+4", OperatorTypes.Addition);

            // Assert.
            calculatorMock.Verify(m => m.Calculate("//+\n1+2+3+4", OperatorTypes.Addition), Times.Once);
        }

        /// <summary>
        /// The calculator service should not call calculators when called with not supported operator.
        /// </summary>
        [Test]
        public void Calculator_Should_Not_Call_Calculators_When_Called_With_NotSupportedOperatorType()
        {
            // Arrange.
            CalculatorService calculatorService = new CalculatorService();
            Mock<IStringCalculator> calculatorMock = new Mock<IStringCalculator>();
            calculatorMock.Setup(l => l.SupportsOperatorType(It.Is<OperatorTypes>(level => level == OperatorTypes.Subtraction))).Returns(true);
            calculatorService.RegisterCalculator(calculatorMock.Object);

            // Act.
            calculatorService.Calculate("//+\n1+2+3+4", OperatorTypes.Multiplication);

            // Assert.
            calculatorMock.Verify(m => m.Calculate("//+\n1+2+3+4", OperatorTypes.Multiplication), Times.Never);
        }

        /// <summary>
        /// The calculator service should call calculators when called with supported operator types and the results are equal (zero in all cases).
        /// </summary>
        [Test]
        public void Calculator_Should_Call_Calculators_When_Called_With_SupportedSeparatorTypes()
        {
            // Arrange.
            CalculatorService calculatorService = new CalculatorService();
            Mock<IStringCalculator> calculatorMock1 = new Mock<IStringCalculator>();
            calculatorMock1.Setup(l => l.SupportsOperatorType(It.Is<OperatorTypes>(level => level == OperatorTypes.Addition))).Returns(true);
            Mock<IStringCalculator> calculatorMock2 = new Mock<IStringCalculator>();
            calculatorMock2.Setup(l => l.SupportsOperatorType(It.Is<OperatorTypes>(level => level == OperatorTypes.Subtraction))).Returns(true);

            // Act.
            calculatorService.RegisterCalculator(calculatorMock1.Object);
            calculatorService.RegisterCalculator(calculatorMock2.Object);

            // Assert.
            Assert.AreEqual(calculatorService.Calculators[0].Calculate("1,2,3", OperatorTypes.Addition), calculatorService.Calculators[1].Calculate("2,3,4", OperatorTypes.Subtraction));
        }
        
        #endregion Methods
    }
}
