using Microsoft.AspNetCore.SignalR;
using ServerFreak.Models;
using WebAppServer1.Controllers;

namespace WebAppServer1.Hubs
{
    public class SignalHub : Hub
    {
        public async Task UpdateChat(string from, string to, string message)
        {
            await Clients.All.SendAsync("MessageSent",from,to, message);
        }

        public async Task UpdateContacts(string user, string contact)
        {
            await Clients.All.SendAsync("ClientAdded", user, contact);
        }
    }
}
