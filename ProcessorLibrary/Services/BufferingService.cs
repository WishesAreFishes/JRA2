using ProcessorLibrary.Interfaces;
using ProcessorLibrary.Models;
using System;

namespace ProcessorLibrary.Services;

/// <summary>
/// The bufferingService is a circular non-linked Queue of data items operating as a read-write buffer
/// It is implemented with UInt64 pointers which provides a numerical space equivalent to about a hundred million years of tweets
/// But we modulo those pointers to provide support for 16384 buckets (2^14) which should be sufficient to allow a burst of tweets
/// up to 16384 in any given second,
/// 
/// Our experience is that the heaviest burst required two concurrent bucket elements. So this Queue seems sufficient to handle the full
/// Twitter tweet feed of 5700 tweets per second.
/// 
/// </summary>
public class BufferingService : IBufferingService
{

    const int BUFFER_SIZE = 16_384;
    const int BUFFER_SIZE_HEX = 0x3FFF;

    private ulong WritePtr { get; set; }
    private ulong ReadPtr { get; set; }
    private DataItem[] DataItems { get; set; }
    private IStatisticsService _statistics { get; set; }


    public object Locker { get; set; }
    public BufferingService(IStatisticsService statistics)
    {
        Reset();
        DataItems = new DataItem[BUFFER_SIZE];
        Locker = new object();
        _statistics = statistics;
    }

    /// <summary>
    /// Return modulo 16284 of the ulong provided
    /// </summary>
    /// <param name="large">ulong value provided</param>
    /// We presume doing the modulo with bit math is as fast as we can get
    /// <returns></returns>
    private static short BucketPointer(ulong large) => (short)(large & BUFFER_SIZE_HEX);

    /// <summary>
    /// Return the next readable data item
    /// </summary>
    /// We only want to return a DataItem if WritePtr > ReadPtr (i.e. a new DataItem has been written that has not been read)
    /// We must also mark the item as having been read, by storing null.
    /// This is necessary, because a non-null DataItem at Put time should
    /// throw an Overflow Exception
    /// <returns>The next DataItem</returns>
    private DataItem Take()
    {

        if (WritePtr <= ReadPtr)
        {
            return null;

        }
        _statistics.AddBufferingServiceStatus((int)(WritePtr - ReadPtr));
        var ptr = BucketPointer(++ReadPtr);
        var itemToReturn = DataItems[ptr];
        DataItems[ptr] = null;
        return itemToReturn;
    }

    /// <summary>
    /// Store the DataItem in the next available Write bucket
    /// </summary>
    /// 
    /// <param name="dataItem">The DataItem to store</param>
    /// <exception cref="OverflowException"></exception>
    private void Put(DataItem dataItem)
    {
        var ptr = BucketPointer(++WritePtr);
        if (DataItems[ptr] is not null)
        {
            throw new OverflowException("Exceeded buffer capacity");
        }
        _statistics.AddBufferingServiceStatus((int)(WritePtr - ReadPtr));

        DataItems[ptr] = dataItem;
    }

    /// <summary>
    /// PutOrTake is implemented as a single method so that we can ensure thread safety for
    /// the incrementing of pointers and placing/retrieving data items
    /// </summary>
    /// <param name="dataItem">The DataItem to Put, if this is a Put operation</param>
    /// <returns></returns>
    public DataItem PutOrTake(DataItem dataItem = null)
    {
        // Ensure thread safety for queue access
        lock (Locker)
        {
            // Null dataItem means this is a Take operation
            // we will return the next available DataItem
            // or null if no DataItem is available
            if (dataItem is null)
            {
                return Take();
            }

            // non-null dataItem means this is a Put operation
            // we will store the dataItem in the next available Bucket location
            Put(dataItem);
            return null;
        }
    }

    public void Reset()
    {
        WritePtr = ReadPtr = 0;
    }
}