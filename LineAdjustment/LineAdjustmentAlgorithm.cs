using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LineAdjustment
{
    public class LineAdjustmentAlgorithm
    {
        public string Transform(string input, int lineWidth)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            var arStr = new ReadOnlySpan<string>(input.Split(' '));
            var resultStr = new StringBuilder(capacity: lineWidth);

            if (arStr.Length == 1)
            {
                resultStr.Append(arStr[0]);
                for (int i = 0; i < (lineWidth - arStr[0].Length); i++)
                {
                    resultStr.Append(' ');
                }
                return resultStr.ToString();
            }

            var resultList = new List<string>(capacity: arStr.Length);
            var currentStrArray = new List<string>();
            foreach (var item in arStr)
            {
                string v1 = string.Join(" ", currentStrArray);
                int currentStrlen = v1.Length;
                var sumLength = currentStrlen + item.Length;

                if (sumLength >= lineWidth)
                {
                    int v = (lineWidth - currentStrlen);

                    if (currentStrArray.Count == 1)
                    {
                        var word = new StringBuilder(currentStrArray[0]);

                        for (int j = 0; j < v; j++)
                        {
                            word.Append(' ');
                        }

                        currentStrArray[0] = word.ToString();
                    }
                    else
                    {
                        for (int i = 0; i < currentStrArray.Count; i++)
                        {
                            var word = new StringBuilder(currentStrArray[i]);
                            if (i == 0)
                            {
                                for (int j = 0; j <= v; j++)
                                {
                                    word.Append(' ');
                                }

                                currentStrArray[0] = word.ToString();
                                continue;
                            }

                            currentStrArray[i] = word.Append(' ').ToString();
                        }
                    }

                    var str = String.Join("", currentStrArray);

                    if (currentStrArray.Count != 1)
                    {
                        str = str.TrimEnd();
                    }

                    System.Diagnostics.Debug.Write(str);
                    System.Diagnostics.Debug.WriteLine(str.Length);

                    resultList.Add(str);

                    currentStrArray.Clear();
                    currentStrArray.Add(item);
                }
                else
                {
                    currentStrArray.Add(item);
                }
            }

            System.Diagnostics.Debug.Write(currentStrArray.Last());

            if (currentStrArray.Any())
            {
                string v1 = string.Join(" ", currentStrArray);
                int currentStrlen = v1.Length;
                int v = (lineWidth - currentStrlen);

                for (int i = 0; i < currentStrArray.Count; i++)
                {
                    var word = new StringBuilder(currentStrArray[i]);
                    if (i == 0)
                    {
                        for (int j = 0; j < v; j++)
                        {
                            word.Append(' ');
                        }

                        currentStrArray[0] = word.ToString();
                        continue;
                    }

                    currentStrArray[i] = word.Append(' ').ToString();
                }

                var str = String.Join("", currentStrArray);
                resultList.Add(str);
            }

            return String.Join("\n", resultList);
        }
    }
}