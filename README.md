# YoutubeDownloader

This is a tool for downloading Youtube videos and playlists in different formats.

This application will start as an API, but it will also have both Web and WPF UIs.

## Build

To build the Docker image yourself, you can run the next commands:

```shell
# From solution directory
$ docker build -f YoutubeDownloader.Api/Dockerfile -t youtubedownloader-api .

# This is an example on how to run it.
$ docker run -d -p 5000:80 youtubedownloader-api

# You can see it running on localhost:5000
```
