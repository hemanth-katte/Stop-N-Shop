using Microsoft.AspNetCore.SignalR;

namespace Stop_nShop.Hubs
{
    public class BroadcastHub : Hub
    {
        public async Task<string> BroadcastOffersToUsers(List<string> offers)
        {
            try
            {
                await Clients.All.SendAsync("ReceiveOffers",offers);
                return "Offers sent to all users!"; //to be displayed in javascript console

            }catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
