using fixDate.Models;

namespace fixDate.interfaces;


public interface IDateMatch
{
    /// <summary>
    /// tries to find the last date pattern in a string and returns only the date/time part
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="patterns">list of patterns to try to match file name against, the order ist inmportant</param>
    /// <returns></returns>
    TheMatchResult FindDateFormatMatch(string fileName, SortedList<int,string> patterns);

    /// <summary>
    /// try to clean away characters the are not needed but stop date matching
    /// </summary>
    /// <param name="originalName"></param>
    /// <returns></returns>
    string TryCleaningUpFilename(string originalName);
}