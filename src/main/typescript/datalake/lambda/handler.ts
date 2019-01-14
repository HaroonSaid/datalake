import { Buffer } from "buffer";
import {
  KinesisStreamEvent,
  KinesisStreamRecord,
  Context,
  KinesisStreamHandler
} from "aws-lambda";
import { Activity } from "./activity_pb";

export const handler: KinesisStreamHandler = async (
  event: KinesisStreamEvent,
  context: Context
): Promise<any> => {
  console.info(`${context.awsRequestId} message recieved`);
  event.Records.forEach(record => processRecord(record));
};
const processRecord = async (record: KinesisStreamRecord) => {
  const payload = new Buffer(record.kinesis.data, "base64");
  const message = Activity.deserializeBinary(payload);
  console.info(`message:${message}`);
};
