using System.IO;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace YoutubeDownloader.Core
{
    public class YoutubeDownloader
    {
        public static async Task<Video> GetVideo(string url)
        {
            var youtube = new YoutubeClient();
            return await youtube.Videos.GetAsync(url);
        }

        public static async Task<Stream> GetMuxedWithHighestVideoQualityStream(string url)
        {
            var youtube = new YoutubeClient();
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(url);
            var streamInfo = streamManifest.GetMuxed().WithHighestVideoQuality();
            if (streamInfo != null)
            {
                return await youtube.Videos.Streams.GetAsync(streamInfo);
            }

            return null;
        }

        public static async Task<Stream> GetVideoOnlyWithHighestVideoQuality(string url)
        {
            var youtube = new YoutubeClient();
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(url);
            var streamInfo = streamManifest.GetVideoOnly().WithHighestVideoQuality();
            if (streamInfo != null)
            {
                return await youtube.Videos.Streams.GetAsync(streamInfo);
            }

            return null;
        }

    }
}