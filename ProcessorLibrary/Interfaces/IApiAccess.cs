using System.Threading.Tasks;

namespace ProcessorLibrary.Interfaces;
public interface IApiAccess
{
    Task GetTweets();
    void Stop();
}