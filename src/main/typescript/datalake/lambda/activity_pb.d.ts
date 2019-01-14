// package: datalake.schema
// file: activity.proto

import * as jspb from "google-protobuf";

export class UserActivity extends jspb.Message {
  getUserid(): string;
  setUserid(value: string): void;

  getUseractivitytype(): UserActivity.UserActivityType;
  setUseractivitytype(value: UserActivity.UserActivityType): void;

  getSystem(): string;
  setSystem(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): UserActivity.AsObject;
  static toObject(includeInstance: boolean, msg: UserActivity): UserActivity.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: UserActivity, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): UserActivity;
  static deserializeBinaryFromReader(message: UserActivity, reader: jspb.BinaryReader): UserActivity;
}

export namespace UserActivity {
  export type AsObject = {
    userid: string,
    useractivitytype: UserActivity.UserActivityType,
    system: string,
  }

  export enum UserActivityType {
    CREATED = 0,
    PASSWORDCHANGED = 1,
    DEACTIAVTED = 2,
  }
}

export class AccountActivity extends jspb.Message {
  getAccountid(): string;
  setAccountid(value: string): void;

  getAccounttype(): AccountActivity.AccountType;
  setAccounttype(value: AccountActivity.AccountType): void;

  getAccountactivitytype(): AccountActivity.AccountActivityType;
  setAccountactivitytype(value: AccountActivity.AccountActivityType): void;

  getOtherdata(): string;
  setOtherdata(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): AccountActivity.AsObject;
  static toObject(includeInstance: boolean, msg: AccountActivity): AccountActivity.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: AccountActivity, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): AccountActivity;
  static deserializeBinaryFromReader(message: AccountActivity, reader: jspb.BinaryReader): AccountActivity;
}

export namespace AccountActivity {
  export type AsObject = {
    accountid: string,
    accounttype: AccountActivity.AccountType,
    accountactivitytype: AccountActivity.AccountActivityType,
    otherdata: string,
  }

  export enum AccountType {
    CHECKING = 0,
    SAVING = 1,
    RETIREMENT = 2,
    ANUNITY = 3,
    GL = 4,
    MORTAGE = 5,
  }

  export enum AccountActivityType {
    CREATED = 0,
    ARCHIVED = 1,
  }
}

export class Activity extends jspb.Message {
  getUserid(): string;
  setUserid(value: string): void;

  getTimestamp(): number;
  setTimestamp(value: number): void;

  getActivitytype(): Activity.ActivityType;
  setActivitytype(value: Activity.ActivityType): void;

  hasAccountactivity(): boolean;
  clearAccountactivity(): void;
  getAccountactivity(): AccountActivity | undefined;
  setAccountactivity(value?: AccountActivity): void;

  hasUseractivity(): boolean;
  clearUseractivity(): void;
  getUseractivity(): UserActivity | undefined;
  setUseractivity(value?: UserActivity): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): Activity.AsObject;
  static toObject(includeInstance: boolean, msg: Activity): Activity.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: Activity, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): Activity;
  static deserializeBinaryFromReader(message: Activity, reader: jspb.BinaryReader): Activity;
}

export namespace Activity {
  export type AsObject = {
    userid: string,
    timestamp: number,
    activitytype: Activity.ActivityType,
    accountactivity?: AccountActivity.AsObject,
    useractivity?: UserActivity.AsObject,
  }

  export enum ActivityType {
    ACCOUNT = 0,
    USER = 1,
  }
}

