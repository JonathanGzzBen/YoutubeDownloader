using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
    }
}