using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.KinesisFirehoseEvents;
using Avro.IO;
using Avro.Specific;
using Newtonsoft.Json;
using static Amazon.Lambda.KinesisFirehoseEvents.KinesisFirehoseResponse;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace LambdaFirehose
{
    public class Functions
    {
        /// <summary>
        /// Default constructor that Lambda will invoke.
        /// </summary>
        public Functions()
        {
        }
        public KinesisFirehoseResponse ProcessStreamAsync(KinesisFirehoseEvent kinesisFireHoseEvent, ILambdaContext context)
        {

            context.Logger.LogLine($"{context.AwsRequestId} {nameof(ProcessStreamAsync)}");
            var records = kinesisFireHoseEvent.Records.Select(p => ProcessActivityRecord(p, context.Logger));
            var response = new KinesisFirehoseResponse
            {
                Records = records.ToList()
            };

            return response;
        }
        internal FirehoseRecord ProcessActivityRecord(KinesisFirehoseEvent.FirehoseRecord record, ILambdaLogger logger)
        {
            var firehoseRecord = new FirehoseRecord
            {
                RecordId = record.RecordId,
                Result = TRANSFORMED_STATE_OK,
            };
            var bytes = Convert.FromBase64String(record.Base64EncodedData);
            var activity = Deserialize(bytes);
            var str = JsonConvert.SerializeObject(activity);
            logger.LogLine(str);
            firehoseRecord.Base64EncodedData = Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
            return firehoseRecord;
        }

        internal Models.Activity Deserialize(Byte[] bytes)
        {
            var writerSchema = Models.Activity._SCHEMA;
            using (var ms = new MemoryStream(bytes.ToArray()))
            {
                var decoder = new BinaryDecoder(ms);
                // Use the correct schema to read payload
                var reader = new SpecificDatumReader<Models.Activity>(writerSchema, Models.Activity._SCHEMA);
                return reader.Read(null, decoder);
            }
        }

    }
}

