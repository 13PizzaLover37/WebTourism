using System;
using System.Collections.Generic;

namespace WebTourism.Models
{
    public partial class Posts
    {
        public Guid Id { get; set; }
        public Guid? CreatorId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Header { get; set; } = null!;
        public string Text { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string? IsActive { get; set; }
    }
}
