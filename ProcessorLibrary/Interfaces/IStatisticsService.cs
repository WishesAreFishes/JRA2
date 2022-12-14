using ProcessorLibrary.Models;
using System;
using System.Collections.Generic;

namespace ProcessorLibrary.Interfaces;
public interface IStatisticsService
{
    TimeSpan Elapsed { get; }
    DateTime EndDate { get; }
    object Locker { get; set; }
    int QueuePerformance { get; }
    DateTime StartDate { get; set; }
    int TweetCount { get; set; }
    double TweetsPerSecond { get; }
    int WaitingCount { get; set; }

    void AddTweet(Tweet tweet);
    void AddBufferingServiceStatus(int waitingCount);
    List<string> TopTenHashTags();
    void Reset();
}