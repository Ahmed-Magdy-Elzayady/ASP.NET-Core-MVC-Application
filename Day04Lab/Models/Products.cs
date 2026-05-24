namespace Day04Lab.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public DateOnly Date_Of_Production { get; set; }
        public DateOnly Date_Of_Expire { get; set; }
        public int CategoriesId { get; set; }
        public  virtual Categories Category { get; set; }

    }
}
