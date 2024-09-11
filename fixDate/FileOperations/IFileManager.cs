namespace fixDate.FileOperations;

public interface IFileManager
{
    bool SetModifiedDate(string fileName, DateTime fileDate);
    DateTime GetModifiedDate(string fileName);
    string GetFileNameWithoutExtension(string fileName);
    List<string> GetFileNames(string basePath);
}
