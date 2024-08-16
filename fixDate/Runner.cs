using fixDate.interfaces;
using fixDate.Models;

namespace fixDate;

/// <summary>
/// class a starter class
/// </summary>
public class Runner
{
    private readonly IFixDates _fixDates;

    public Runner(IFixDates fixDates)
    {
        _fixDates = fixDates;
    }

    public void Execute(bool forceUpdate, string relStartPath=@".\", int verbose = 1)
    {
        Console.WriteLine("Changing Modified Date of Filename according to suffix.");
        List<TheReportLine> report = _fixDates.LosGehts(forceUpdate, relStartPath);

        var reportStatus = verbose switch
        {
            1 => Enum.GetValues(typeof(FStatus)).Cast<FStatus>().Where(w=>w == FStatus.Changed),
            2 => Enum.GetValues(typeof(FStatus)).Cast<FStatus>().Where(w=>w != FStatus.Unchanged),
            3 => Enum.GetValues(typeof(FStatus)).Cast<FStatus>(),
            _ => Enum.GetValues(typeof(FStatus)).Cast<FStatus>().Where(w => w == FStatus.Changed)
        };

        foreach (FStatus status in reportStatus)
        {
            report
                .Where(w => w.FileStatus == status)
                .Select(w => w.LogLine)
                .ToList()
                .ForEach(l => Console.WriteLine(l));
            Console.WriteLine("");
        }
    }
}