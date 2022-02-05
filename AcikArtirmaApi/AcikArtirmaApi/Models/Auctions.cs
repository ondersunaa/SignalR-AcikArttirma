using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcikArtirmaApi.Models
{
    public class Auctions
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Product { get; set; }
        public decimal StartingPrice { get; set; }
        public string PhotoUrl { get; set; }
        public int UserCount { get; set; }
    }
}
