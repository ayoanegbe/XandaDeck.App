using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XandaApp.Infra.Services;
using XandaApp.Data.Models;

namespace XandaApp.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly IConfiguration _configuration;

        public MediaController(IBus bus)
        {
            _bus = bus;
            _configuration = AppServices.GetConfiguration();
        }

        [HttpPost("publish")]
        public async Task<IActionResult> PublishMedia([FromBody] Media media)
        {
            if (media != null)
            {
                media.CreatedOn = DateTime.Now;
                Uri uri = new Uri($"{_configuration["RabbitMQ:Uri"]}/mediaQueue");
                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(media);
                return Ok();
            }

            return BadRequest();
        }
    }
}
