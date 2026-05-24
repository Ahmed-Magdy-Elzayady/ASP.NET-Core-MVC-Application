namespace Day04Lab.Models
{
    public class Categories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Products> Product { get; set; }
    }
}
