namespace ProcessorLibrary;

public record DataItem
{
    public string Json { get; set; }
    public DataItem()
    {

    }
    public DataItem(string json)
    {
        Json = json;
    }
}
