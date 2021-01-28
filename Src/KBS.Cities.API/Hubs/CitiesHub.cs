using KBS.Cities.Shared.DTO;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace KBS.Cities.API.Hubs
{
    public class CitiesHub : Hub
    {
        public async Task Update(CityDto dto)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("Updated", dto);
        }

        public async Task Delete(CityDto dto)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("Deleted", dto);
        }

        public async Task Add(CityDto dto)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("Added", dto);
        }
    }
}
