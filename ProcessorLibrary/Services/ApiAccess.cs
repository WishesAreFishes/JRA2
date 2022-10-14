using ProcessorLibrary.Interfaces;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessorLibrary.Services;
public class ApiAccess : IApiAccess
{


    private IWheel _wheel { get; set; }
    private bool _running = false;
    public ApiAccess(IWheel wheel)
    {
        _wheel = wheel;
    }
    private int backoffDelayInMilliseconds { get; set; }

    private int retryCount = 0;
    private HttpClient myClient;

    /// <summary>
    /// Interface for GetTweetStream
    /// </summary>
    /// Allows us to retry in case of exception in GetTweetStream
    /// <returns></returns>
    public async Task GetTweets()
    {
        _running = true;

        while (_running)
        {
            try
            {
                await GetTweetStream();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Waiting: 1 second to retry");
                Thread.Sleep(1000);
            }
        }

    }

    /// <summary>
    /// Get stream of tweets
    /// </summary>
    /// We initialize and consume the HttpClient here, so on retry we will launch an entirely new HttpClient hierarchy
    /// <returns></returns>
    private async Task GetTweetStream()
    {
        var key = Environment.GetEnvironmentVariable("TwitterApiKey");

        myClient = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
        myClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", key);

        while (_running)
        {
            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Get, "https://api.twitter.com/2/tweets/sample/stream");
                var response = await myClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();
                    using var reader = new StreamReader(responseStream);
                    string line = null;
                    while ((line = await reader.ReadLineAsync()) != null && _running)
                    {
                        _wheel.PutOrTake(new(line));
                        Console.WriteLine($"Received tweet: {line}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                CloseStream();
                throw;
            }
        }
        CloseStream();
    }

    /// <summary>
    /// Ensure myClient is disposed before we leave
    /// </summary>
    private void CloseStream()
    {
        myClient.CancelPendingRequests();
        myClient.Dispose();
    }
    /// <summary>
    /// Allows our consumer to stop this service
    /// </summary>
    public void Stop()
    {
        _running = false;
    }
}
