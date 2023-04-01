using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace WordFrequency
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "tolstoj_lew_nikolaewich-text_0040 (1).txt";
            string text = "";

            // Read input text file
            using (StreamReader reader = new StreamReader(filePath))
            {
                text = reader.ReadToEnd();
            }

            // Extract words from text
            string pattern = @"\p{L}+"; // Matches any Unicode letter
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(text);

            // Count frequency of each word
            var wordCounts = matches.Cast<Match>()
                                    .GroupBy(m => m.Value.ToLower())
                                    .ToDictionary(g => g.Key, g => g.Count());

            // Sort by descending frequency
            var sortedWordCounts = wordCounts.OrderByDescending(w => w.Value);

            // Write results to file
            using (StreamWriter writer = new StreamWriter("word-frequency.txt"))
            {
                foreach (var wordCount in sortedWordCounts)
                {
                    writer.WriteLine($"{wordCount.Key} {wordCount.Value}");
                }
            }

            Console.WriteLine("Done! Results written to file.");
        }
    }
}
