using NUnit.Framework;
using ProcessorLibrary.Models;
using ProcessorLibrary.Services;
using System;
using System.Linq;

namespace TestProject1;

public class BufferingServiceTests
{
    const int BUFFER_SIZE = 16_384;
    const int BUFFER_SIZE_HEX = 0x3FFF;

    private BufferingService bufferingService { get; set; }
    private StatisticsService statistics { get; set; }
    [SetUp]
    public void Setup()
    {
        statistics = new();
        bufferingService = new(statistics);
    }

    [Test]
    public void CanWeThrowErrorOnOverflow()
    {
        var arr = new int[BUFFER_SIZE];
        arr.ToList().ForEach(x => bufferingService.PutOrTake(new()));

        Assert.Throws<OverflowException>(() => bufferingService.PutOrTake(new()));
    }
    [Test]
    public void CanWeExtractOneDataItem()
    {
        bufferingService.PutOrTake(new("Json"));
        var result = bufferingService.PutOrTake();
        Assert.AreEqual(new DataItem("Json"), result);
    }
}