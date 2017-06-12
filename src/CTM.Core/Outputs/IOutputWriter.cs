namespace CTM.Core.Outputs
{
    public interface IOutputWriter
    {
        void Write(string outputText);
        void WriteLine(string outputText);
    }
}