using fixDate;
using fixDate.interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace haha; // Note: actual namespace depends on the project name.

internal class Program
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="f">force update file date even if it is already correct</param>
    /// <param name="p">relative path where to find documents to check, default is current path</param>
    /// <param name="v">verbose output, changed only=1, all but unchanged=2, all=3</param>
    static void Main(bool f = false, string p = @".\", int v = 1)
    {
        ServiceCollection services = new ServiceCollection();
        services.AddSingleton<IFixDates, FixDates>();
        services.AddSingleton<IFileListFilter, FileListFilter>();
        services.AddSingleton<IConfigurationReader, ConfigurationReaderHardCoded>();
        services.AddSingleton<IDateMatch, DateMatcher>();
        services.AddSingleton<IDateParsing, DateParsing>();
        services.AddSingleton<IFileNameProvider, FileNameProviderShell>();
        try
        {
            Console.ReadLine();
            Runner? executerService =services.AddSingleton<Runner>()
                .BuildServiceProvider()
                .GetService<Runner>();
            if (executerService != null) 
                executerService.Execute(f, p ,v);
            else
            {
                Console.WriteLine($"Error getting executor service, response is null");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }
}