namespace TwitterStatistics;
using ProcessorLibrary;
using System.Threading;

/// <summary>
/// Sample Runtime Host
/// 
/// In actual production environment, the code in this file would be deployed in a more appropriate container
/// This is what I would consider to be the "Developer's Tool" while the ProcessorLibrary dll is the actual production deliverable.
/// </summary>
public partial class Form1 : Form
{
    private Statistics statistics { get; set; }
    private Wheel wheel { get; set; }
    ApiAccess apiAccess { get; set; }
    ParsingEngine parsingEngine { get; set; }

    Thread[] threads = new Thread[2];
    public Form1()
    {
        InitializeComponent();
    }

    private void btnStart_Click(object sender, EventArgs e)
    {
        btnStopCapturing.Enabled = true;
        btnStart.Enabled = false;

        statistics = new();
        wheel = new(statistics);
        apiAccess = new ApiAccess(statistics, wheel);
        parsingEngine = new ParsingEngine(statistics, wheel);
        
        timer1.Enabled = true;

        var thread = new Thread(new ThreadStart(LaunchGetTweets));
        thread.Start();
        threads[0] = thread;

        thread = new Thread(new ThreadStart(LaunchDeQueue));
        thread.Start();
        threads[1] = thread;
    }
    private void LaunchDeQueue()
    {
        parsingEngine.Dequeue();
    }

    private void LaunchGetTweets()
    {
        Task.Run(async () => await apiAccess.GetTweets());
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        txtElapsedTime.Text = statistics.Elapsed.ToString(@"d\.hh\:mm\:ss");
        txtTotalTweets.Text  = statistics.TweetCount.ToString();
        txtTweetsPerSecond.Text = statistics.TweetsPerSecond.ToString("00.0");
        txtQueuePerformance.Text = statistics.QueuePerformance.ToString();
        if (statistics.TopTenHashTags != null && statistics.TopTenHashTags().Any())
        {
            txtHashTags.Text = statistics
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
        parsingEngine?.Stop();
        apiAccess?.Stop();
    }
    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
        StopDaemons();
    }
}