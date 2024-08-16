using fixDate.Models;

namespace fixDate.interfaces;

public interface IFixDates
{
    List<TheReportLine> LosGehts(bool forceUpdate, string relativStartPath);
}