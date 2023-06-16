namespace DotNetCensus.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
[TestCategory("UnitTest")]
public class TimingHelperTests
{
    [TestMethod]
    public void TimingHelper_ReturnsCorrectStringForOneMinute()
    {
        // Arrange
        TimeSpan timespan = TimeSpan.FromMinutes(1.5);

        // Act
        string result = TimingHelper.GetTime(timespan);

        // Assert
        Assert.AreEqual("1:30.0 mins", result);
    }

    [TestMethod]
    public void TimingHelper_ReturnsCorrectStringForOneSecond()
    {
        // Arrange
        TimeSpan timespan = TimeSpan.FromSeconds(1.55);

        // Act
        string result = TimingHelper.GetTime(timespan);

        // Assert
        Assert.AreEqual("1.550 seconds", result);
    }

    [TestMethod]
    public void TimingHelper_ReturnsCorrectStringForOneMillisecond()
    {
        // Arrange
        TimeSpan timespan = TimeSpan.FromMilliseconds(33);

        // Act
        string result = TimingHelper.GetTime(timespan);

        // Assert
        Assert.AreEqual("33 ms", result);
    }
}