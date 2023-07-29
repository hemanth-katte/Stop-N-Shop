using Microsoft.AspNetCore.SignalR;

namespace Stop_nShop.Hubs
{
    public class ChatHub : Hub
    {
        public async Task<string> SendMessage(string user, string message)
        {
            try
            {
                Clients.All.SendAsync("ReceiveMessage", user, message); //message to be displayed to all users using chat
                return "Message sent successfully!"; //to be printed in javascript console 

            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return ex.Message; //to be printed in javascript console
            }
        }
    }
}
