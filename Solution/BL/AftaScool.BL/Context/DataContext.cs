using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AftaScool.BL.Entities.SecurityData;
using TCR.Lib.BL;
using AftaScool.BL.Entities.Logging;
using System.Data.Entity.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AfterScool.BL.Entities.SecurityData;


//this is a comment 
//Demo changes.

namespace AftaScool.BL.Context
{
    public class DataContext : DbContext, IAuditDBContext<PrivilegeType>
    {
        #region Ctor

        public DataContext()
            : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
        }

        public DataContext(DbConnection connection)
            : base(connection, true)
        {

        }

        public static void Setup()
        {
            var connection = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"];
            using (DbConnection setupCon = new SqlConnection(connection.ConnectionString))
            {
                DataContext.Setup(setupCon);
            }
        }

        public static void Setup(DbConnection dbConnection, bool initDefaultData = true)
        {
            // Database.SetInitializer(new MigrateDatabaseToLatestVersion<SMDataContext, SkillsMap.BL.Migrations.Configuration>());
            Database.SetInitializer<DataContext>(new DropCreateDatabaseIfModelChanges<DataContext>());
            using (var context = new DataContext(dbConnection))
            {
                context.Database.Initialize(true);
            }
        }

        #endregion

        #region DBSets

       
        public DbSet<Role> RoleSet { get; set; }
        public DbSet<Privilege> PrivilegeSet { get; set; }
        public DbSet<UserIdentity> UserIdentitySet { get; set; }
        public DbSet<AuditLog> AuditLogSet { get; set; }

        
        public DbSet<SystemLog> SystemLogSet { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();


            modelBuilder.Entity<Role>()
                         .HasMany(x => x.Privileges)
                         .WithMany(a => a.Roles)
                         .Map(x =>
                         {
                             x.ToTable("RolePrivilege");
                             x.MapLeftKey("RoleId");
                             x.MapRightKey("PrivilegeId");
                         });


            modelBuilder.Entity<UserIdentity>()
                         .HasMany(x => x.Roles)
                         .WithMany(a => a.UserIdentities)
                         .Map(x =>
                         {
                             x.ToTable("UserIdentityRole");
                             x.MapLeftKey("UserIdentityId");
                             x.MapRightKey("RoleId");
                         });
        }

        public void AddSystemLogEntry(object sender, Guid guid, long? currentUserId,
            LogEventType logEventType, string message,
            string stackTrace = null, string innerExceptionMessage = null, string innerExceptionStackTrace = null)
        {
            try
            {
                //create a fresh context because the current context is in an error state
                using (var dbContext = new DataContext())
                {
                    var syslogEntry = new SystemLog();
                    dbContext.SystemLogSet.Add(syslogEntry);

                    syslogEntry.EventTime = DateTime.Now;
                    syslogEntry.Sender = sender.ToString();
                    syslogEntry.Id = guid;
                    syslogEntry.UserIdentityId = currentUserId;
                    syslogEntry.EventType = logEventType;
                    syslogEntry.Message = message;
                    syslogEntry.StackTrace = stackTrace;
                    syslogEntry.InnerException = innerExceptionMessage;
                    syslogEntry.InnerExceptionStackTrace = innerExceptionStackTrace;

                    dbContext.SaveChanges();
                }
            }
            catch
            {
                //empty exception never allowed!
                //in this case we are unable to hit the db to log an exception
                //therefore we do not want to change the actual error that happend in the handler
            }
        }


        #region Audit


        public int SaveChanges(IUserContext<PrivilegeType> currentUser)
        {
            if (currentUser != null)
                return AuditHandler.SaveChanges<AuditLog>(this, AuditLogSet, currentUser.UserName, currentUser.Id);
            return base.SaveChanges();
        }

        #endregion
    }
}
