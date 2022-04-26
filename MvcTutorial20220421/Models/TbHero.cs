using System;
using System.Collections.Generic;

namespace MvcTutorial20220421.Models
{
    public partial class TbHero
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Atk { get; set; }
        public int Hp { get; set; }
        public string Skill { get; set; } = null!;
    }
}
