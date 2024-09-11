namespace fixDate.FileOperations
{
    public interface IFileNameProvider
    {
        List<string> GetFileNames(string basePath);
    }
}
