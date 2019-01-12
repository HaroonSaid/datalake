using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Kinesis;
using CommandLine;
using Datalake.kinesisClient;
using Datalake.Schema;

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
        static async Task Main(string[] args)
        {
            var result = CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(async opts => await SendDataAsync(opts));
        }
        private static Activity GetNextActivity(string userId)
        {
            var symbol = stocks[_random.Next(0, stocks.Length)];
            var activity = new Activity
            {
                UserId = userId,
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            };
            var index = _random.Next(0, 2);
            if (index == 0)
            {
                activity.ActivityType = Activity.Types.ActivityType.User;
                activity.UserActivity = new UserActivity
                {
                    UserActivityType = _random.Next(0, 1) == 0 ?
                        UserActivity.Types.UserActivityType.Created :
                        UserActivity.Types.UserActivityType.Deactiavted,
                    UserId = userId
                };
            }
            if (index == 1)
            {
                activity.ActivityType = Activity.Types.ActivityType.Account;
                activity.AccountActivity = new AccountActivity
                {
                    AccountActivityType = _random.Next(0, 1) == 0 ?
                        AccountActivity.Types.AccountActivityType.Created :
                        AccountActivity.Types.AccountActivityType.Archived,
                    AccountType = GetRandomAccountActivityType()
                };
            }
            return activity;
        }
        private static AccountActivity.Types.AccountType GetRandomAccountActivityType()
        {
            var enums = Enum.GetValues(typeof(AccountActivity.Types.AccountType));
            return (AccountActivity.Types.AccountType)_random.Next(0, enums.Length-1);
        }
        private static async Task SendDataAsync(Options opts)
        {
            var userId = Guid.NewGuid().ToString();
            var amazonKinesisClient = new AmazonKinesisClient(Amazon.RegionEndpoint.GetBySystemName(opts.RegionEndpoint));
            var client = new KinesisClient(amazonKinesisClient, opts.StreamName);
            while (true)
            {
                var activity = GetNextActivity(userId);
                Console.Write($"Activity:{activity.ActivityType} ");
                await client.RecordAsync(activity, "test");
                Thread.Sleep(TimeSpan.FromMilliseconds(500));
            }
        }
    }
}
