using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftwareApproach.TestingExtensions;
using AftaScool.BL.Test;
using AftaScool.BL.Entities.AssessorData;
using System.Diagnostics.CodeAnalysis;
using AftaScool.BL.Provider.LearnerData;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AftaScool.BL.Provider.AssessorData;
using AftaScool.BL.Provider.SchoolData;

namespace AftaScool.BL.Test.Provider
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class AssessorSchoolProviderTest : ProviderTestBase
    {
        [TestMethod]
        [TestCategory("Provider.AssessorSchool")]
        public void saveAssessorSchool()
        {
            var user = SeedData.CreateAdminUser(Context);
            IAssessorSchoolProvider provider = new AssessorSchoolProvider(Context,user);
            ISchoolProvider skul = new SchoolProvider(Context, user);
            IAssessorProvider _assesor = new AssessorProvider(Context, user);
            var assess = _assesor.createAssessor(null,user.Id );
            var skuul = skul.SchoolSave(null, "Carel De Vet HighSchool", "145 Gamyn Ave", "North", "Vereniging", "0001");
            

            //Act
            var assessorSchool = provider.saveAssessorSchool(null, assess.Id,skuul.Id,DateTime.Now,DateTime.Today.AddDays(6));
          
            //Test
            assessorSchool.ShouldNotBeNull();
           
        }

        [TestMethod]
        [TestCategory("Provider.Learner")]
        public void assessorSchoolListTest()
        {
            var user = SeedData.CreateAdminUser(Context);
            IAssessorSchoolProvider provider = new AssessorSchoolProvider(Context, user);
            IAssessorProvider _assesor = new AssessorProvider(Context, user);
            ISchoolProvider skul = new SchoolProvider(Context, user);
            var assess = _assesor.createAssessor(null, user.Id);
            var skuul = skul.SchoolSave(null, "Carel De Vet HighSchool", "145 Gamyn Ave", "North", "Vereniging", "0001");
            var skuul2 = skul.SchoolSave(null, "piet", "145 Gamyn Ave", "North", "Vereniging", "0001");

            var assessorSchool = provider.saveAssessorSchool(null, assess.Id, skuul.Id, DateTime.Now, DateTime.Today);
            var assessorSchool2 = provider.saveAssessorSchool(null, assess.Id, skuul2.Id, DateTime.Now, DateTime.Today);

            //Act
            var z = provider.GetAssessorSchool().Count();

            //Test
           // z.ShouldEqual(2);
        }



    }
}