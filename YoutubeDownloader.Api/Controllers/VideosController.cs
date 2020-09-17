using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace YoutubeDownloader.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/videos")]
    [ApiVersion("1")]
    public class VideosController : ControllerBase
    {
        /// <summary>
        /// Get details of video with specified url.
        /// </summary>
        /// <param name="videoUrl"></param>
        /// <returns></returns>
        [HttpGet("details")]
        public async Task<IActionResult> Details(string videoUrl)
        {
            var videoDetails = await Core.YoutubeDownloader.GetVideo(videoUrl);
            return Ok(videoDetails);
        }

        [HttpGet("download")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Download(string videoUrl, bool videoOnly = false)
        {
            try
            {
                Stream videoStream;

                if (videoOnly)
                    videoStream = await Core.YoutubeDownloader.GetVideoOnlyWithHighestVideoQuality(videoUrl);
                else
                    videoStream = await Core.YoutubeDownloader.GetMuxedWithHighestVideoQualityStream(videoUrl);

                return File(videoStream, "application/octet-stream", "youtube-download.mp4");
            }
            catch
            {
                return NotFound("No video with that url found.");
            }

        }

    }
}