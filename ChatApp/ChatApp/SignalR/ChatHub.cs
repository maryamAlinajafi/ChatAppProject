using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
namespace SignalRChat
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update ALL clients.
            //Clients.All.addNewMessageToPage(name, message);

            // Call the addNewMessageToPage method to update ONE client & SENDER.
            //Clients.User(name).addNewMessageToPage(name, message);
            //Clients.User("maryamalinajafi").addNewMessageToPage(name, message);


            // Call the addNewMessageToPage method to update GROUP OF clients.
          
        }
    }
}