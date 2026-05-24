namespace Day04Lab.ViewModels
{
    public class ProductsDetailsVM {


        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public DateOnly Date_Of_Production { get; set; }
        public DateOnly Date_Of_Expire { get; set; }
        public string Category { get; set; }


    }
}
