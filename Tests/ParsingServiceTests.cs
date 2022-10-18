using Moq;
using NUnit.Framework;
using ProcessorLibrary.Interfaces;
using ProcessorLibrary.Models;
using ProcessorLibrary.Services;
using System;
using System.Threading;

namespace TestProject1;

public class ParsingServiceTests
{
    private IParsingService _parsingService { get; set; }
    private IStatisticsService _statistics { get; set; }
    private IBufferingService _bufferingService { get; set; }
    
    public void Setup(Mode mode)
    {
        _statistics = new StatisticsService();

        var bufferingMock = new Mock<IBufferingService>();

        switch (mode)
        {
            case Mode.NULL_JSON:
                {
                    bufferingMock.Setup(x => x.PutOrTake(null)).Returns(new DataItem(null));
                    break;
                }

            case Mode.EMPTY_JSON:
                {
                    bufferingMock.Setup(x => x.PutOrTake(null)).Returns(new DataItem(""));
                    break;
                }
            case Mode.NO_DATA_ELEMENT:
                bufferingMock.Setup(x => x.PutOrTake(null)).Returns(new DataItem("{ \"sample\": [ \"this#\" : null]}"));
                break;
            case Mode.NO_TEXT_ELEMENT:
                bufferingMock.Setup(x => x.PutOrTake(null)).Returns(new DataItem("{ \"data\": [ \"this#\" : null]}"));
                break;
            case Mode.NO_HASH_TAGS_EXIST:
                bufferingMock.Setup(x => x.PutOrTake(null)).Returns(new DataItem("{\"data\":{\"text\":\"\",\"other\":\"#\"}}"));
                break;
            case Mode.ONE_HASH_TAG_EXISTS:
                bufferingMock.Setup(x => x.PutOrTake(null)).Returns(new DataItem("{\"data\":{\"text\":\"#one\",\"other\":\"#\"}}"));
                break;
            case Mode.TWO_HASH_TAGS_EXIST:
                bufferingMock.Setup(x => x.PutOrTake(null)).Returns(new DataItem("{\"data\":{\"text\":\"#one#two\",\"other\":\"#\"}}"));
                break;

        }
        _bufferingService = bufferingMock.Object;

        _parsingService = new ParsingService(_statistics, _bufferingService);

        var thread = new Thread(new ThreadStart(LaunchDeQueue));
        thread.Start();
        Thread.Sleep(300);
        _parsingService.Stop();

    }
    [Test]
    public void DoesNullJsonReturnEmptyList()
    {
        Setup(Mode.NULL_JSON);

        var hashTags = _statistics.TopTenHashTags();
        Assert.IsEmpty(hashTags);
    }
    [Test]
    public void DoesEmptyJsonReturnEmptyList()
    {
        Setup(Mode.EMPTY_JSON);

        var hashTags = _statistics.TopTenHashTags();
        Assert.IsEmpty(hashTags);
    }
    [Test]
    public void DoesJsonReturnEmptyListIfNoDataElement()
    {
        Setup(Mode.NO_DATA_ELEMENT);

        var hashTags = _statistics.TopTenHashTags();
        Assert.IsEmpty(hashTags);
    }
    [Test]
    public void DoesJsonReturnEmptyListIfNoTextElement()
    {
        Setup(Mode.NO_TEXT_ELEMENT);

        var hashTags = _statistics.TopTenHashTags();
        Assert.IsEmpty(hashTags);
    }
    [Test]
    public void DoesJsonReturnEmptyListIfNoHashTagsExist()
    {
        Setup(Mode.NO_HASH_TAGS_EXIST);

        var hashTags = _statistics.TopTenHashTags();
        Assert.IsEmpty(hashTags);
    }
    [Test]
    public void DoesJsonReturnOneHashTagIfOneHashTagExists()
    {
        Setup(Mode.ONE_HASH_TAG_EXISTS);

        var hashTags = _statistics.TopTenHashTags();
        Assert.That(hashTags, Has.Count.EqualTo(1));
    }
    [Test]
    public void DoesJsonReturnTwoHashTagsIfTwoHashTagExist()
    {
        Setup(Mode.TWO_HASH_TAGS_EXIST);

        var hashTags = _statistics.TopTenHashTags();
        Assert.That(hashTags, Has.Count.EqualTo(2));
    }
    private void LaunchDeQueue()
    {
        _parsingService.Dequeue();
    }
}



[Flags]
public enum Mode
{
    NULL_JSON = 1,
    EMPTY_JSON = 2,
    NO_DATA_ELEMENT = 4,
    NO_TEXT_ELEMENT = 8,
    NO_HASH_TAGS_EXIST = 16,
    ONE_HASH_TAG_EXISTS = 32,
    TWO_HASH_TAGS_EXIST = 64,
}