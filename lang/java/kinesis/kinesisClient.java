import java.net.InetAddress;
import java.util.UUID;

import com.amazonaws.AmazonClientException;
import com.amazonaws.auth.AWSCredentials;
import com.amazonaws.auth.profile.ProfileCredentialsProvider;
import com.amazonaws.services.dynamodbv2.AmazonDynamoDB;
import com.amazonaws.services.dynamodbv2.AmazonDynamoDBClientBuilder;
import com.amazonaws.services.kinesis.AmazonKinesis;
import com.amazonaws.services.kinesis.AmazonKinesisClientBuilder;
import com.amazonaws.services.kinesis.clientlibrary.interfaces.IRecordProcessorFactory;
import com.amazonaws.services.kinesis.clientlibrary.lib.worker.InitialPositionInStream;
import com.amazonaws.services.kinesis.clientlibrary.lib.worker.KinesisClientLibConfiguration;
import com.amazonaws.services.kinesis.clientlibrary.lib.worker.Worker;
import com.amazonaws.services.kinesis.model.ResourceNotFoundException;
import org.apache.avro.Schema;
import org.apache.avro.file.DataFileReader;
import org.apache.avro.file.DataFileWriter;
import org.apache.avro.file.FileReader;
import org.apache.avro.generic.GenericData;
import org.apache.avro.generic.GenericDatumReader;
import org.apache.avro.generic.GenericDatumWriter;
import org.apache.avro.generic.GenericRecord;
import org.apache.avro.io.*;
import org.apache.avro.specific.SpecificDatumReader;
import org.apache.avro.specific.SpecificDatumWriter;
import org.apache.avro.util.Utf8;

import java.io.ByteArrayOutputStream;

public interface IActivity {
    void recordAsync(Activity record, String partitionKey);
}

public class KinesisClient implements IActivity {
    private final AmazonKinesis _kinesis;
    private final String _streamName;

    public KinesisClient(AmazonKinesis kinesis, string streamName) {
        _kinesis = kinesis;
        _streamName = streamName;
    }

    @Override
    public void recordAsync(Activity record, String partitionKey) throws IOException {
        PutRecordRequest putRecordRequest = new PutRecordRequest();
        putRecordRequest.setStreamName(_streamName);
        putRecordRequest.setData(serilize(record));
        putRecordRequest.setPartitionKey(String.format("partitionKey-%d", createTime));
        PutRecordResult putRecordResult = kinesis.putRecord(putRecordRequest);
    }

    private Byte[] serialize(Activity record) throws IOException {
        ByteArrayOutputStream ms = new ByteArrayOutputStream();
        SpecificDatumWriter<Activity> writer = new SpecificDatumWriter<Activity>(record.class);
        Encoder encoder = EncoderFactory.get().binaryEncoder(out, null);
        writer.write(record, encoder);
        encoder.flush();
        ms.close();
        return ms.toByteArray();
    }
}
