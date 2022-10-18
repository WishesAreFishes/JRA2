using ProcessorLibrary.Interfaces;
using ProcessorLibrary.Models;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace ProcessorLibrary.Services;
/// <summary>
/// Parsing Service extracts meaning from a tweet, storing its result in Statistics
/// This Service is meant to run in its own thread, to decouple it from the Api Access Service,
/// which also runs in its own thread.
/// 
/// It may appear unnecessary to run this service in its own thread,
/// however, in antisipation of other activities that might be required later,
/// this architecture is easily extensible.
/// 
/// For instance, there may be a need to reach out to the API via another
/// API service class to receive more information about the source and sink
/// of the tweet.
/// 
/// Also, it may be useful to limit the language of hashtags to English.
/// 
/// Any such processessing might need to also be done in separate threads,
/// However, storing the tweet data in a database for offline processing by
/// other services may prove more effective.
/// </summary>
public class ParsingService : IParsingService
{
    private IStatisticsService _statistics;
    private IBufferingService _bufferingService;
    bool _running = false;

    public ParsingService(IStatisticsService statistics, IBufferingService bufferingService)
    {
        _statistics = statistics;
        _bufferingService = bufferingService;
    }

    /// <summary>
    /// Remove items from Queue as soon as they are available
    /// </summary>
    public void Dequeue()
    {
        _running = true;
        while (_running)
        {
            var dataItem = _bufferingService.PutOrTake();
            if (dataItem == null)
            {
                System.Threading.Thread.Sleep(10);
            }
            else
            {
                var hashTags = Parse(dataItem);
                _statistics.AddTweet(new(hashTags));
            }
        }
    }

    /// <summary>
    /// Parse the data item, extracting what we care about
    /// </summary>
    /// <param name="dataItem">The data item containing interesting Json</param>
    /// In another realization of this method, we might look for other data.
    /// <returns>List of hash tags in the tweet</returns>
    private List<string> Parse(DataItem dataItem)
    {
        var rtn = new List<string>();
        if (dataItem.Json is not null && dataItem.Json.Contains('#'))
        {
            var node = JsonNode.Parse(dataItem.Json);
            var data = node!["data"];
            var text = data!["text"];

            var reg = new Regex("#(\\w+)");

            var matches = reg.Matches(text.ToString());
            foreach (var match in matches)
            {
                rtn.Add(match.ToString());
            }
        }

        return rtn;
    }

    /// <summary>
    /// Allows our consumer to stop this service
    /// </summary>
    public void Stop()
    {
        _running = false;
    }
}


