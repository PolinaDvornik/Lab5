using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.MVC.DAL.Models;

namespace School.MVC.DAL.Database.ModelData
{
    public class ScheduleData : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.HasData(DbDataConfigurator.Schedules);
        }
    }
}
