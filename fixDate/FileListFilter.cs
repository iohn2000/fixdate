using fixDate.FileOperations;
using fixDate.interfaces;
using fixDate.Models;
using System.Text.RegularExpressions;

namespace fixDate;

public class FileListFilter(IFileNameProvider fileNameProvider, IConfigurationReader configReader) : IFileListFilter
{
    public List<FileNameItem> GetAllFileNames(string basePath)
    {
        var regex = configReader.GetExcludedFoldersPatterns()
            .Where(w=>!string.IsNullOrWhiteSpace(w))
            .Select(p=>new Regex(p,RegexOptions.IgnoreCase)).ToList();

        var unfilteredFiles = fileNameProvider.GetFileNames(basePath);

        var filteredFiles = unfilteredFiles.Select(s=>new FileNameItem
        { 
            FileName = s,
            IsIncluded = !regex.Any(a =>
            {
                string? cleanPath = Path.GetDirectoryName(s.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
                return a.IsMatch(cleanPath);
            })
        }).ToList();

        return filteredFiles;
     }
}