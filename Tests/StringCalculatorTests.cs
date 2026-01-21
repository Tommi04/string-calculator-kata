using Microsoft.Extensions.Logging;
using Moq;
using string_calculator_kata.Application.Interfaces;
using string_calculator_kata.Application.Services;
using Xunit;
using Xunit.Abstractions;

namespace string_calculator_kata;

public class StringCalculatorTests(ITestOutputHelper output)
{
    private readonly ITestOutputHelper _output = output;

    [Fact]
    public void TestSimplest()
    {
        Assert.True(true);
    }

    [Fact]
    public void TestSimplestInt()
    {
        Assert.Equal(1, 1);
    }

    [Fact]
    public void TestSimplestSum()
    {
        Assert.Equal(10, 3 + 7);
    }

    [Theory]
    [InlineData(0, "")]
    [InlineData(10, "3,7")]
    [InlineData(20, "3,7,10")]
    public void TestSum(int expectedSum, string numbersToSumSeparetedByComma)
    {
        Assert.Equal(expectedSum, GetAddService().Add(numbersToSumSeparetedByComma));
    }


    [Theory]
    [InlineData(2, "1\n1")]
    [InlineData(3, "1\n1,1")]
    [InlineData(10, "6\n3,1")]
    public void TestSumNewLine(int expectedSum, string numbersToSumSeparetedByComma)
    {
        Assert.Equal(expectedSum, GetAddService().Add(numbersToSumSeparetedByComma));
    }

    [Theory]
    [InlineData("//!")]
    public void TestDelimiter(string delimiter)
    {
        Assert.Equal('!', AddService.GetDelimiter(delimiter));
    }

    [Theory]
    [InlineData("//!6\n3,1")]
    public void TestSeparatorNewLineBetweenDelimiterAndStringNumbers(string stringNumbersToSum)
    {
        Assert.True(AddService.GetStringNumbersBySeparator('\n', stringNumbersToSum).Length > 1);
    }

    [Theory]
    [InlineData(10, "//,\n6,3,1")]
    [InlineData(10, "//!\n6!3!1")]
    public void TestSumNewDelimiterNewLine(int expectedSum, string stringNumbersToSum)
    {
        Assert.Equal(expectedSum, GetAddService().Add(stringNumbersToSum));
    }

    [Theory]
    [InlineData("6,5,-1, -3")]
    public void TestSumNegativeNumber(string stringNumbersToSum)
    {
        try
        {
            GetAddService().Add(stringNumbersToSum);
            Assert.True(false);
        }
        catch (Exception ex)
        {
            _output.WriteLine(ex.Message);
            Assert.True(true);
        }
    }

    [Theory]
    [InlineData(3, "3,7000")]
    [InlineData(10, "3,7,1001")]
    public void TestSumBigNumbers(int expectedSum, string numbersToSumSeparetedByComma)
    {
        Assert.Equal(expectedSum, GetAddService().Add(numbersToSumSeparetedByComma));
    }

    private AddService GetAddService(ILogger? logger = null, IWebservice? webService = null)
    {
        if (logger == null)
        {
            logger = GetMockLoggerObject();
        }
        if (webService == null)
        {
            webService = GetMockWebService().Object;
        }
        return new AddService(logger, webService);
    }

    private ILogger GetMockLoggerObject()
    {
        return new Mock<ILogger>().Object;
    }

    private Mock<IWebservice> GetMockWebService()
    {
        return new Mock<IWebservice>();
    }

    [Theory]
    [InlineData("1,2,3")]
    public void TestLoggerExceptionNotifiesWebService(string numbersToSumSeparetedByComma)
    {
        var mockWebService = GetMockWebService();
        var stubLogger = new Mock<ILogger>();
        string exceptionMessage = "Logger failed";

        stubLogger.Setup(x => x.Log(
            It.IsAny<LogLevel>(),
            It.IsAny<EventId>(),
            It.IsAny<It.IsAnyType>(),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()))
            .Throws(new Exception(exceptionMessage));

        int? result = GetAddService(stubLogger.Object, mockWebService.Object).Add(numbersToSumSeparetedByComma);
        mockWebService.Verify(w => w.NotifyLoggingFailure(exceptionMessage), Times.Once);
    }
}