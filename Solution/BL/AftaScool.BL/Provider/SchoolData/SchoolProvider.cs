using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AftaScool.BL.Context;
using AftaScool.BL.Types;
using AftaScool.BL.Provider.Security;
using AftaScool.BL.Entities.SchoolData;
using AftaScool.BL.Entities.SecurityData;

namespace AftaScool.BL.Provider.SchoolData
{
    public class SchoolProvider:AftaScoolProvider,ISchoolProvider
    {
        #region school
        public SchoolProvider(DataContext context)
            : base(context, null)
        {
        }
        public SchoolProvider(DataContext context, ICurrentUser currentUser)
            : base(context,currentUser)
        {
        }
        #endregion

        public School SchoolSave(long? id, string schoolName, string addressline1, string addressline2, string city, string postalCode)
        {
            Authenticate(PrivilegeType.SchoolMaintenance);

            School saveSchool = DataContext.SchoolSet.Where(a => a.SchoolName == schoolName && a.Id != id).SingleOrDefault();
            if (saveSchool != null)
                throw new SchoolException("School with the name : " + schoolName + " already exists.");


            if (id != null && id > 0)
                saveSchool = DataContext.SchoolSet.Where(a => a.Id == id).SingleOrDefault();
            else
            {
                saveSchool = new School();
                DataContext.SchoolSet.Add(saveSchool);
            }
             //set attributes
            saveSchool.SchoolName = schoolName;
            saveSchool.AddressLine1 = addressline1;
            saveSchool.AddressLine2 = addressline2;
            saveSchool.City = city;
            saveSchool.PostalCode = postalCode;
            
           

            DataContextSaveChanges();
            return saveSchool;
        }

        public void ArchiveSchool(long id)
        {
            Authenticate(PrivilegeType.SchoolMaintenance);

            var school = DataContext.SchoolSet.Where(a => a.Id == id).Single();
            school.Status = SchoolType.Primary;
            DataContextSaveChanges();
        }

        public IQueryable<School> GetSchools()
        {
            var q = from h in DataContext.SchoolSet
                    orderby h.SchoolName
                    select h;

            return q;
        }

   }
}