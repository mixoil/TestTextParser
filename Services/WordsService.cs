using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestTextParser.Data;
using TestTextParser.Models.Results;
using TestTextParser.Repositories;

namespace TestTextParser.Services
{
    public class WordsService
    {
        private readonly WordRepository repository;

        public WordsService()
        {
            repository = new WordRepository();
        }

        public async Task<ParsingResult> ParseWords(IEnumerable<string> lines)
        {
            var result = new ParsingResult();
            var wordsWithMentionsCount = new Dictionary<string, int>();
            var regex = new Regex(@"[^\W\d](\w|[-'](?=\w))*");
            foreach (var line in lines)
            {
                var loweredLineWords = regex.Matches(line)
                    .Select(w => w.Value.ToLower()).ToList();
                foreach (var word in loweredLineWords)
                {
                    if (wordsWithMentionsCount.ContainsKey(word))
                        wordsWithMentionsCount[word]++;
                    else 
                        wordsWithMentionsCount[word] = 1;
                }
            }
            wordsWithMentionsCount = FiltrateWords(wordsWithMentionsCount);
            var savingResult = await SaveWordsToDatabase(wordsWithMentionsCount);
            if (!savingResult.Succeeded)
            {
                result.ErrorMessage = savingResult.ErrorMessage;
                return result;
            }
            result.AddedWordsCount = savingResult.ChangedEntities;
            result.Succeeded = true;
            return result;
        }

        private Dictionary<string, int> FiltrateWords(Dictionary<string, int> wordsWithMentionsCount)
        {
            var minWordLength = 3;
            var maxWordLength = 20;
            var minOccurences = 4;
            return wordsWithMentionsCount
                .Where(w => w.Key.Length >= minWordLength
                && w.Key.Length <= maxWordLength
                && w.Value >= minOccurences)
                .ToDictionary(w => w.Key, w => w.Value);
        }

        private async Task<SavingInDbResult> SaveWordsToDatabase(
            Dictionary<string, int> wordsWithMentionsCount)
        {
            var result = new SavingInDbResult();
            using var transaction = repository.Context.Database.BeginTransaction();
            foreach(var word in wordsWithMentionsCount)
            {
                var dbWord = await repository.GetWordAsync(word.Key);
                if (dbWord != null)
                    dbWord.Occasions += word.Value;
                else
                {
                    var newDbWord = new Word
                    {
                        Occasions = word.Value,
                        Value = word.Key
                    };
                    repository.AddWord(newDbWord);
                }
            }
            result.ChangedEntities = await repository.SaveChangesAsync();
            transaction.Commit();
            result.Succeeded = true;
            return result;
        }
    }
}
