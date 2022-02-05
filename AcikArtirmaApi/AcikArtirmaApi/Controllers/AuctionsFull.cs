using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcikArtirmaApi.Hubs;
using AcikArtirmaApi.Models;
using Microsoft.AspNetCore.SignalR;

namespace AcikArtirmaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionsFull : ControllerBase
    {
        private readonly IHubContext<AuctionHub> _context;

        public AuctionsFull(IHubContext<AuctionHub> context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddAuction(Auctions model)
        {

            //AuctionHub.auctionses.Add(model);
            await _context.Clients.All.SendAsync("NewAuction",  "Son eklenen açık artırma "+DateTime.Now+" tarihinde eklendi.");
            return Ok();
        }

    }
}
