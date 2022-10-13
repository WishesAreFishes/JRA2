namespace ProcessorLibrary;

public interface IWheel
{
    DataItem PutOrTake(DataItem dataItem = null);
}