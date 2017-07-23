using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrimeReportSystem.BL.Context;
using CrimeReportSystem.BL.Test;
using CrimeReportSystem.BL.Entities.SecurityData;

namespace CrimeReportSystem.BL.Test
{
    /*
     * This class contains 1 running test method.
     * The execution of this method will be slow as it attempts to create and seed a database with allot of data in it
     * this data can be used for report development.
     * 
     * I have commented out the [TestMethod] attribute for quick testing.
     
     */
    [TestClass]
    public class ContextTest : ProviderTestBase
    {
        [TestMethod]
        [TestCategory("Utilities")]
        public void TestCreateDB()
        {
            DataContext.Setup();
        }

      //  [TestMethod]
        [TestCategory("Utilities")]
        public void SeedRandomDB()
        {
            using (var ctx = new DataContext())
            {
               
            }
        }

        private void CreateUserAndTimesheets(DataContext ctx, UserIdentity user)
        {
            user = SeedData.CreateUser(ctx);

            //List<Timesheet> ts = new List<Timesheet>
            //{
            //    new Timesheet{UserIdentityId=user.Id, StartDate=new DateTime(2015,05,05),EndDate=new DateTime(2015,05,11)},
            //    new Timesheet{UserIdentityId=user.Id, StartDate=new DateTime(2015,05,12),EndDate=new DateTime(2015,05,17)}

            //};



            //user = SeedData.CreateUser(ctx);
           // var user1 = SeedData.CreateUser(ctx,);
            //var user2 = SeedData.CreateUser(ctx);
            //var user3 = SeedData.CreateUser(ctx);
            //var user4 = SeedData.CreateUser(ctx);
            //var user5 = SeedData.CreateUser(ctx);
            //var user6 = SeedData.CreateUser(ctx);
            //var user7 = SeedData.CreateUser(ctx);
            //var user8 = SeedData.CreateUser(ctx);
            //var user9 = SeedData.CreateUser(ctx);
            //var user10 = SeedData.CreateUser(ctx);

            //var timesheet = new Timesheet();
            //var obj = SeedData.CreateTimesheet(ctx, user1.Id, new DateTime(2015, 04, 27), new DateTime(2015, 05, 04));

            //List<Timesheet> ts = new List<Timesheet>();

            // ts.Add(new SeedData.CreateTimesheet(){ctx, user1.Id, new DateTime(2015, 05, 04), new DateTime(2015, 05, 10)});
            //     //var ts1 = SeedData.CreateTimesheet(ctx, user1.Id, new DateTime(2015, 05, 04), new DateTime(2015, 05, 10));

            //foreach(SeedData.CreateTimesheet() ct in ts)
            //    ctx.TimesheetSet.Add(ct);

            //var ts1 = SeedData.CreateTimesheet(ctx, user1.Id, new DateTime(2015, 05, 01), new DateTime(2015, 05, 10));
            //var ts2 = SeedData.CreateTimesheet(ctx, user1.Id, new DateTime(2015, 05, 02), new DateTime(2015, 05, 10));
            //var ts3 = SeedData.CreateTimesheet(ctx, user2.Id, new DateTime(2015, 05, 03), new DateTime(2015, 05, 17));
            //var ts4 = SeedData.CreateTimesheet(ctx, user2.Id, new DateTime(2015, 05, 04), new DateTime(2015, 05, 17));

            //var ts5 = SeedData.CreateTimesheet(ctx, user.Id, new DateTime(2015, 05, 05), new DateTime(2015, 05, 10));
            //var ts6 = SeedData.CreateTimesheet(ctx, user1.Id, new DateTime(2015, 05, 06), new DateTime(2015, 05, 10));
            //var ts7 = SeedData.CreateTimesheet(ctx, user2.Id, new DateTime(2015, 05, 07), new DateTime(2015, 05, 17));
            //var ts8 = SeedData.CreateTimesheet(ctx, user2.Id, new DateTime(2015, 05, 08), new DateTime(2015, 05, 17));
            //List<Timesheet> ts = new List<Timesheet>
            //{
            //    new Timesheet{UserIdentityId=user1.Id, StartDate=new DateTime(2015,05,05),EndDate=new DateTime(2015,05,11)},
            //    new Timesheet{UserIdentityId=user1.Id, StartDate=new DateTime(2015,05,12),EndDate=new DateTime(2015,05,17)}

            //};
            ////var ts9 = SeedData.CreateTimesheet(ctx, user2.Id, new DateTime(2015, 05, 09), new DateTime(2015, 05, 17));
            //foreach (var item in ts)
            //    ctx.TimesheetSet.Add(item);

            //var ts10 = SeedData.CreateTimesheet(ctx, ts.FirstOrDefault().Id, ts.FirstOrDefault().EndDate, ts.FirstOrDefault().EndDate);

            //var ts1 = new[]
            //{
            //   // new timesheet {ctx, user1.Id, new DateTime(2015, 04, 27), new DateTime(2015, 05, 04)},
            //   new SeedData.CreateTimesheet(new DateTime(2015, 04, 27), new DateTime(2015, 05, 04))
            //    //new SeedData{ctx, user1.Id, new DateTime(2015, 05, 11), new DateTime(2015, 05, 17)},
            //    //new SeedData.CreateTimesheets{ctx, user1.Id, new DateTime(2015, 05, 18), new DateTime(2015, 05, 24)},
            //    //new SeedData.CreateTimesheet{ctx, user1.Id, new DateTime(2015, 05, 25), new DateTime(2015, 06, 01)}
            //};
            //timesheet.Add(new SeedData.CreateTimesheet(){ctx, user1.Id, new DateTime(2015, 05, 11), new DateTime(2015, 05, 17)});

            //foreach (timesheet ts in ts1)
            //    ctx.TimesheetSet.Add(ts1);

            //var ts2 = SeedData.CreateTimesheet(ctx, user1.Id, new DateTime(2015, 04, 27), new DateTime(2015, 05, 04));
            //var ts3 = SeedData.CreateTimesheet(ctx, user1.Id, new DateTime(2015, 05, 11), new DateTime(2015, 05, 17));
            //var ts4 = SeedData.CreateTimesheet(ctx, user1.Id, new DateTime(2015, 05, 18), new DateTime(2015, 05, 24));
            //var ts5 = SeedData.CreateTimesheet(ctx, user1.Id, new DateTime(2015, 05, 25), new DateTime(2015, 06, 01));

        }
    }
}
