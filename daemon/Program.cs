using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RandomActivityDaemon
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var streamName = args[0];
            var amazonKinesisClient = new Amazon.Kinesis.AmazonKinesisClient(Amazon.RegionEndpoint.USEast1);
            var client = new Activity.KinesisClient(amazonKinesisClient, streamName);
            while (true)
            {
                var activity = GetNextActivity();
                if (activity != null)
                {
                    await client.RecordAsync(activity, "test");
                    Debug.WriteLine($"Activity:{activity.ActivityType}");
                }
                Thread.Sleep(TimeSpan.FromMilliseconds(500));
            }
        }
        private static Models.Activity GetNextActivity()
        {
            return new Models.Activity();
        }
    }
}
