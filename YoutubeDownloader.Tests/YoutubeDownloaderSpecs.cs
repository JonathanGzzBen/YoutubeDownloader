using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace YoutubeDownloader.Tests
{
    public class YoutubeDownloaderSpecs
    {
        [Theory]
        [InlineData("6RWfxcXCDrc")]
        [InlineData("https://www.youtube.com/watch?v=6RWfxcXCDrc")]
        public async Task GetVideo_ValidUrl_NotNullObject(string url)
        {
            var video = await Core.YoutubeDownloader.GetVideo(url);
            Assert.NotNull(video);
        }

        [Theory]
        [InlineData("6RWfxcXCDrc")]
        [InlineData("https://www.youtube.com/watch?v=XpS2i6ESnjE")]
        public async Task GetMuxedWithHighestVideoQualityStream_ValidUrl_ReturnsNotNull(string url)
        {
            await using var stream = await Core.YoutubeDownloader.GetMuxedWithHighestVideoQualityStream(url);
            Assert.NotNull(stream);
        }

        [Theory]
        [InlineData("https://www.youtube.com/watch?v=DkAHBVur-N4")]
        public async Task GetVideoOnlyWithHighestVideoQualityStream_ValidUrl_ReturnsNotNull(string url)
        {
            await using var stream = await Core.YoutubeDownloader.GetVideoOnlyWithHighestVideoQuality(url);
            Assert.NotNull(stream);
        }
    }
}