using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM.Model;

namespace ORM
{
    public class PhotoGalleryDbContext : DbContext
    {
        public PhotoGalleryDbContext() : base("PhotoGalleryDbConnection")
        {
            Database.SetInitializer(new PhotoGalleryContextInitializer());
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserImage> UsersImages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region Membership

            modelBuilder.Entity<User>().HasRequired(u => u.Role).WithRequiredPrincipal(p => p.User).Map(m => m.MapKey("UserId"));

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserImages)
                .WithMany(r => r.Users)
                .Map(m => { m.ToTable("UsersImages"); m.MapLeftKey("UserId"); m.MapRightKey("ImageId"); });

            

            #endregion
        }
    }

    public class PhotoGalleryContextInitializer : CreateDatabaseIfNotExists<PhotoGalleryDbContext>
    {
        protected override void Seed(PhotoGalleryDbContext context)
        {
            context.Users.Add(new User
            {
                Name = "admin",
                Email = "admin@email.com",
                Password = "admin",
                Role = new Role { RoleName = "Администратор" }
            });
            context.SaveChanges();
        }

    }

}
