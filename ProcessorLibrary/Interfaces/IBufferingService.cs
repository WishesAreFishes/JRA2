using ProcessorLibrary.Models;

namespace ProcessorLibrary.Interfaces;

public interface IBufferingService
{
    DataItem PutOrTake(DataItem dataItem = null);

    void Reset();
}