using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTextParser.Helpers;
using TestTextParser.Models;
using TestTextParser.Models.Results;

namespace TestTextParser.Services
{
    public class InputPoller
    {
        private readonly WordsService wordsService;
        private readonly int maxFileByteSize = 1024 * 1024 * 100;

        public InputPoller()
        {
            wordsService = new WordsService();
        }

        public async Task StartPolling()
        {
            while (true)
            {
                await HandleFile();
            }
        }

        private async Task HandleFile()
        {
            Console.WriteLine("Insert text file full path:");
            var filePath = Console.ReadLine();
            Console.WriteLine("Parsing in progress...");
            var stopwatch = Stopwatch.StartNew();
            try
            {
                long length = new FileInfo(filePath).Length;
                if(length > maxFileByteSize)
                {
                    Console.WriteLine($"File size is more than " +
                        $"{(double)maxFileByteSize / (1024 * 1024)} Mb");
                    return;
                }
                var fileLines = FileHelper.ReadFromFile(filePath);
                var parsingResult = await wordsService.ParseWords(fileLines);
                if (parsingResult.Succeeded)
                    Console.WriteLine($"File parsing succeeded, " +
                        $"parsed {parsingResult.AddedWordsCount} words.");
                else
                    Console.WriteLine("File parsing failed: " + parsingResult.ErrorMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine("File parsing failed: " + ex.Message);
            }
            finally 
            { 
                stopwatch.Stop();
                Console.WriteLine($"Time ellapsed: {stopwatch.ElapsedMilliseconds} ms");
            }
        }
    }
}
