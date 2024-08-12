namespace fixDate.interfaces;

public interface IFileListFilter
{
    List<string> GetAllFileNames(string basePath);
}