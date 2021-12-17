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
    public class MatrixUtilitiesTests
    {
        private readonly IMatrixUtilities _matrixUtilities;

        public MatrixUtilitiesTests()
        {
            _matrixUtilities = new MatrixUtilities();
        }
        
        [Fact]
        public void Print2DArray_Should_ThrowArgumentNullException_When_InputMatrixIsEmpty()
        {
            Action print2dArrayAction = () => { _matrixUtilities.Print2DArray<double[,]>(null); };
            
            print2dArrayAction.Should().Throw<ArgumentNullException>();
        }
    }
}