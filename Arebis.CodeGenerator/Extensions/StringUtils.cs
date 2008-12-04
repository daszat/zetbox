using System;
using System.Collections.Generic;
using System.Text;

namespace Arebis.Utils
{
    /// <summary>
    /// Utility class containing several static utility methods operating on strings.
    /// </summary>
	public static class StringUtils
	{
		/// <summary>
		/// Returns an array with all index position where the searchedString appears.
		/// </summary>
		public static int[] AllIndexesOf(string value, string searchedString, StringComparison comparisonType)
		{
			List<int> indexes = new List<int>();
			int pos = 0;
			while (true)
			{
				pos = value.IndexOf(searchedString, pos, comparisonType);
				if (pos == -1) break;
				indexes.Add(pos);
				pos += searchedString.Length;
			}
			return indexes.ToArray();
		}
		
		/// <summary>
		/// Generates an identifier based on the given string. The identifier is
		/// guaranteed to start with a letter or an underscore, and contains only
		/// letters, numbers and underscores.
		/// </summary>
		public static string ToIdentifier(string s)
		{
			StringBuilder identifier = new StringBuilder(s.Length);
			int pos = -1;
			foreach (char c in s.ToCharArray())
			{
				pos++;
				if (((c >= '0') && (c <= '9')))
				{
					if (pos == 0) identifier.Append("Id");
					identifier.Append(c);
				}
				else if (((c >= 'a') && (c <= 'z')) || ((c >= 'A') && (c <= 'Z')))
					identifier.Append(c);
				else if (c == ' ')
					identifier.Append("");
				else
					identifier.Append('_');
			}
			if (identifier.Length == 0) identifier.Append("BlankIdentifier");
			return identifier.ToString();
		}

		/// <summary>
		/// Splits a string into parts based on a given list of tokens.
		/// </summary>
		/// <param name="str">The string to be parsed.</param>
		/// <param name="tokens">The list of tokens.</param>
		/// <param name="includeTokens">Whether the result should include the tokens.</param>
		/// <param name="includeEmptyStrings">Whether empty strings should be stripped out of the result or not.</param>
		/// <param name="comparisonType">Type of string comparison to perform to lookup the tokens.</param>
		/// <returns>An array of sections of the original string where tokens have been used as delimiters.</returns>
		public static string[] SplitByTokens(string str, string[] tokens, bool includeTokens, bool includeEmptyStrings, StringComparison comparisonType)
		{
			// Retrieve all indexpositions where tokens appear:
			List<int> indexes = new List<int>();
			Dictionary<int, string> tokenPositions = new Dictionary<int, string>();
			foreach (string token in tokens)
			{
				foreach (int index in StringUtils.AllIndexesOf(str, token, comparisonType))
				{
					indexes.Add(index);
					tokenPositions.Add(index, token);
				}
			}
			// Sort the indexpositions:
			indexes.Sort();

			// If no tokens found, simply return string:
			if (indexes.Count == 0)
			{
				return new string[] { str };
			}

			// Build result:
			List<string> splits = new List<string>();
			int pos = 0;

			// Add what preceeds the next token, and add eventually the next token:
			foreach (int index in indexes)
			{
				// What preceeds the token:
				splits.Add(str.Substring(pos, (index - pos)));
				// Add the token:
				if (includeTokens)
				{
					splits.Add(str.Substring(index, tokenPositions[index].Length));
				}
				// Move position:
				pos = index + tokenPositions[index].Length;
			}

			// Add part after last token:
			splits.Add(str.Substring(pos));

			// Remove empty strings if requested:
			if (!includeEmptyStrings)
			{
				for (int i = splits.Count - 1; i >= 0; i--)
					if (splits[i].Equals(String.Empty)) splits.RemoveAt(i);
			}

			// Translate result to array and return:
			return splits.ToArray();
		}
	}
}
