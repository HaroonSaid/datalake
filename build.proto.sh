#!/bin/bash
~/.nuget/packages/google.protobuf.tools/3.6.1/tools/macosx_x64/protoc --proto_path=src/main/schema activity.proto --csharp_out=src/main/csharp/datalake/schema/
~/.nuget/packages/google.protobuf.tools/3.6.1/tools/macosx_x64/protoc --proto_path=src/main/schema activity.proto --java_out=src/main/java/
~/.nuget/packages/google.protobuf.tools/3.6.1/tools/macosx_x64/protoc --proto_path=src/main/schema activity.proto --js_out=src/main/js
