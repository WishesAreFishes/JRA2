using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace ProcessorLibrary;
public class ParsingEngine
{
    private Statistics _statistics;
    private Wheel _wheel;
    bool _running = false;

    public ParsingEngine(Statistics statistics, Wheel wheel)
    {
        _statistics = statistics;
        _wheel = wheel;
    }

    /// <summary>
    /// Remove items from Queue as soon as they are available
    /// </summary>
    public void Dequeue()
    {
        _running = true;
        while (_running)
        {
            var dataItem = _wheel.PutOrTake();
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
        if (dataItem.Json.Contains("#"))
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


