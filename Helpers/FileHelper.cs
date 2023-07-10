using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTextParser.Helpers
{
    public class FileHelper
    {
        public static IEnumerable<string> ReadFromFile(string filePath)
        {
            string line;
            using (var reader = File.OpenText(filePath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }
    }
}
