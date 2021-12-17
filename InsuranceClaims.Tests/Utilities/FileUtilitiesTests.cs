using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using InsuranceClaims.Models;
using InsuranceClaims.Utilities.Impls;
using InsuranceClaims.Utilities.Interfaces;
using Newtonsoft.Json;
using Xunit;

namespace InsuranceClaims.Tests.Utilities
{
    public class FileUtilitiesTests
    {
        private readonly IFileUtilities _fileUtilities;

        public FileUtilitiesTests()
        {
            _fileUtilities = new FileUtilities();
        }

        private const string FileAddress = "Files/input.txt"; 
        [Fact]
        public async Task ReadTextFileAndTranslateToPaymentModelAsync_Should_ReadTextFileAndTranslateToPaymentModel_When_FindFile()
        {
            List<PaymentModel> paymentModels = await _fileUtilities.ReadTextFileAndTranslateToPaymentModelAsync(FileAddress);

            var expected = GetPaymentModelExpectedData();
            
            paymentModels.Should().BeEquivalentTo(expected);
        }
        [Fact]
        public async Task ReadTextFileAndTranslateToPaymentModelAsync_Should_ThrowArgumentNullException_When_FileIsNullOrEmpty()
        {
            Func<Task> readTextFileEmpty = async () => { await _fileUtilities.ReadTextFileAndTranslateToPaymentModelAsync(""); };
            Func<Task> readTextFileNull = async () => { await _fileUtilities.ReadTextFileAndTranslateToPaymentModelAsync(null); };
            await readTextFileEmpty.Should().ThrowAsync<ArgumentException>();
            await readTextFileNull.Should().ThrowAsync<ArgumentNullException>();
        }
        
        [Fact]
        public async Task ReadTextFileAndTranslateToPaymentModelAsync_Should_ThrowFileNotFoundException_When_FileAddressIsWrong()
        {
            Func<Task> readTextFileEmpty = async () => { await _fileUtilities.ReadTextFileAndTranslateToPaymentModelAsync("wrongAddressFile.txt"); };
            await readTextFileEmpty.Should().ThrowAsync<FileNotFoundException>();
        }
        [Fact]
        public async Task ReadTextFileAsync_Should_ReadAllData_When_FindFile()
        {
            string[] inputText = await _fileUtilities.ReadTextFileAsync(FileAddress);

            var expected = GetExpectedData();
            
            inputText.Should().BeEquivalentTo(expected);
        }
        [Fact]
        public async Task ReadTextFileAsync_Should_ThrowArgumentNullException_When_FileIsNullOrEmpty()
        {
            Func<Task> readTextFileEmpty = async () => { await _fileUtilities.ReadTextFileAsync(""); };
            Func<Task> readTextFileNull = async () => { await _fileUtilities.ReadTextFileAsync(null); };
            await readTextFileEmpty.Should().ThrowAsync<ArgumentException>();
            await readTextFileNull.Should().ThrowAsync<ArgumentNullException>();
        }
        
        [Fact]
        public async Task ReadTextFileAsync_Should_ThrowFileNotFoundException_When_FileAddressIsWrong()
        {
            Func<Task> readTextFileEmpty = async () => { await _fileUtilities.ReadTextFileAsync("wrongAddressFile.txt"); };
            await readTextFileEmpty.Should().ThrowAsync<FileNotFoundException>();
        }

        private static string[] GetExpectedData()
        {
            string[] expected = {
                "Product, Origin Year, Development Year, Incremental Value",
                "Comp, 1992, 1992, 110.0",
                "Comp, 1992, 1993, 170.0",
                "Comp, 1993, 1993, 200.0",
                "Non-Comp, 1990, 1990, 45.2",
                "Non-Comp, 1990, 1991, 64.8",
                "Non-Comp, 1990, 1993, 37.0",
                "Non-Comp, 1991, 1991, 50.0",
                "Non-Comp, 1991, 1992, 75.0",
                "Non-Comp, 1991, 1993, 25.0",
                "Non-Comp, 1992, 1992, 55.0",
                "Non-Comp, 1992, 1993, 85.0",
                "Non-Comp, 1993, 1993, 100.0"
            };
            return expected;
        }
        private static List<PaymentModel> GetPaymentModelExpectedData()
        {
            string json = File.ReadAllText("Files/PaymentModelExpectedData.json");
            return JsonConvert.DeserializeObject<List<PaymentModel>>(json);
        }
    }
}