using static SimpleService.StringCalculator;

namespace SimpleService
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStringCalculator
    {
        #region Methods

        /// <summary>
        /// Returns <c>True</c> if the calculator supports the given separator type else returns <c>False</c>.
        /// </summary>
        /// <param name="operatorType">The separator type to query.</param>
        /// <returns><c>True</c> if the calculator supports the given separator type else returns <c>False</c>.</returns>
        bool SupportsOperatorType(OperatorTypes operatorType);

        /// <summary>
        /// Adds non negative numbers given as a string parameter with custom separators.
        /// </summary>
        /// <param name="numbers">The non negative numbers given as a string parameter with custom separators.</param>
        /// <param name="operatorType">The separator type.</param>
        /// <returns>The sum of the The non negative numbers given as a string parameter with custom separators.</returns>
        int Calculate(string numbers, OperatorTypes operatorType);

        /// <summary>
        /// Adds non negative numbers given as a string parameter with custom separators.
        /// </summary>
        /// <param name="numbers">The non negative numbers given as a string parameter with custom separators.</param>
        /// <returns>The sum of the The non negative numbers given as a string parameter with custom separators.</returns>
        int Calculate(string numbers);

        #endregion Methods
    }
}
