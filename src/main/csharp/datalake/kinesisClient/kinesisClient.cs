using Amazon.Kinesis;
using Amazon.Kinesis.Model;
using System;
using System.IO;
using System.Threading.Tasks;
using Datalake.Schema;
using Google.Protobuf;

namespace Datalake.kinesisClient
{
    public interface IActivity
    {
        Task RecordAsync(Activity record, string partitionKey);
    }
    public class KinesisClient : IActivity
    {
        private readonly IAmazonKinesis _kinesis;
        private readonly string _streamName;
        public KinesisClient(IAmazonKinesis kinesis, string streamName)
        {
            if (string.IsNullOrEmpty(streamName))
                throw new ArgumentNullException(nameof(streamName));
            _kinesis = kinesis;
            _streamName = streamName;
        }

        public async Task RecordAsync(Activity record, string partitionKey)
        {

            var bytes = Serialize(record);
            using (var ms = new MemoryStream(bytes))
            {
                var putRecordRequest = new PutRecordRequest
                {
                    StreamName = _streamName,
                    Data = ms,
                    PartitionKey = partitionKey
                };
                await _kinesis.PutRecordAsync(putRecordRequest);
            }
        }
        internal Byte[] Serialize(Activity record)
        {
            using (var ms = new MemoryStream())
            {
                record.WriteTo(ms);
                return ms.ToArray();
            }
        }
    }
}
