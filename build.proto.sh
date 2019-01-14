#!/bin/bash
export PROTOC_GEN_TS_PATH="./src/main/schema/node_modules/.bin/protoc-gen-ts"
export OUT_DIR="./src/main/typescript/datalake/lambda"
~/.nuget/packages/google.protobuf.tools/3.6.1/tools/macosx_x64/protoc --proto_path=src/main/schema activity.proto --csharp_out=src/main/csharp/datalake/schema/
~/.nuget/packages/google.protobuf.tools/3.6.1/tools/macosx_x64/protoc --proto_path=src/main/schema activity.proto --java_out=src/main/java/
~/.nuget/packages/google.protobuf.tools/3.6.1/tools/macosx_x64/protoc --proto_path=src/main/schema activity.proto \
--plugin="protoc-gen-ts=${PROTOC_GEN_TS_PATH}" \
    --js_out="import_style=commonjs,binary:${OUT_DIR}" \
    --ts_out="${OUT_DIR}" 
