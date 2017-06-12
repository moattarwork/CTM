using System;
using System.IO;

namespace CTM.Core.Inputs
{
    public class FileInputReader : IFileInputReader
    {
        public string[] ReadContent(string filePath)
        {
            if (filePath == null) throw new ArgumentNullException(nameof(filePath));
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Error in reading input file", filePath);

            return File.ReadAllLines(filePath);
        }
    }
}