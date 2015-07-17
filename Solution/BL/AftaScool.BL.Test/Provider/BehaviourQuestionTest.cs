using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AftaScool.BL.Provider.BehaviourData;

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





        }
    }
}
