using System;
using System.Collections.Generic;

namespace MvcTutorial20220421.Models
{
    public partial class Cartoon
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public short EpisodeCount { get; set; }
        public int? PublishYear { get; set; }
        public DateTime? LastModifiedAt { get; set; }
    }
}
