using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using CsvHelper;
using CsvHelper.Configuration;
using InsuranceClaims.Models;
using InsuranceClaims.Utilities.Interfaces;

namespace InsuranceClaims.Utilities.Impls
{
    public class FileUtilities: IFileUtilities
    {
        public async Task WriteTextToFileAsync(string file, string text)
        {
            Guard.Against.NullOrEmpty(file, nameof(file));
            Guard.Against.NullOrEmpty(text, nameof(text));

            await File.WriteAllTextAsync(file, text);
        }
        public async Task<List<PaymentModel>> ReadTextFileAndTranslateToPaymentModelAsync(string file)
        {
            Guard.Against.NullOrEmpty(file, nameof(file));
            if (!File.Exists(file)) throw new FileNotFoundException(file);
            
            var config = GetCsvConfiguration();
            
            using (var reader = new StreamReader(file))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.RegisterClassMap<PaymentModelMap>();
                var result = csv.GetRecordsAsync<PaymentModel>();
                return await result.ToListAsync();
            }
        }

        private CsvConfiguration GetCsvConfiguration()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower(),
                Delimiter = ",",
                TrimOptions = TrimOptions.Trim,
                IgnoreBlankLines = true,
                BadDataFound = x =>
                {
                    Console.WriteLine($"Bad data found: <{x.RawRecord}>");
                },
                MissingFieldFound = x =>
                {
                    Console.WriteLine($"Missing Field Found: <{x.Index}>: <{string.Join(",", x.HeaderNames)}>");
                }
            };
            return config;
        }
        
        public async Task<string[]> ReadTextFileAsync(string file)
        {
            Guard.Against.NullOrEmpty(file, nameof(file));
            if (!File.Exists(file)) throw new FileNotFoundException(file);

            return await File.ReadAllLinesAsync(file);
        }
    }
}