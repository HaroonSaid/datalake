package datalake;

import java.net.InetAddress;
import java.nio.ByteBuffer;
import java.util.UUID;

import com.amazonaws.AmazonClientException;
import com.amazonaws.auth.AWSCredentials;
import com.amazonaws.auth.profile.ProfileCredentialsProvider;
import com.amazonaws.services.kinesis.AmazonKinesis;
import com.amazonaws.services.kinesis.AmazonKinesisClientBuilder;
import com.amazonaws.services.kinesis.model.PutRecordRequest;
import com.amazonaws.services.kinesis.model.PutRecordResult;
import com.amazonaws.services.kinesis.model.ResourceNotFoundException;
import org.apache.avro.Schema;
import org.apache.avro.specific.*;
import org.apache.avro.io.*;
import org.apache.avro.util.Utf8;
import org.joda.time.DateTime;

import java.io.ByteArrayOutputStream;
import java.io.IOException;


public class KinesisClient {
    private final AmazonKinesis _kinesis;
    private final String _streamName;

    public KinesisClient(AmazonKinesis kinesis, String streamName) {
        _kinesis = kinesis;
        _streamName = streamName;
    }

    public void recordAsync(Models.Activity record, String partitionKey) {
        PutRecordRequest putRecordRequest = new PutRecordRequest();
        putRecordRequest.setStreamName(_streamName);
        
        putRecordRequest.setData(ByteBuffer.wrap(serialize(record)));
        
		putRecordRequest.setPartitionKey("partitionKey-1");
        PutRecordResult putRecordResult = _kinesis.putRecord(putRecordRequest);
    }

    private byte[] serialize(Models.Activity record)  {
        ByteArrayOutputStream out = new ByteArrayOutputStream();
        SpecificDatumWriter<Models.Activity> writer = new SpecificDatumWriter<Models.Activity>(record.getSchema());
        Encoder encoder = EncoderFactory.get().binaryEncoder(out, null);
        writer.write(record, encoder);
        encoder.flush();
        out.close();
        return out.toByteArray();
    }
}