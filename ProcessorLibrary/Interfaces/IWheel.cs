using ProcessorLibrary.Models;

namespace ProcessorLibrary.Interfaces;

public interface IWheel
{
    DataItem PutOrTake(DataItem dataItem = null);

    void Reset();
}