namespace fixDate.FileOperations;

public class FileManagerDotNet : IFileManager
{
    public string GetFileNameWithoutExtension(string fileName)
    {
        return Path.GetFileNameWithoutExtension(fileName);
    }

    public DateTime GetModifiedDate(string fileName)
    {
        FileInfo fi = new FileInfo(fileName);
        return fi.LastWriteTime;
    }

    public bool SetModifiedDate(string fileName, DateTime fileDate)
    {
        FileInfo fi = new FileInfo(fileName);
        fi.LastWriteTime = fileDate;
        fi.Refresh();
        return true;
    }

    public List<string> GetFileNames(string basePath)
    {
        EnumerationOptions enumOptions = new EnumerationOptions { MatchCasing = MatchCasing.CaseInsensitive, RecurseSubdirectories = true };

        var files = Directory.GetFiles(Path.Combine(basePath, ""), "*.*", enumOptions).ToList();
        return files;
    }
}
