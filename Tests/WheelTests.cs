using NUnit.Framework;
using ProcessorLibrary;
using ProcessorLibrary.Models;
using ProcessorLibrary.Services;
using System;
using System.Linq;

namespace TestProject1;

public class Tests
{
    const int BUFFER_SIZE = 16_384;
    const int BUFFER_SIZE_HEX = 0x3FFF;

    private Wheel wheel { get; set; }
    private Statistics statistics { get; set; }
    [SetUp]
    public void Setup()
    {
        statistics = new();
        wheel = new(statistics);
    }

    [Test]
    public void CanWeThrowErrorOnOverflow()
    {
        var arr = new int[BUFFER_SIZE];
        arr.ToList().ForEach(x => wheel.PutOrTake(new()));

        Assert.Throws<OverflowException>(() => wheel.PutOrTake(new()));
    }
    [Test]
    public void CanWeExtractOneDataItem()
    {
        wheel.PutOrTake(new("Json"));
        var result = wheel.PutOrTake();
        Assert.AreEqual(new DataItem("Json"), result);
    }
}