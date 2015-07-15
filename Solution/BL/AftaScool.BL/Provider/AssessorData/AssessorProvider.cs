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

        //Assessor SaveAssessor(long? id, long userId, string emailAddress, string title, string firstName,
         //  string surname, string idOrPassportNumber, string telephone, string addressLine1, string addressLine2, string city, string postalCode)
         // {
         //     if (userId != CurrentUser.Id)
         //         Authenticate(PrivilegeType.AssessorMaintenance);

         //     var saveAssessor = new Assessor;

         //   if (saveAssessor != null && (id == null) || (id == 0))
         //   {
         //       throw new AssessorException("This assessor already exist.");
         //   }

         //   if (id != null && id > 0)
         //   {
         //       if (saveAssessor.Id != id)
         //       {
                    
         //       }
         //   }
         //   else
         //   {
         //       saveAssessor = new Assessor();
         //       saveAssessor.UserIdentities = DataContext.UserIdentitySet.Where(d => d.Id == userId).Single();
         //       saveAssessor.UserIdentityId = userId;
         //       DataContext.AssessorSet.Add(saveAssessor);
         //   }


         //   saveAssessor.UserIdentities.Title = title;
         //   saveAssessor.UserIdentities.FirstName = firstName;
         //   saveAssessor.UserIdentities.Surname = surname;
         //   saveAssessor.UserIdentities.IdPassportNum = idOrPassportNumber;
         //   saveAssessor.UserIdentities.Telephone = telephone;
         //   saveAssessor.UserIdentities.EmailAddress = emailAddress;
         //   saveAssessor.UserIdentities.AddressLine1 = addressLine1;
         //   saveAssessor.UserIdentities.AddressLine2 = addressLine2;
         //   saveAssessor.UserIdentities.City = city;
         //   saveAssessor.UserIdentities.PostalCode = postalCode;

         //   DataContextSaveChanges();

         //   return saveAssessor;
         //}
#endregion 
         
         




        public Assessor createAssessor(long? id, long userId)
        {

            Authenticate(PrivilegeType.AssessorMaintenance);



            Assessor saveAssessor = new Assessor();


            // saveAssessor = DataContext.AssessorSet.Where(a => a.UserIdentityId == userId).SingleOrDefault();

            if (id != null && id > 0)
                saveAssessor = DataContext.AssessorSet.Where(a => a.Id == id).SingleOrDefault();
            else
            {
                saveAssessor = new Assessor();
                DataContext.AssessorSet.Add(saveAssessor);
            }

            saveAssessor.UserIdentityId = userId;
            DataContextSaveChanges();
            return saveAssessor;

        }

        public IQueryable<Assessor> GetAssessor()
        {

            var q = from h in DataContext.AssessorSet
                    orderby h.UserIdentityId
                    select h;

            return q;

        }


        public Assessor GetAssessor(long id)
        {
           //Please implement this method.
            throw new NotImplementedException();
        }
    }
}