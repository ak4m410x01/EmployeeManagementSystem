using EmployeeManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EmployeeManagementSystem.Persistence.DbContexts
{
    public class EmployeeManagementSystemDbContext : DbContext
    {
        #region Constructors

        public EmployeeManagementSystemDbContext(DbContextOptions<EmployeeManagementSystemDbContext> options) : base(options)
        {
        }

        #endregion Constructors

        #region DbSets

        public DbSet<User> Employees { get; set; }

        #endregion DbSets

        #region Methods

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Apply Entities Configurations
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        #endregion Methods
    }
}