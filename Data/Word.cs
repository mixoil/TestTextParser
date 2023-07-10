using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTextParser.Data
{
    public class Word
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public int Occasions { get; set; }
    }
}
