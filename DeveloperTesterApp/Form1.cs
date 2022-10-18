namespace TwitterStatistics;
using ProcessorLibrary.Interfaces;
using System.Threading;

/// <summary>
/// Sample Runtime Host
/// 
/// In actual production environment, the code in this file would be deployed in a more appropriate container
/// This is what I would consider to be the "Developer's Tool" while the ProcessorLibrary dll is the actual production deliverable.
/// </summary>
public partial class Form1 : Form
{
    IStatisticsService _statistics { get; set; }
    IBufferingService _bufferingService { get; set; }
    IApiAccessService _apiAccess { get; set; }
    IParsingService _parsingEngine { get; set; }

    Thread[] threads = new Thread[2];
    public Form1(IBufferingService bufferingService, IStatisticsService statistics, IApiAccessService apiAccess, IParsingService parsingEngine)
    {
        InitializeComponent();
        _bufferingService = bufferingService;
        _statistics = statistics;
        _apiAccess = apiAccess;
        _parsingEngine = parsingEngine;
    }

    private void btnStart_Click(object sender, EventArgs e)
    {
        btnStopCapturing.Enabled = true;
        btnStart.Enabled = false;

        timer1.Enabled = true;

        _statistics.Reset();
        _bufferingService.Reset();
        DisplayStatistics();

        var thread = new Thread(new ThreadStart(LaunchGetTweets));
        thread.Start();
        threads[0] = thread;

        thread = new Thread(new ThreadStart(LaunchDeQueue));
        thread.Start();
        threads[1] = thread;
    }
    private void LaunchDeQueue()
    {
        _parsingEngine.Dequeue();
    }

    private void LaunchGetTweets()
    {
        Task.Run(async () => await _apiAccess.GetTweets());
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        DisplayStatistics();
    }

    private void DisplayStatistics()
    {
        txtElapsedTime.Text = _statistics.Elapsed.ToString(@"d\.hh\:mm\:ss");
        txtTotalTweets.Text = _statistics.TweetCount.ToString();
        txtTweetsPerSecond.Text = _statistics.TweetsPerSecond.ToString("00.0");
        txtQueuePerformance.Text = _statistics.QueuePerformance.ToString();
        txtHashTags.Text = "";
        if (_statistics.TopTenHashTags != null && _statistics.TopTenHashTags().Any())
        {
            txtHashTags.Text = _statistics
                .TopTenHashTags()
                .Aggregate((x, y) => x + "\r\n" + y);
        }
    }

    private void btnStopCapturing_Click(object sender, EventArgs e)
    {
        StopDaemons();
        btnStopCapturing.Enabled = false;
        btnStart.Enabled = true;
    }

    private void StopDaemons()
    {
        timer1.Enabled = false;
        _parsingEngine?.Stop();
        _apiAccess?.Stop();
    }
    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
        StopDaemons();
    }
}