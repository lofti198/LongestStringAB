using System;
using System.Text;
using System.Linq;

using System.Collections.Specialized;

class Solution
{
    string GetLongestString(IEnumerable<string> stringCollection)
    {
        if (stringCollection == null || !stringCollection.Any())
        {
            // Handle the case where the collection is null or empty
            return null; // Or return an appropriate default value
        }

        string longestString = stringCollection.First(); // Initialize with the first string

        foreach (string str in stringCollection)
        {
            if (str.Length > longestString.Length)
            {
                // Update if the current string is longer
                longestString = str;
            }
        }

        return longestString;
    }

    public string solution(int AA, int AB, int BB)
    {
        IEnumerable<string> results = GenerateSequences("", AA, AB, BB);
        string longestString = GetLongestString(results);

        return !String.IsNullOrEmpty(longestString)?longestString:"";
    }

    private IEnumerable<string> GenerateSequences(string current, int AA, int AB, int BB)
    {
        // Base case: If AA, AB, and BB are all zero, update result if the current sequence is longer
        if (AA == 0 && AB == 0 && BB == 0)
        {
            return new List<string> { current };
        }

        // Generate strings that can be created
        string startingSequence = System.String.IsNullOrEmpty(current) ? "" : current.Substring(0, 2);

        if(System.String.IsNullOrEmpty(startingSequence))
        {
            List<string> unitedList = new List<string>();
            if(AA > 0) unitedList.AddRange(GenerateSequences("AA", AA-1, AB, BB));
            if (AB > 0) unitedList.AddRange(GenerateSequences("AB", AA, AB-1, BB));
            if (BB > 0) unitedList.AddRange(GenerateSequences("BB", AA, AB, BB-1));
            return unitedList;
        }


        string endingSequence = "";
        if (current.Length > 2) endingSequence = current.Substring(current.Length - 2);

        switch (startingSequence)
        {
            case "AA":
                if (AB > 0) return GenerateSequences($"AB{current}", AA, AB - 1, BB);
                if (BB > 0) return GenerateSequences($"BB{current}", AA, AB, BB - 1);
                break;
            case "BB":
                if (AA > 0) return GenerateSequences($"AA{current}", AA-1, AB, BB);
                break;
            case "AB":
                if (AB > 0) return GenerateSequences($"AB{current}", AA, AB - 1, BB);
                if (BB > 0) return GenerateSequences($"BB{current}", AA, AB, BB - 1);
                break;
            default:
                // throw new Exception("Strange behavior 1");
                break;
        }
        if(!System.String.IsNullOrEmpty(endingSequence))
        {
            switch (endingSequence)
            {
                case "AA":
                    if (BB > 0) return GenerateSequences($"{current}BB", AA, AB, BB - 1);
                    break;
                case "BB":
                    if (AA > 0) return GenerateSequences($"{current}AA", AA - 1, AB, BB);
                    if (AB > 0) return GenerateSequences($"{current}AB", AA, AB - 1, BB);
                    break;
                case "AB":
                    if (AB > 0) return GenerateSequences($"{current}AB", AA, AB - 1, BB);
                    if (AA > 0) return GenerateSequences($"AA{current}", AA - 1, AB, BB);
                    break;
                default:
                    // throw new Exception("Strange behavior 2");
                    break;
            }
        }

        return new List<string> { current };
        // throw new Exception("Strange behavior 3");
    }
}


class Program
{
    static void Main()
    {
        Solution solution = new Solution();
        Console.WriteLine(solution.solution(0, 2, 0)); // Output: "ABAB"
        Console.WriteLine(solution.solution(5, 0, 2)); // Output: "AABBAABBAA"
        Console.WriteLine(solution.solution(1, 2, 1)); // Output: "BBABABAA" or any other valid result

        Console.WriteLine(solution.solution(0, 0, 10)); // Output: "BB"

        Console.ReadKey();
    }
}
