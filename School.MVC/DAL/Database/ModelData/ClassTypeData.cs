using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.MVC.DAL.Models;

namespace School.MVC.DAL.Database.ModelData
{
    public class ClassTypeData : IEntityTypeConfiguration<ClassType>
    {
        public void Configure(EntityTypeBuilder<ClassType> builder)
        {
            builder.HasData(DbDataConfigurator.ClassTypes);
        }
    }
}
