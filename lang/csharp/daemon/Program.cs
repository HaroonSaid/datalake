using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommandLine;

namespace RandomActivityDaemon
{
    class Options
    {
        [Option('s', "streamName", Required = true, HelpText = "Kinesis Stream Name.")]
        public string StreamName { get; set; }
        [Option('r', "region", Required = false, Default = "us-east-1", HelpText = "Kinesis region")]
        public string RegionEndpoint { get; set; }
    }
    class Program
    {
        private static Random _random = new Random();
        private static string[] stocks = new[] { "GOOG", "AMAZ", "FB", "BABA", "NFLX", "APPL", "MSFT" };
        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(opts => SendDataAsync(opts).GetAwaiter().GetResult());
        }
        private static Models.Activity GetNextActivity(string userId)
        {
            var symbol = stocks[_random.Next(0, stocks.Length)];
            var activity = new Models.Activity
            {
                UserId = userId,
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            };
            var index = _random.Next(0, 2);
            if (index == 0)
            {
                activity.ActivityType = Models.ActivityType.UserProfile;
                activity.UserInfo = new Models.UserInfo
                {
                    First = "John",
                    Last = "Doe",
                    Middle = "",
                };
            }
            if (index == 1)
            {
                activity.ActivityType = Models.ActivityType.StockTrade;
                activity.Ticket = new Models.DealTicket
                {
                    Symbol = symbol,
                    Price = "1000.00",
                    OrderType = _random.Next(0, 1) == 0 ? Models.OrderType.Buy : Models.OrderType.Sell,
                    Quantity = _random.Next(100, 10000),
                };
            }
            return activity;
        }
        private static async Task SendDataAsync(Options opts)
        {
            var userId = Guid.NewGuid().ToString();
            var amazonKinesisClient = new Amazon.Kinesis.AmazonKinesisClient(Amazon.RegionEndpoint.GetBySystemName(opts.RegionEndpoint));
            var client = new Activity.KinesisClient(amazonKinesisClient, opts.StreamName);
            while (true)
            {
                var activity = GetNextActivity(userId);
                Console.Write($"Activity:{activity.ActivityType} ");
                if (activity.ActivityType == Models.ActivityType.StockTrade){
                    Console.WriteLine($"Symbol:{activity.Ticket.Symbol} Order Type:{activity.Ticket.OrderType} Qty:{activity.Ticket.Quantity}");
                }
                else 
                    Console.WriteLine("");
                await client.RecordAsync(activity, "test");
                Thread.Sleep(TimeSpan.FromMilliseconds(500));
            }
        }
    }
}
