FROM mcr.microsoft.com/dotnet/core/runtime:3.1
WORKDIR /app

RUN apt-get update && apt-get -y install cmake protobuf-compiler
RUN apt install g++ -y

COPY box2d .

RUN cmake -DCMAKE_BUILD_TYPE=Release -DCMAKE_DEPENDS_USE_COMPILER=FALSE -G "Unix Makefiles" /app
RUN cmake --build /app --target box2d