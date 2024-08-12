using fixDate.interfaces;
using System.Text.RegularExpressions;

namespace fixDate;

public class FileListFilter : IFileListFilter
{
    private readonly IFileNameProvider fileNameProvider;
    private readonly IConfigurationReader configReader;

    public FileListFilter(IFileNameProvider fileNameProvider, IConfigurationReader configReader)
    {
        this.fileNameProvider = fileNameProvider;
        this.configReader = configReader;
    }

    public List<string> GetAllFileNames(string basePath)
    {
        var excludedFolders = configReader.GetExcludedFoldersPatterns();
        var unfilteredFiles = fileNameProvider.GetFileNames(basePath);
        var filteredFiles = unfilteredFiles.Where(w=> 
        {
            var pathOnly = Path.GetDirectoryName(Path.GetFullPath(w.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)));
            var inPattern = excludedFolders.Any(a=>!new Regex(a, RegexOptions.IgnoreCase).IsMatch(pathOnly));
            if (inPattern) return false;
            return true;
        });
        return filteredFiles.ToList();
     }
}