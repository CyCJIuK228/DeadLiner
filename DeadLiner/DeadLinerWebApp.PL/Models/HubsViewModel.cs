using System.Collections.Generic;

namespace DeadLinerWebApp.PL.Models
{
    public class HubsViewModel
    {
        public List<HubModel> Hubs { get; set; }
    }

    public class HubModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}