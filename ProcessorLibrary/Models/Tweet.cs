using System.Collections.Generic;

namespace ProcessorLibrary.Models;

public class Tweet
{
    public Tweet(List<string> hashTags)
    {
        HashTags = hashTags;
    }

    public List<string> HashTags { get; set; }
}
