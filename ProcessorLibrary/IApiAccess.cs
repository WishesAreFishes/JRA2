using System.Threading.Tasks;

namespace ProcessorLibrary;
public interface IApiAccess
{
    Task GetTweets();
    void Stop();
}