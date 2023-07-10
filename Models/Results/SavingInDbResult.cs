using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTextParser.Models.Results
{
    public class SavingInDbResult : OperationResult
    {
        public int ChangedEntities { get; set; }
    }
}
