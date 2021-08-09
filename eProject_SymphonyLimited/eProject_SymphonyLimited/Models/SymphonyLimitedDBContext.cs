using eProject_SymphonyLimited.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

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
        public virtual DbSet<RegisterAdmission> RegisterAdmission { get; set; }
    }
}