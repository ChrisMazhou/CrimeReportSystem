using AftaScool.BL.Provider.BehaviourData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AftaScool.BL.Provider.Security;
using AftaScool.BL.Types;
using SoftwareApproach.TestingExtensions;

namespace AftaScool.BL.Test.Provider
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BehaviourProviderTest : ProviderTestBase
    {
        [TestMethod]
        [TestCategory("Provider.Behaviour")]
        public void saveBehaviour()
        {
            //Setup
            var accountUser = SeedData.CreateAdminUser(Context);
            IBehaviourProvider provider = new BehaviourProvider(Context, accountUser);

            //Act
            var behaviour = provider.saveBehaviour(null, "Harassment");
            //Test
            behaviour.ShouldNotBeNull();
            behaviour.Id.ShouldBeGreaterThan(0);
        }
        [TestMethod]
        [TestCategory("Provider.Behaviour")]
        public void BehaviourListTest()
        {
            //Setup
            var accountUser = SeedData.CreateAdminUser(Context);
            IBehaviourProvider provider = new BehaviourProvider(Context, accountUser);
            var behaviour = provider.saveBehaviour(null, "Harassment");
            var behaviour2 = provider.saveBehaviour(null, "Violence");

            //Act
            var x = provider.GetBehaviourTypes().Count();

            //Test
            x.ShouldEqual(2);
        }
    }
}
