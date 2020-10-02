using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YoutubeExplode.Videos;

namespace YoutubeDownloader.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/videos")]
    [ApiVersion("1")]
    public partial class VideosController : ControllerBase
    {
        /// <summary>
        /// Get details of video with specified url.
        /// </summary>
        /// <param name="videoUrl"></param>
        /// <returns></returns>
        [HttpGet("details")]
        [ProducesResponseType(typeof(Video), StatusCodes.Status200OK)]
        public async Task<IActionResult> Details([Required]string videoUrl)
        {
            var videoDetails = await Core.YoutubeDownloader.GetVideo(videoUrl);
            return Ok(videoDetails);
        }

        [HttpGet("download")]
        [ProducesResponseType(typeof(Stream), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Download([Required]string videoUrl, DownloadType downloadType = DownloadType.Muxed)
        {
            try
            {
                var videoDetails = await Core.YoutubeDownloader.GetVideo(videoUrl);
                Stream videoStream = downloadType switch
                {
                    DownloadType.VideoOnly => await Core.YoutubeDownloader.GetVideoOnlyWithHighestVideoQuality(videoUrl),
                    _ => await  Core.YoutubeDownloader.GetMuxedWithHighestVideoQualityStream(videoUrl)
                };

                return File(videoStream, "application/octet-stream", (videoDetails.Title + ".mp4"));
            }
            catch
            {
                return NotFound("No video with that url found.");
            }

        }

    }

    public partial class VideosController
    {
        public enum DownloadType
        {
            Muxed,
            SoundOnly,
            VideoOnly
        }
    }

}