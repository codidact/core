using Codidact.Core.Domain.Extensions;
using Xunit;

namespace Domain.UnitTests.Extensions
{

    public class StringExtensionsTest
    {
        [Fact]
        public void CamelCaseBecomesSnake()
        {
            Assert.Equal("camel_case_example", "camelCaseExample".ToSnakeCase());
        }
    }
}
