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
        public async Task GetMuxedTiwhHighestVideoQualityStream_ValidUrl_GetsValidVideoStream(string url)
        {
            await using var stream = await Core.YoutubeDownloader.GetMuxedWithHighestVideoQualityStream(url);
            var testFileName = @"test.mp4";
            await using var fileStream = File.Create(testFileName);
            await stream.CopyToAsync(fileStream);

            Assert.True(File.Exists(testFileName));

            File.Delete(testFileName);
        }
    }
}