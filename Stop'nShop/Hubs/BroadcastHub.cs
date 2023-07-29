using Microsoft.AspNetCore.SignalR;

namespace Stop_nShop.Hubs
{
    public class BroadcastHub : Hub<IBroadcastHubClient>
    {
        public async Task<string> BroadcastOffersToUsers(List<string> offers)
        {
            try
            {
                await Clients.All.BroadcastOffersToUsers(offers);
                return "Offers sent to all users!"; //to be displayed in javascript console

            }catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
