using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServerFreak.Models;
using WebAppServer1.Models;

namespace WebAppServer1.Data
{
    public class WebAppServer1Context : DbContext
    {
        public WebAppServer1Context (DbContextOptions<WebAppServer1Context> options)
            : base(options)
        {
        }

        public DbSet<ServerFreak.Models.Chat>? Chat { get; set; }

        public DbSet<WebAppServer1.Models.Contact> Contact { get; set; }
    }
}
