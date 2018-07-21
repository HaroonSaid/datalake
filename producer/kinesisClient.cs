using Amazon.Kinesis;
using Amazon.Kinesis.Model;
using Avro.IO;
using Avro.Specific;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Activity
{
    public interface IActivity
    {
        Task RecordAsync(Models.Activity record, string partitionKey);
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

        public async Task RecordAsync(Models.Activity record, string partitionKey)
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
        internal Byte[] Serialize(Models.Activity record)
        {
            using (var ms = new MemoryStream())
            {
                var encoder = new BinaryEncoder(ms);
                var writer = new SpecificDatumWriter<Models.Activity>(record.Schema);
                writer.Write(record, encoder);
                return ms.ToArray();
            }
        }
    }
}
