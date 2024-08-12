using fixDate.Models;

namespace fixDate.interfaces;

public interface IFileListFilter
{
    List<FileNameItem> GetAllFileNames(string basePath);
}