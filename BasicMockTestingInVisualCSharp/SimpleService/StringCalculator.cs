using System;
using System.Collections.Generic;

namespace SimpleService
{
    /// <summary>
    /// A simple calulcator to add numbers given as a string parameter, that are non negative and are separated with the default or given custom separators.
    /// </summary>
    public class StringCalculator : IStringCalculator
    {
        #region Fields

        /// <summary>
        /// The basic mathematical operator tpyes.
        /// </summary>
        [Flags]
        public enum OperatorTypes
        {
            /// <summary>
            /// 
            /// </summary>
            Addition,
            /// <summary>
            /// 
            /// </summary>
            Subtraction,
            /// <summary>
            /// 
            /// </summary>
            Multiplication
        }

        #endregion Fields

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        /// <value>
        /// 
        /// </value>
        public OperatorTypes OperatorType
        { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StringCalculator"/> class.
        /// </summary>
        public StringCalculator()
        { }

        /// <summary>
        /// Terminates an instance of the <see cref="StringCalculator"/> class.
        /// </summary>
        ~StringCalculator()
        { }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Calculates operation at non negative numbers given as a string parameter with default or given custom separators.
        /// </summary>
        /// <param name="numbers">The non negative numbers given as a string parameter with default or given custom separators.</param>
        /// <returns>The sum of the non negative numbers.</returns>
        public int Calculate(string numbers)
        {
            // The string is null, empty or only whitespaces.
            if (string.IsNullOrEmpty(numbers) ||
                string.IsNullOrWhiteSpace(numbers))
            {
                return 0;
            }

            int parseHelper = 0;

            // The parameter does not contain separators.
            if (int.TryParse(numbers, out parseHelper))
            {
                return parseHelper;
            }
            // The parameter contains at least one separator.
            else
            {
                int sum = 0;

                if (OperatorType == OperatorTypes.Multiplication)
                {
                    sum = 1;
                }

                // The parameter contains at leat one custom separator.
                if (numbers.StartsWith("//"))
                {
                    // Get the custom separator section of the parameter.
                    string customSeparator = numbers.Substring(0, numbers.IndexOf("\n")).Replace("//", "");

                    // The custom separator is more than one character long.
                    if (customSeparator.Contains("[") &&
                        customSeparator.Contains("]"))
                    {
                        // More then one custom separator is given.
                        if (customSeparator.Contains("]["))
                        {
                            // Split the custom separators at the length marker characters.
                            string[] customSeparators = customSeparator.Split(new string[] { "][" }, StringSplitOptions.None);
                            customSeparators[0] = customSeparators[0].TrimStart('[');
                            customSeparators[customSeparators.Length - 1] = customSeparators[customSeparators.Length - 1].TrimEnd(']');
                            numbers = numbers.Substring(numbers.IndexOf("\n") + 1);

                            foreach (string separator in customSeparators)
                            {
                                numbers = numbers.Replace(separator, ",");
                            }
                        }
                        // Only one custom separator is given.
                        else
                        {
                            customSeparator = customSeparator.Replace("[", "").Replace("]", "");
                            numbers = numbers.Substring(numbers.IndexOf("\n") + 1);
                            numbers = numbers.Replace(customSeparator, ",");
                        }
                    }
                    // The custom separator is one character long and only one custom separator can be given this way.
                    else
                    {
                        if (customSeparator.Length > 1)
                        {
                            throw new Exception("The custom separator can only be one character long.");
                        }

                        numbers = numbers.Substring(numbers.IndexOf("\n") + 1);
                        numbers = numbers.Replace(customSeparator, ",");
                    }
                }

                // The parameter contains at least one new line escape character as a separator.
                if (numbers.Contains("\n"))
                {
                    numbers = numbers.Replace("\n", ",");
                }

                // Only separators are given in the parameter.
                if (string.IsNullOrEmpty(numbers.Replace(",", "")) ||
                    string.IsNullOrWhiteSpace(numbers.Replace(",", "")))
                {
                    throw new Exception("The parameter does not contain any numbers.");
                }

                // Split the numbers at the default comma separator.
                string[] sliptParts = numbers.Split(',');
                List<string> negativeNumbers = new List<string>();

                // Process the numbers one by one.
                foreach (string part in sliptParts)
                {
                    // The number can not be parsed.
                    if (!int.TryParse(part.Trim(), out parseHelper))
                    {
                        throw new Exception("The number is not is a correct format.");
                    }

                    // Non of the numbers can be negative.
                    if (parseHelper < 0)
                    {
                        negativeNumbers.Add(parseHelper.ToString());
                    }
                    // The number is larger then 1000, it should be ignored.
                    else if (parseHelper > 1000)
                    {
                        parseHelper = 0;

                        if (OperatorType == OperatorTypes.Multiplication)
                        {
                            parseHelper = 1;
                        }
                    }

                    switch (OperatorType)
                    {
                        case OperatorTypes.Addition:
                            sum += parseHelper;
                            break;
                        case OperatorTypes.Subtraction:
                            sum -= parseHelper;
                            break;
                        case OperatorTypes.Multiplication:
                            sum *= parseHelper;
                            break;
                    }
                }

                // Negative numbers were found among the given numbers.
                if (negativeNumbers.Count > 0)
                {
                    throw new Exception("Negatives not allowed: " + string.Join(", ", negativeNumbers));
                }

                return sum;
            }
        }

        /// <summary>
        /// Sets the operator type and calculates operation at non negative numbers given as a string parameter with default or given custom separators.
        /// </summary>
        /// <param name="numbers">The non negative numbers given as a string parameter with default or given custom separators.</param>
        /// <param name="operatorType">The operator that is used in the calculation.</param>
        /// <returns>The sum of the non negative numbers.</returns>
        public int Calculate(string numbers, OperatorTypes operatorType)
        {
            OperatorType = operatorType;

            return Calculate(numbers);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operatorType">The operator that is used in the calculation.</param>
        /// <returns></returns>
        public bool SupportsOperatorType(OperatorTypes operatorType)
        {
            return OperatorType == operatorType;
        }

        #endregion Methods
    }
}
