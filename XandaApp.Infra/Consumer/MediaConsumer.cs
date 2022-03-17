using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XandaApp.Data.Context;
using XandaApp.Data.Models;

namespace XandaApp.Infra.Consumer
{
    public class MediaConsumer : IConsumer<Media>
    {
        private readonly ApplicationDbContext _context;

        public MediaConsumer(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<Media> context)
        {
            var data = context.Message;

            await _context.AddAsync(data);
            _context.SaveChanges();

        }
    }
}
