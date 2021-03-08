using System;
using FluentAssertions;
using Insight.Application.Common.Exceptions;
using Xunit;

namespace Insight.UnitTesting
{
    public class ValidationExceptionTests
    {
        [Fact]
        public void DefaultConstructorCreatesAnEmptyErrorDictionary()
        {
            var actual = new ValidationException().Errors;

            actual.Keys.Should().BeEquivalentTo(Array.Empty<string>());
        }
    }
}
