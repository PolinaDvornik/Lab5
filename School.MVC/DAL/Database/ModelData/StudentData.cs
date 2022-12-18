using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.MVC.DAL.Models;

namespace School.MVC.DAL.Database.ModelData
{
    public class StudentData : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasData(DbDataConfigurator.Students);
        }
    }
}
