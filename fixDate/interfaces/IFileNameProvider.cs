namespace fixDate.interfaces
{
    public interface IFileNameProvider
    {
        List<string> GetFileNames(string basePath);
    }
}
