using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTextParser.Data;

namespace TestTextParser.Repositories
{
    public class WordRepository
    {
        public AppDbContext Context { get; set; }

        public WordRepository()
        {
            Context = new AppDbContext();
        }

        public async Task<Word?> GetWordAsync(string word)
        {
            return await Context.Words
                .FirstOrDefaultAsync(w => w.Value == word);
        }

        public void AddWord(Word word)
        {
            Context.Words.Add(word);
        }

        /// <returns>Written to database entities</returns>
        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }

        ~WordRepository()
        {
            Context?.Dispose();
        }
    }
}
