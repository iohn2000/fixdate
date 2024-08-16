using System.ComponentModel.Design;
using System.Text.RegularExpressions;
using System.Xml.XPath;
using fixDate.interfaces;
using fixDate.Models;

namespace fixDate;

/// <inheritdoc />
public class DateMatcher : IDateMatch
{
    /// <inheritdoc />
    public TheMatchResult FindDateFormatMatch(string fileName, SortedList<int,string> patterns)
    {
        if (fileName == null)
        {
            return new TheMatchResult
            {
                Success = false,
                TheValue = ""
            };
        }

        fileName = TryCleaningUpFilename(fileName);
        foreach (var pattern in patterns)
        {
            MatchCollection result = Regex.Matches(fileName, pattern.Value);
            if (result.Count > 0)
            {
                return new TheMatchResult
                {
                    Success = result[^1].Success,
                    TheValue = result[^1].Value
                };
            }
        }

        return new TheMatchResult
        {
            Success = false,
            TheValue = ""
        };

    }

    /// <inheritdoc />
    public string TryCleaningUpFilename(string originalName)
    {
        MatchCollection result = Regex.Matches(originalName, "_[\\d]*$");
        if (result.Count > 0)
        {
            string clean = originalName.Remove(result[^1].Index, result[^1].Length);
            return clean;
        }

        return originalName;
    }
}