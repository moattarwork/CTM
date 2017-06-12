namespace CTM.Core.Inputs
{
    public interface IFileInputReader
    {
        string[] ReadContent(string filePath);
    }
}