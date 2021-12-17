using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using InsuranceClaims.Utilities.Impls;
using InsuranceClaims.Utilities.Interfaces;
using Xunit;

namespace InsuranceClaims.Tests
{
    public class InsuranceTests
    {
        private readonly IInsurance _insurance;
        private readonly IFileUtilities _fileUtilities;

        public InsuranceTests()
        {
            _fileUtilities = new FileUtilities();
            _insurance = new Insurance(_fileUtilities, new MatrixUtilities());
        }

        [Fact]
        public async Task GenerateResultFromInputFile_Should_GenerateDesiredStringFromInputFile()
        {
            string output = await _insurance.GenerateResultFromInputFile("Files/input.txt");

            string expected = String.Join(Environment.NewLine, await _fileUtilities.ReadTextFileAsync("Files/ExpectedOutput.txt"));

            output.Trim().Should().Be(expected.Trim());
        }
        [Fact]
        public async Task GenerateResultFromInputFile_Should_ThrowArgumentNullException_When_FileIsNullOrEmpty()
        {
            Func<Task> generateResultFromInputFileEmpty = async () => { await _insurance.GenerateResultFromInputFile(""); };
            Func<Task> generateResultFromInputFileNull = async () => { await _insurance.GenerateResultFromInputFile(null); };
            await generateResultFromInputFileEmpty.Should().ThrowAsync<ArgumentException>();
            await generateResultFromInputFileNull.Should().ThrowAsync<ArgumentNullException>();
        }
       
    }
}