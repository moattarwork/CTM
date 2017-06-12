using System;
using CTM.Core.Inputs;
using CTM.Core.Inputs.Parsing;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace CTM.Core.UnitTests.Inputs
{
    public class SessionDefinitionReaderUnitTests
    {
        [Fact]
        public void Should_ReadDefinition_ReturnTheRigthSetOfDefinitions_WhenInputFileIsCorrect()
        {
            // Given
            var sessionDefinition = new SessionDefinition("Title #1", 45);

            var inputReader = Substitute.For<IFileInputReader>();
            inputReader.ReadContent(Arg.Any<string>()).Returns(new[] {"sample"});

            var inputOptionAccessor = Substitute.For<IOptions<InputOptions>>();
            inputOptionAccessor.Value.Returns(new InputOptions());

            var parser = Substitute.For<ISessionDefinitionParser>();
            parser.Parse(Arg.Any<string>()).Returns(ParsingResult.FromResult(sessionDefinition));

            var reader = new SessionDefinitionReader(inputReader, inputOptionAccessor, new[] {parser});

            // When
            var result = reader.ReadDefinition();

            // Then
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.Contain(sessionDefinition);
        }

        [Fact]
        public void Should_ReadDefinition_ThrowProperException_WhenInputFileIsNotCorrect()
        {
            // Given
            var inputReader = Substitute.For<IFileInputReader>();
            inputReader.ReadContent(Arg.Any<string>()).Returns(new[] {"sample"});

            var inputOptionAccessor = Substitute.For<IOptions<InputOptions>>();
            inputOptionAccessor.Value.Returns(new InputOptions());

            var parser = Substitute.For<ISessionDefinitionParser>();
            parser.Parse(Arg.Any<string>()).Returns(ParsingResult.FromNoMatch());

            var reader = new SessionDefinitionReader(inputReader, inputOptionAccessor, new[] {parser});

            // When
            Action action = () => reader.ReadDefinition();

            // Then
            action.ShouldThrow<ParsingException>();
        }
    }
}