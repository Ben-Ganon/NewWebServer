using Microsoft.AspNetCore.SignalR;

namespace WebAppServer1.Hubs
{
    public class SignalHub : Hub
    {

        public async Task Changed(string value)
        {

        }
    }
}
