using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class StringCalc
    {
        private const string _commaDelimiter = ",";
        private const string _newlineDelimiter = "\n";

        public int Add(string numbers)
        {
            var delimiters = new List<string> { _commaDelimiter, _newlineDelimiter };
            var inputValue = numbers;

            inputValue = CheckForDelimiterInput(numbers, delimiters, inputValue);

            var parts = inputValue.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

            if (parts.Count < 3)
            {
                return 0;
            }
            else
            {
                return AnalyseInputNumbers(parts);
            }
        }

        private string CheckForDelimiterInput(string numbers, List<string> delimiters, string inputValue)
        {
            if (numbers.Length > 4 && numbers.StartsWith("//"))
            {
                var firstNewLine = inputValue.IndexOf("\n");

                switch (firstNewLine)
                {
                    case -1:
                    case 0:
                    case 1:
                    case 2:
                        // No delimiters - drop out and return inputValue
                        break;

                    case 3:
                        // Single one character delimiter
                        delimiters.Add(numbers.Substring(2, 1));
                        inputValue = numbers.Substring(4);
                        break;

                    default:
                        // Multiple delimiters of varying lengths
                        var delimiterString = numbers.Substring(2, firstNewLine - 2);

                        var delimitersSplit = delimiterString.Split(new string[] { "[", "]" }, 
                                                                    StringSplitOptions.RemoveEmptyEntries)
                                                                    .ToList();

                        if (delimitersSplit.Any())
                        {
                            delimiters.AddRange(delimitersSplit);
                            inputValue = numbers.Substring(firstNewLine + 1);
                        }
                        break;
                }
            }

            return inputValue;
        }

        private int AnalyseInputNumbers(List<string> parts)
        {
            var result = 0;

            var negativeNumbers = new List<int>();

            parts.ForEach(s =>
            {
                if (int.TryParse(s, out var number))
                {
                    if (number < 0)
                    {
                        negativeNumbers.Add(number);
                    }
                    else if (number <= 1000)
                    {
                        result += number;
                    }
                }
            });

            if (negativeNumbers.Any())
            {
                throw new ApplicationException($"negatives not allowed - {string.Join(",", negativeNumbers)}");
            }

            return result;
        }
    }
}
