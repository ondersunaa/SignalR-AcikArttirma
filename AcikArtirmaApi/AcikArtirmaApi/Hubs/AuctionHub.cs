using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using AcikArtirmaApi.Models;
using Microsoft.AspNetCore.SignalR;

namespace AcikArtirmaApi.Hubs
{
    public class AuctionHub:Hub
    {
        public List<Auctions> auctionses = new List<Auctions>();

        public static Dictionary<string, string> users = new Dictionary<string, string>();
        public static int lastOffer = 0;
        public IReadOnlyList<string> GList;
        public async Task GetAuctions()
        {
            if (!users.ContainsKey(Context.ConnectionId))
            {
                Auctions acik = new Auctions
                {
                    Id = 1,
                    Name = "Araba açık artırması",
                    Product = "Audi A3",
                    StartingPrice = 100000,
                    PhotoUrl = "https://cdn.motor1.com/images/mgl/AebbV/s1/audi-a3-sportback-45-tfsi-e-2021.jpg",
                    UserCount = users.Count
                };
                auctionses.Clear();
                auctionses.Add(acik);
                await Clients.All.SendAsync("ReveicerAuctions", auctionses);
            }
           
        }

        public async Task offerUpdate(string offer)
        {
            lastOffer = Convert.ToInt32(offer);
            //await Clients.Groups("artirma").SendAsync("users", users);
            await Clients.Groups("artirma").SendAsync("lastOffer", users.Where(x=>x.Key == Context.ConnectionId).First().Value,lastOffer);
            // await Clients.Groups()
        }
        public async Task SubScribeAcikArtirma(string userName)
        {

            await Groups.AddToGroupAsync(Context.ConnectionId, "artirma");
            users.Add(Context.ConnectionId,userName);
            await Clients.Groups("artirma").SendAsync("users", users);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "artirma");
            users.Remove(Context.ConnectionId);
            await Clients.Groups("artirma").SendAsync("users", users);
        }
    }
}
