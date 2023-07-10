using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTextParser.Data
{
    public class Word
    {
        [Key]
        public string Value { get; set; }
        public int Occasions { get; set; }
    }
}
