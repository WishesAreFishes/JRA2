using System.Collections.Generic;

namespace ProcessorLibrary;

public class Tweet
{
    public Tweet(List<string> hashTags)
    {
        HashTags = hashTags;
    }

    public List<string> HashTags { get; set; }
}
