using BM.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BM.DataAccess
{
    public class DbContext : IdentityDbContext<User>
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<PharmaReport.Models.PharmaReportContext>());

        public DbContext()
            : base("DefaultConnection")
        {
            //Configuration.ProxyCreationEnabled = true;
            //Configuration.LazyLoadingEnabled = true;
            Configuration.AutoDetectChangesEnabled=true;
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Ledger> Ledgers { get; set; }
        public DbSet<NLogError> NLogError { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            if (modelBuilder == null)
            {

                throw new ArgumentNullException("modelBuilder");

            }

            modelBuilder.Entity<Ledger>().HasRequired(s => s.Group).WithMany(r => r.Ledgers).WillCascadeOnDelete(false);

            // Keep this:

            modelBuilder.Entity<IdentityUser>().ToTable("Users");

            // Change TUser to User everywhere else - 
            // IdentityUser and User essentially 'share' the AspNetUsers Table in the database:

            EntityTypeConfiguration<User> table =

                modelBuilder.Entity<User>().ToTable("Users");

            table.Property((User u) => u.UserName).IsRequired();

            // EF won't let us swap out IdentityUserRole for UserRole here:

            modelBuilder.Entity<User>().HasMany<IdentityUserRole>((User u) => u.Roles);
            modelBuilder.Entity<IdentityUserRole>().HasKey((IdentityUserRole r) =>
                new { UserId = r.UserId, RoleId = r.RoleId }).ToTable("UserRoles");

            // Leave this alone:

            EntityTypeConfiguration<IdentityUserLogin> entityTypeConfiguration =
                modelBuilder.Entity<IdentityUserLogin>().HasKey((IdentityUserLogin l) =>
                    new
                    {
                        UserId = l.UserId,
                        LoginProvider = l.LoginProvider,
                        ProviderKey = l.ProviderKey
                    }).ToTable("UserLogins");



            entityTypeConfiguration.HasRequired<IdentityUser>((IdentityUserLogin u) => u.User);
            EntityTypeConfiguration<IdentityUserClaim> table1 =
                modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");



            table1.HasRequired<IdentityUser>((IdentityUserClaim u) => u.User);
            // Add this, so that IdentityRole can share a table with Role:

            modelBuilder.Entity<IdentityRole>().ToTable("Roles");

            // Change these from IdentityRole to Role:

            EntityTypeConfiguration<Role> entityTypeConfiguration1 =
                modelBuilder.Entity<Role>().ToTable("Roles");

            entityTypeConfiguration1.Property((Role r) => r.Name).IsRequired();

            //modelBuilder.Entity<Location>().HasRequired(x => x.ParentLocation)
            //.WithMany(x => x.ChildLocations)
            //.HasForeignKey(x => x.ParentId)
            //.WillCascadeOnDelete(true);
        }
    }
}
