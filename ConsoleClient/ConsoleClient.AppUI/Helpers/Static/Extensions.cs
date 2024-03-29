﻿using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleClient.AppUI.Helpers;

/// <summary>
/// A static class that contains extension methods.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Returns the string in pieces sliced over words, if possible.
    /// </summary>
    /// <param name="input">The string to slice.</param>
    /// <param name="length">The length of the pieces.</param>
    /// <returns><paramref name="input"/> in <paramref name="length"/> lengthed pieces sliced over words, if possible.</returns>
    public static IList<string> Slice(this string input, int length)
    {
        var tokens = new Regex("\\s").Split(input) ?? new string[0];
        var output = new List<string>();
        string remainder = string.Empty;
        StringBuilder sliceBuilder = new();
        for (int i = 0; i < tokens.Length; i++)
        {
            string token;
            if (tokens[i].Length > length)
            {
                token = tokens[i][..length];
                remainder = tokens[i][length..];
            }
            else if (remainder.Length > length)
            {
                token = remainder[..length];
                remainder = remainder[length..];
                i--;
            }
            else if (remainder.Length > 0)
            {
                token = remainder;
                remainder = string.Empty;
                i--;
            }
            else
            {
                token = tokens[i];
            }

            if (sliceBuilder.Length + token.Length > length)
            {
                output.Add(sliceBuilder.ToString());
                sliceBuilder.Clear();
                sliceBuilder.Append(token);
            }
            else
            {
                sliceBuilder.Append(token);
            }

            if (sliceBuilder.Length + 1 <= length)
            {
                sliceBuilder.Append(' ');
            }
            else
            {
                output.Add(sliceBuilder.ToString());
                sliceBuilder.Clear();
            }
        }
        if (sliceBuilder.Length > 0)
        {
            output.Add(sliceBuilder.ToString());
            sliceBuilder.Clear();
        }
        while (remainder.Length > 0)
        {
            output.Add(remainder[..length]);
            remainder = remainder[length..];
        }
        return output;
    }
}
