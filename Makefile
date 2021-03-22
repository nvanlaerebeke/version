.PHONY: linux alpine

linux: clean
	dotnet publish -c Release -o ./build -r linux-x64 --self-contained true -p:PublishTrimmed=true -p:PublishSingleFile=true ./src/version/version.csproj
alpine: clean
	dotnet publish -c Release -o ./build -r alpine-x64 --self-contained true  -p:PublishTrimmed=true -p:PublishSingleFile=true ./src/version/version.csproj
clean:
	rm -rf build
	rm -rf ./src/*/obj
	rm -rf ./src/*/bin
