using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using School.MVC.DAL.Database.ModelData;
using School.MVC.DAL.Database;
using School.MVC.DAL.Models;

namespace School.MVC.Areas.Identity.Data;

//TODO: Класс для связи с бд, который содержит списки соответствующие таблицам
public class SchoolContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Class> Classes { get; set; }
    public DbSet<ClassType> ClassTypes { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Teacher> Teachers { get; set; }

    public SchoolContext(DbContextOptions<SchoolContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // настраиваем тип удаления (no action) с помощью Fluent Api
        modelBuilder.Entity<Schedule>()
            .HasOne(o => o.Teacher).WithMany().OnDelete(DeleteBehavior.NoAction);

        /*
        DbDataConfigurator.FillModelArrays();

        modelBuilder.ApplyConfiguration(new ClassData());
        modelBuilder.ApplyConfiguration(new ClassTypeData());
        modelBuilder.ApplyConfiguration(new ScheduleData());
        modelBuilder.ApplyConfiguration(new StudentData());
        modelBuilder.ApplyConfiguration(new SubjectData());
        modelBuilder.ApplyConfiguration(new TeacherData());*/
    }
}
