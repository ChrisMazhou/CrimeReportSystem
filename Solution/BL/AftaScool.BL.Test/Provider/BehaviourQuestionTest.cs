using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AftaScool.BL.Provider.BehaviourData;
using System.Linq;
using System.Text;
using AftaScool.BL.Context;
using AftaScool.BL.Test;
using SoftwareApproach.TestingExtensions;
using System.Diagnostics.CodeAnalysis;

namespace AftaScool.BL.Test.Provider
{
    [TestClass]
    public class BehaviourQuestionTest:ProviderTestBase
    {
        [TestMethod]
        public void TestMethod1()
        {


            var accountUser = SeedData.CreateAdminUser(Context);
            IBehaviourQuestionProvider provider = new BehaviourQuestionProvider(Context, accountUser);
            IBehaviourProvider behaviour = new BehaviourProvider(Context, accountUser);
            var beh = behaviour.saveBehaviour(null, "Rape");


            var info = provider.bquestion(null, beh.Id, -2, 4);


            info.Id.ShouldBeGreaterThan(0);
            info.Id.ShouldNotBeNull();




        }




    }
}
