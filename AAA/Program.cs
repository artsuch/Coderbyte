// See https://aka.ms/new-console-template for more information
using System;
using System.Linq;

Console.WriteLine(Example.PalindromeCreator("dd"));
Console.WriteLine(Example.PalindromeCreator("mmop"));
Console.WriteLine(Example.PalindromeCreator("abjchba"));
Console.WriteLine(Example.PalindromeCreator("kjjjhjjj"));
Console.WriteLine(Example.PalindromeCreator("kkkkkjjjhjjj"));
Console.WriteLine(Example.PalindromeCreator("kkkkkaaaajjjhjjj"));
Console.WriteLine(Example.PalindromeCreator("abcdecba"));
Console.WriteLine(Example.PalindromeCreator("adbcceba"));
Console.ReadLine(); 

public static class Example
{
    private static Dictionary<char, List<int>> symbolPositions;
    private static readonly string isNotPalindromMessage = "not possible";
    private static readonly string isPalindromMessage = "palindrome";
    private static readonly string lengthValidationMessage = "Length less than 3";

    public static string PalindromeCreator(string str)
    {
        symbolPositions = new Dictionary<char, List<int>>();

        if (str.Length < 3)
        {
            return lengthValidationMessage;
        }

        if (IsPalindrome(str))
        {
            return isPalindromMessage;
        }

        GroupSymbols(str);

        if (IsPalindromeExtendedVersion(out string secondChanceValues) != string.Empty)
        {
            return secondChanceValues;
        }

        return isNotPalindromMessage;
    }

    private static bool IsPalindrome(string str)
    {
        var flag = str.SequenceEqual(str.Reverse()) && str.Length > 2;
        return flag;

    }

    private static string IsPalindromeExtendedVersion(out string symbols)
    {
        symbols = NewPalindromeFromPositions();
        return symbols;

    }

    private static string NewPalindromeFromPositions()
    {
        KeyValuePair<string, string> result;

        for (int i = 0; i < symbolPositions.Count; i++)
        {
            symbolPositions.ElementAt(i);
            result = OrderSymbolsExtendedVersion(symbolPositions, new List<int> { i });

            if (IsPalindrome(result.Key))
            {
                return result.Value;
            }
            for(int j = 0; j < symbolPositions.Count; j++)           
            {
                if ( i != j )
                {
                    result = OrderSymbolsExtendedVersion(symbolPositions, new List<int> { i, j });
                    if (IsPalindrome(result.Key))
                    {
                        return result.Value;
                    }
                }               
            }
            i++;
        }

        return string.Empty;
    }

    private static KeyValuePair<string, string> OrderSymbolsExtendedVersion(Dictionary<char, List<int>> symbols, List<int> indexesOfSymoblsToSkip)
    {
        var reversedDictionary = new Dictionary<int, char>();

        var skippedSymbols = string.Empty;

        int i = 0;
        foreach (var symbol in symbols)
        {
            if (!indexesOfSymoblsToSkip.Contains(i))
            {
                foreach (var v in symbol.Value)
                {
                    reversedDictionary[v] = symbol.Key;
                }
            }
            else
            {
                skippedSymbols += string.Concat(symbols.ElementAt(i).Key);
            }
            i++;
        }

        var newOutputString = string.Concat(reversedDictionary.OrderBy(x => x.Key).Select(x => x.Value));

        return new KeyValuePair<string, string>(newOutputString, skippedSymbols);
    }

    private static void GroupSymbols(string str)
    {
        int i = 0;
        foreach (var s in str)
        {
            if (symbolPositions.ContainsKey(s))
            {
                symbolPositions[s].Add(i);
            }
            else
            {
                symbolPositions.Add(s, new List<int> { i });
            }
            i++;
        }

    }
}