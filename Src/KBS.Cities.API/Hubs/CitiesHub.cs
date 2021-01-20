using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace KBS.Cities.API.Hubs
{
    public class CitiesHub : Hub
    {
        public async Task UpdateCities()
        {
            await Clients.All.SendAsync("CitiesUpdated");
        }
    }
}
