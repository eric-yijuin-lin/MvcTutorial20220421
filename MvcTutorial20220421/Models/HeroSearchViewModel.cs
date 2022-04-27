namespace MvcTutorial20220421.Models
{
    public class HeroSearchViewModel
    {
        // 搜尋條件
        public HeroSearchParams SearchParams { get; set; }
        // 搜尋結果(英雄們)
        public List<TbHero> Heroes { get; set; }

        public HeroSearchViewModel()
        {
            // 初始化記憶體 => 把房間打掃乾淨，才能入住
            SearchParams = new HeroSearchParams();
            Heroes = new List<TbHero>();
        }
    }

    public class HeroSearchParams
    {
        public string? Name { get; set; }
        public int MinAtk { get; set; }
        public int MaxAtk { get; set; }
    }
}
