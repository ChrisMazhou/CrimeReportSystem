using AftaScool.BL.Context;
using AftaScool.BL.Entities.AssessorData;
using AftaScool.BL.Entities.SecurityData;
using AftaScool.BL.Provider.Security;
using AftaScool.BL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AftaScool.BL.Provider.AssessorData
{
    public class AssessorProvider : AftaScoolProvider, IAssessorProvider
    {
        #region Ctor

        public AssessorProvider(DataContext context, ICurrentUser currentUser)
            : base(context, currentUser)
        { }
        #endregion

        #region Save Asessor Implementation

        public Assessor SaveAssessor(long? id, long userId, string userName, string password, string emailAddress, string title, string firstName, string surname, string idOrPassportNumber, GenderType gender,
            string telephone, string addressLine1, string addressLine2, string city, string postalCode)
         {

             if (userId != CurrentUser.Id)
                  Authenticate(PrivilegeType.AssessorMaintenance);

            Assessor createAssessor = new Assessor();

             createAssessor = DataContext.AssessorSet.Where(p => p.UserIdentities.PasswordHash == password && p.UserIdentities.UserName == userName).SingleOrDefault();

             if (createAssessor != null && (id == null) || (id == 0))
             {
                 throw new AssessorException("This assessor already exist.");
             }

             if (id != null && id > 0)
             {
                 if (createAssessor.Id != id)
                 {
                     createAssessor = DataContext.AssessorSet.Where(p => p.Id == id).SingleOrDefault();
                 }
             }
             else
             {
                 createAssessor = new Assessor();
                 createAssessor.UserIdentities = DataContext.UserIdentitySet.Where(d => d.Id == userId).Single();
                 createAssessor.UserIdentityId = userId;
                 DataContext.AssessorSet.Add(createAssessor);
             }


             createAssessor.UserIdentities.Title = title;
             createAssessor.UserIdentities.FirstName = firstName;
             createAssessor.UserIdentities.Surname = surname;
             createAssessor.UserIdentities.IdPassportNum = idOrPassportNumber;
             createAssessor.UserIdentities.Telephone = telephone;
             createAssessor.UserIdentities.EmailAddress = emailAddress;
             createAssessor.UserIdentities.AddressLine1 = addressLine1;
             createAssessor.UserIdentities.AddressLine2 = addressLine2;
             createAssessor.UserIdentities.City = city;
             createAssessor.UserIdentities.PostalCode = postalCode;
             createAssessor.UserIdentities.PasswordHash = password;
             createAssessor.UserIdentities.UserName = userName;
             createAssessor.UserIdentities.Gender = gender;
            createAssessor.UserIdentities.Id = userId;

             DataContextSaveChanges();
            return createAssessor;


        }
#endregion 
         public IQueryable<Assessor> GetAssessor()
         {

             var q = from h in DataContext.AssessorSet
                     orderby h.UserIdentities.FirstName
                     select h;

             return q;

         }

        public void GetAssessor(long id)
         {
             Authenticate(PrivilegeType.AssessorMaintenance);

             var assessor = DataContext.AssessorSet.Where(a => a.Id == id).Single();
             DataContextSaveChanges();
             
         }
         

    }
}