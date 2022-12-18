using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.MVC.DAL.Models;

namespace School.MVC.DAL.Database.ModelData
{
    public class ClassData : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.HasData(DbDataConfigurator.Classes);
        }
    }
}
