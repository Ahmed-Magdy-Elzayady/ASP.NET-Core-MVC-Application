using Day04Lab.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Day04Lab.Configrations
{
    public class ProductConfigration : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.HasOne(r => r.Category)
                .WithMany(t => t.Product)
                .HasForeignKey(o => o.CategoriesId)
                .IsRequired();

        }


            }
}
