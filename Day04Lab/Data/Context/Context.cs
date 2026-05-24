using Day04Lab.Models;
using Microsoft.EntityFrameworkCore;

namespace Day04Lab.Data
{
    public class Context:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string ConnectionString =
                "Data source=MR-BOMBASTIC\\SQLEXPRESS;Initial catalog=Day04MVcLab;Integrated security=true;TrustServerCertificate=True";
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
            List<Products> _products = new List<Products>()
{
    new Products()
    {
        Id = 1,
        Title = "Premium Egyptian Rice",
        Description = "High-quality natural white rice, 1kg pack",
        Price = 35,
        Count = 150,
        Date_Of_Production = new DateOnly(2026, 1, 10),
        Date_Of_Expire = new DateOnly(2027, 1, 10),
        CategoriesId = 1 
    },
    new Products()
    {
        Id = 2,
        Title = "Penne Pasta",
        Description = "Made from 100% pure durum wheat semolina, 400g",
        Price = 15,
        Count = 200,
        Date_Of_Production = new DateOnly(2026, 2, 1),
        Date_Of_Expire = new DateOnly(2027, 2, 1),
        CategoriesId = 1
    },
    new Products()
    {
        Id = 3,
        Title = "Sunflower Oil",
        Description = "Pure refined sunflower oil for cooking and frying, 1L",
        Price = 80,
        Count = 80,
        Date_Of_Production = new DateOnly(2026, 1, 5),
        Date_Of_Expire = new DateOnly(2027, 1, 5),
        CategoriesId = 1
    },
    new Products()
    {
        Id = 4,
        Title = "Full Cream Milk",
        Description = "Pasteurized and sterilized natural cow milk, 1L",
        Price = 42,
        Count = 60,
        Date_Of_Production = new DateOnly(2026, 5, 1),
        Date_Of_Expire = new DateOnly(2026, 11, 1),
        CategoriesId = 2 
    },
    new Products()
    {
        Id = 5,
        Title = "Feta White Cheese",
        Description = "Smooth vegetable oil feta cheese, 500g",
        Price = 38,
        Count = 90,
        Date_Of_Production = new DateOnly(2026, 4, 15),
        Date_Of_Expire = new DateOnly(2026, 10, 15),
        CategoriesId = 2
    },
    new Products()
    {
        Id = 6,
        Title = "Natural Yogurt",
        Description = "Fresh plain yogurt cup, 105g",
        Price = 8,
        Count = 120,
        Date_Of_Production = new DateOnly(2026, 5, 20),
        Date_Of_Expire = new DateOnly(2026, 6, 4), 
        CategoriesId = 2
    },
    new Products()
    {
        Id = 7,
        Title = "Frozen Beef Burger",
        Description = "Pure beef burger patties ready for grilling, 1kg",
        Price = 160,
        Count = 45,
        Date_Of_Production = new DateOnly(2026, 3, 1),
        Date_Of_Expire = new DateOnly(2026, 9, 1),
        CategoriesId = 3 
    },
    new Products()
    {
        Id = 8,
        Title = "Frozen Chicken Pane",
        Description = "Breaded and seasoned chicken breast fillets, 1kg",
        Price = 180,
        Count = 40,
        Date_Of_Production = new DateOnly(2026, 2, 20),
        Date_Of_Expire = new DateOnly(2026, 8, 20),
        CategoriesId = 3
    },
    new Products()
    {
        Id = 9,
        Title = "Tomato Paste Puree",
        Description = "Natural rich tomato paste jar, 360g",
        Price = 22,
        Count = 110,
        Date_Of_Production = new DateOnly(2026, 1, 15),
        Date_Of_Expire = new DateOnly(2027, 7, 15),
        CategoriesId = 4
    },
    new Products()
    {
        Id = 10,
        Title = "Solid Tuna",
        Description = "Easy-open canned solid tuna in vegetable oil, 185g",
        Price = 55,
        Count = 130,
        Date_Of_Production = new DateOnly(2026, 1, 1),
        Date_Of_Expire = new DateOnly(2029, 1, 1),
        CategoriesId = 4
    }
};


            List<Categories> _categories = new List<Categories>()
                 {
                 new Categories() { Id = 1, Name = "Dry Groceries" },
                 new Categories() { Id = 2, Name = "Dairy & Eggs" },
                 new Categories() { Id = 3, Name = "Meat & Frozen Food" },
                 new Categories() { Id = 4, Name = "Canned Goods" },
                 new Categories() { Id = 5, Name = "Beverages" },
                 new Categories() { Id = 6, Name = "Snacks & Sweets" }
                    };

            modelBuilder.Entity<Products>().HasData(_products);
            modelBuilder.Entity<Categories>().HasData(_categories);

        }

        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }



    }
}
