using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LineAdjustment
{
    public class LineAdjustmentAlgorithm
    {
        private const char separator = ' ';

        public string Transform(string input, int lineWidth)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            var arStr = new ReadOnlySpan<string>(input.Split(separator));

            if (arStr.Length == 1)
            {
                var resultStr = new StringBuilder(capacity: lineWidth);
                resultStr.Append(arStr[0]);
                var numbersOfSpaces = lineWidth - arStr[0].Length;

                AddSeparator(resultStr, numbersOfSpaces);

                return resultStr.ToString();
            }

            var resultList = new List<string>(capacity: arStr.Length);
            var buffer = new List<string>();
            foreach (var item in arStr)
            {
                int currentStrlen = string.Join(separator, buffer).Length;
                var sumLength = currentStrlen + item.Length;

                if (sumLength >= lineWidth)
                {
                    int numbersOfSpaces = (lineWidth - currentStrlen);

                    if (buffer.Count == 1)
                    {
                        var word = new StringBuilder(buffer[0]);

                        AddSeparator(word, numbersOfSpaces);

                        buffer[0] = word.ToString();
                        var str = String.Join("", buffer);
                        resultList.Add(str);
                    }
                    else
                    {
                        WorkerWords(buffer, numbersOfSpaces);

                        var str = String.Join("", buffer);
                        str = str.TrimEnd();
                        resultList.Add(str);
                    }

                    buffer.Clear();
                    buffer.Add(item);
                }
                else
                {
                    buffer.Add(item);
                }
            }

            if (buffer.Any())
            {
                int currentStrlen = string.Join(separator, buffer).Length;
                int numbersOfSpaces = (lineWidth - currentStrlen);

                //since there is no hyphen at the end
                WorkerWords(buffer, numbersOfSpaces - 1);

                var str = String.Join("", buffer);
                resultList.Add(str);
            }

            return String.Join("\n", resultList);
        }

        private static void WorkerWords(List<string> currentStrArray, int v)
        {
            for (int i = 0; i < currentStrArray.Count; i++)
            {
                var word = new StringBuilder(currentStrArray[i]);
                if (i == 0)
                {
                    for (int j = 0; j <= v; j++)
                    {
                        word.Append(separator);
                    }

                    currentStrArray[0] = word.ToString();
                    continue;
                }

                currentStrArray[i] = word.Append(separator).ToString();
            }
        }

        private static void AddSeparator(StringBuilder resultStr, int v)
        {
            for (int i = 0; i < v; i++)
            {
                resultStr.Append(separator);
            }
        }
    }
}