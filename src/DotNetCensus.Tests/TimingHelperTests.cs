using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class TimingHelperTests
{
    [TestMethod]
    public void TimingHelper_ReturnsCorrectStringForOneMinute()
    {
        // Arrange
        TimeSpan timespan = TimeSpan.FromMinutes(1.5);

        // Act
        string result = TimingHelper.FormatTime(timespan);

        // Assert
        Assert.AreEqual("1:30.0 mins", result);
    }

    [TestMethod]
    public void TimingHelper_ReturnsCorrectStringForOneSecond()
    {
        // Arrange
        TimeSpan timespan = TimeSpan.FromSeconds(1.5);

        // Act
        string result = TimingHelper.FormatTime(timespan);

        // Assert
        Assert.AreEqual("1.0 seconds", result);
    }

    [TestMethod]
    public void TimingHelper_ReturnsCorrectStringForOneMillisecond()
    {
        // Arrange
        TimeSpan timespan = TimeSpan.FromMilliseconds(1.5);

        // Act
        string result = TimingHelper.FormatTime(timespan);

        // Assert
        Assert.AreEqual("1 ms", result);
    }
}