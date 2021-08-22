using eProject_SymphonyLimited.Migrations;
using eProject_SymphonyLimited.Models.Authorize;
using System.Data.Entity;

namespace eProject_SymphonyLimited.Models
{
    public class SymphonyLimitedDBContext : DbContext
    {
        public SymphonyLimitedDBContext() : base("name=SymphonyLimited")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SymphonyLimitedDBContext, Configuration>());
        }
        
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<RegisterInfo> RegisterInfo { get; set; }
        public virtual DbSet<Class> Class { get; set; }
        public virtual DbSet<Admission> Admission { get; set; }
        public virtual DbSet<CoreConfigData> CoreConfigData { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<Partner> Partner { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }
        public virtual DbSet<Faq> Faq { get; set; }
        public virtual DbSet<PaidRegister> PaidRegister { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<GroupUser> GroupUser { get; set; }
        public virtual DbSet<Business> Business { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<GroupPermission> GroupPermission { get; set; }
    }
}