using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.MVC.DAL.Models;

namespace School.MVC.DAL.Database.ModelData
{
    public class TeacherData : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasData(DbDataConfigurator.Teachers);
        }
    }
}
