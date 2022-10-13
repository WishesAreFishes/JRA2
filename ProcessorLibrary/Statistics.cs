using System;
using System.Collections.Generic;
using System.Linq;

namespace ProcessorLibrary;
public class Statistics
{
    public int TweetCount { get; set; }
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate => DateTime.Now;
    public TimeSpan Elapsed  => EndDate - StartDate;

    public int WaitingCount { get; set; }
    public object Locker { get; set; } = new object();

    private Dictionary<string, int> HashTags = new Dictionary<string, int>();

    public void AddWheelStatus(int waitingCount)
    {
        WaitingCount = waitingCount;
    }

    /// <summary>
    /// Add a tweet's hash tags to the HashTags Dictionary and increment tweet count
    /// </summary>
    /// 
    /// This is protected by a lock, so that TweetCount and HashTags are thread safe
    /// 
    /// We might want to treat the tweet as more than a collection of hash tags at this point.
    /// However, to do so is out of scope for this project
    /// <param name="tweet"></param>
    public void AddTweet(Tweet tweet)
    {
        lock (Locker)
        {
            TweetCount++;
            tweet.HashTags.ForEach(hashTag => AddHashTag(hashTag));
        }
    }

    /// <summary>
    /// Add a hash tag's value and count to the HashTags dictionary.
    /// </summary>
    /// <param name="hashTag">The hashtag to add</param>
    private void AddHashTag(string hashTag)
    {
        if (!HashTags.ContainsKey(hashTag))
        {
            HashTags.Add(hashTag, 1);
            return;
        }
        HashTags[hashTag]++;
    }
    public double TweetsPerSecond => TweetCount / Elapsed.TotalSeconds;

    public int QueuePerformance => WaitingCount;

    /// <summary>
    /// Return the Top Ten hash tags for the entire scope of runtime
    /// </summary>
    /// It might be helpful to be able to calculate a running top ten (most recent minute, for instance),
    /// but this is out of scope for this project. Doing so would be facilitated by a backing database.
    /// <returns></returns>
    public List<string> TopTenHashTags()
    {
        var sorted = from entry 
                     in HashTags 
                     orderby entry.Value descending 
                     select entry.Key;

        return sorted.Take(10).ToList();
    }
}
