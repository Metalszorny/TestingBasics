using System.Collections.Generic;
using System.Linq;
using static SimpleService.StringCalculator;

namespace SimpleService
{
    /// <summary>
    /// 
    /// </summary>
    public class CalculatorService
    {
        #region Fields

        // The list of calculators.
        private List<IStringCalculator> _calculators = new List<IStringCalculator>();

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the value of the list of calculators.
        /// </summary>
        public List<IStringCalculator> Calculators
        {
            get { return _calculators; }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculatorService"/> class.
        /// </summary>
        public CalculatorService()
        { }

        /// <summary>
        /// Terminates an instance of the <see cref="CalculatorService"/> class.
        /// </summary>
        ~CalculatorService()
        { }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Adds a calculator to the list of calculators.
        /// </summary>
        /// <param name="calculator">The calculator to add to tha list.</param>
        public void RegisterCalculator(IStringCalculator calculator)
        {
            _calculators.Add(calculator);
        }

        /// <summary>
        /// Adds non negative numbers given as a string parameter with custom separators.
        /// </summary>
        /// <param name="numbers">The non negative numbers given as a string parameter with custom separators.</param>
        /// <param name="operatorType">The separator type.</param>
        public List<int> Calculate(string numbers, OperatorTypes operatorType)
        {
            List<int> returnValue = new List<int>();

            foreach (var calculators in _calculators.Where(l => l.SupportsOperatorType(operatorType)))
            {
                returnValue.Add(calculators.Calculate(numbers, operatorType));
            }

            return returnValue;
        }

        /// <summary>
        /// Adds non negative numbers given as a string parameter with custom separators.
        /// </summary>
        /// <param name="numbers">The non negative numbers given as a string parameter with custom separators.</param>
        public List<int> Calculate(string numbers)
        {
            List<int> returnValue = new List<int>();

            foreach (var calculator in _calculators)
            {
                returnValue.Add(calculator.Calculate(numbers));
            }

            return returnValue;
        }

        #endregion Methods
    }
}
