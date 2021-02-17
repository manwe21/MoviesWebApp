namespace Web.Models.Search
{
    public class SearchItemViewModel
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public SearchCategory SearchCategory { get; set; }
        public int? Year { get; set; }
        public double? Rating { get; set; }

    }
}
