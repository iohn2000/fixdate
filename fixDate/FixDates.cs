using fixDate.interfaces;

namespace fixDate;

public class FixDates(IFileListFilter fileListFilter, IConfigurationReader cfgReader, IDateMatch dateMatcher,
    IDateParsing dateParser) : IFixDates
{
    public List<TheReportLine> LosGehts(bool forceUpdate, string relativeStartPath = @".\")
    {
        // for parallel each - this has to be thread safe, ConcurrentBag ...
        List<TheReportLine> theReport = new List<TheReportLine>();

        SortedList<int, string> parseFormats = cfgReader.GetDateTimeParsingFormats();
        SortedList<int, string> dateFormats = cfgReader.GetDateTimeMatchingPatterns();
        List<string> excludedFolders = cfgReader.GetExcludedFoldersPatterns();

        var listOfFiles = fileListFilter.GetAllFileNames(relativeStartPath).ToList();
          
        foreach (string theFile in listOfFiles)
        {
            CheckFileDateAndFix(forceUpdate, theFile, dateFormats, parseFormats, theReport);
        }

        return theReport;
    }

    private void CheckFileDateAndFix(
        bool forceUpdate, string f, SortedList<int,string> dateFormats, SortedList<int, string> parseFormats, List<TheReportLine> theReport)
    {
        bool success = false;
        string n = Path.GetFileNameWithoutExtension(f);
        TheMatchResult theMatch = dateMatcher.FindDateFormatMatch(n, dateFormats);
        if (theMatch.Success)
        {
            var parseWentOk = dateParser.TryParseDateExact(theMatch, parseFormats.Values.ToList(), out var fdate);
            if (parseWentOk)
            {
                success = true;
                FileInfo fi = new FileInfo(f);
                if (fi.LastWriteTime != fdate || forceUpdate)
                {
                    fi.LastWriteTime = fdate;
                    fi.Refresh();
                    theReport.Add(new TheReportLine
                    {
                        FileStatus = FStatus.Changed,
                        LogLine = $"{FStatus.Changed}: {f} date changed to:{fdate.ToString("u")}."
                    });
                }
                else
                {
                    theReport.Add(new TheReportLine
                    {
                        FileStatus = FStatus.Unchanged,
                        LogLine = $"{FStatus.Unchanged}: file date already correct for {f}"
                    });
                }
            }
        }

        if (!success)
        {
            theReport.Add(new TheReportLine
                {FileStatus = FStatus.Problem, LogLine = $"{FStatus.Problem}: cannot change {f} "});
        }
    }
}