using Microsoft.EntityFrameworkCore;
using PeoplePortalDomainLayer.Entities.Models;

namespace PeoplePortalDataAccessLayer.DbContext
{

    /// <summary>
    /// this class is used to map models with database tables and make connection with database
    /// </summary>
    public class PeoplePortalDb : Microsoft.EntityFrameworkCore.DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DesignationLevel>()
                .HasKey(c => new { c.DepartmentDesignationId, c.LevelId });

            modelBuilder.Entity<FeatureException>()
                .HasKey(c => new { c.EmployeeId, c.ExceptionFeature });
        }

        public PeoplePortalDb(DbContextOptions<PeoplePortalDb> dbContextOptions) : base(dbContextOptions)
        {

        }
        /// <summary>
        /// this property is used to get and set data from Departments table in given database
        /// </summary>
        public DbSet<Department> Departments { get; set; }
        /// <summary>
        /// this property is used to get and set data from Designations table in given database
        /// </summary>
        public DbSet<Designation> Designations { get; set; }
        /// <summary>
        /// this property is used to get and set data from Levels table in given database
        /// </summary>

        public DbSet<Level> Levels { get; set; }
        /// <summary>
        /// this property is used to get and set data from Skills table in given database
        /// </summary>
        public DbSet<Skill> Skills { get; set; }
        /// <summary>
        /// this property is used to get and set data from ProjectRequirements table in given database
        /// </summary>
        public DbSet<ProjectRequirements> ProjectRequirements { get; set; }
        /// <summary>
        /// this property is used to get and set data from Features table in given database
        /// </summary>
        public DbSet<Feature> Features { get; set; }
        /// <summary>
        /// this property is used to get and set data from Department_Designations table in given database
        /// </summary>
        public DbSet<DepartmentDesignation> DepartmentDesignations { get; set; }
        /// <summary>
        /// this property is used to get and set data from Basic_Configurations table in given database
        /// </summary>
        public DbSet<BasicConfiguration> BasicConfigurations { get; set; }
        /// <summary>
        /// this property is used to get and set data from Employees table in given database
        /// </summary>
        public DbSet<Employee> Employees { get; set; }
        /// <summary>
        /// this property is used to get and set data from EmployeeSkills table in given database
        /// </summary>
        public DbSet<EmployeeSkill> EmployeeSkills { get; set; }
        /// <summary>
        /// this property is used to get and set data from EmployeeType table in given database
        /// </summary>
        public DbSet<EmployeeType> EmployeeType { get; set; }
        /// <summary>
        /// this property is used to get and set data from EmployeeLevel table in given database
        /// </summary>
        public DbSet<DesignationLevel> DesignationLevel { get; set; }
        /// <summary>
        /// this property is used to get and set data from Exception table in given database
        /// </summary>
        public DbSet<FeatureException> FeatureException { get; set; }
        /// <summary>
        /// this property is used to get and set data from Login table in given database
        /// </summary>
        public DbSet<Login> Login { get; set; }
        /// <summary>
        /// this property is used to get and set data from Project table in given database
        /// </summary>
        public DbSet<Project> Project { get; set; }
        /// <summary>
        /// this property is used to get and set data from ProjectManagement table in given database
        /// </summary>
        public DbSet<ProjectManagement> ProjectManagement { get; set; }
        /// <summary>
        /// this property is used to get and set data from ReportingManager table in given database
        /// </summary>
        public DbSet<ReportingManager> ReportingManager { get; set; }


        /// </summary>
        /// this constructor is used to add connection with database
        /// </summary>
        ///<remarks>
        ///the connection string written inside the web config, here we are just using the connection name written inside the config file
        /// </remarks>

    }
}
