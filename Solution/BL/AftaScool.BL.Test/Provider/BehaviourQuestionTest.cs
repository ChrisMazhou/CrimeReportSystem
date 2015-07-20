using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    [ExcludeFromCodeCoverage]
    public class BehaviourQuestionTest:ProviderTestBase
    {
        [TestMethod]
        [TestCategory("Provider.Behaviour")]
        public void TestBehaviour()
        {
            //SetUp
            var accountUser = SeedData.CreateAdminUser(Context);
            IBehaviourQuestionProvider provider = new BehaviourQuestionProvider(Context, accountUser);
            IBehaviourProvider behaviour = new BehaviourProvider(Context, accountUser);
            var beh = behaviour.saveBehaviour(null, "Rape");

            //Act
            var info = provider.bquestion(null, beh.Id, -2, 4);

            //Test
            info.Id.ShouldNotBeNull();
            info.ShouldNotBeNull();
       }
        [TestMethod]
        [TestCategory("Provider.Behaviour")]
        public void ArchiveBehaviourQuestion()
        {
            //Act
            var accountUser = SeedData.CreateAdminUser(Context);
            IBehaviourQuestionProvider provider = new BehaviourQuestionProvider(Context, accountUser);
            IBehaviourProvider behaviour = new BehaviourProvider(Context, accountUser);
            var beh = behaviour.saveBehaviour(null, "Rape");
            var info = provider.bquestion(null, beh.Id, -2, 4);
            //Act
            provider.ArchiveBehaviour(beh.Id);
            
            info.Id.ShouldBeGreaterThan(0);
            info.Id.ShouldNotBeNull();

            
            

        }




    }
}
