using System.Threading.Tasks;

namespace ProcessorLibrary.Interfaces;
public interface IApiAccessService
{
    Task GetTweets();
    void Stop();
}