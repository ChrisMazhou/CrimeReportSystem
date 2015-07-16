using AftaScool.BL.Context;
using AftaScool.BL.Entities.LearnerData;
using AftaScool.BL.Entities.SecurityData;
using AftaScool.BL.Provider.Security;
using AftaScool.BL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AftaScool.BL.Provider.LearnerData
{
    public class LearnerProvider:AftaScoolProvider,ILearnerProvider
    {


        #region Ctor

        public LearnerProvider(DataContext context, ICurrentUser currentUser)
            : base(context, currentUser)
        { }
        #endregion

        public Learner LearnerSave(long? id, long userId, string learnername, string learnersurname, string grade, string idPassportNum, GenderType gender, string addressLine1, string addressLine2, string city, string postalCode, string telephone)
        {


            Authenticate(PrivilegeType.LearnerMaintenance);

            Learner saveLearner = null;

            saveLearner = DataContext.LearnerSet.Where(a => a.UserIdentities.Id == userId && a.Id != id).SingleOrDefault();


            if (saveLearner != null && saveLearner.UserIdentities.IdPassportNum == idPassportNum)
                throw new LearnerException("Learner with the name : " + learnername + " already exists.");


            if (id != null && id > 0)
                saveLearner = DataContext.LearnerSet.Where(a => a.Id == id).SingleOrDefault();
            else
            {
                saveLearner = new Learner();
                saveLearner.UserIdentities.Active = true ;
                DataContext.LearnerSet.Add(saveLearner);
            }

         
            //set attributes
            saveLearner.UserIdentities.FirstName = learnername;
            saveLearner.UserIdentities.Surname = learnersurname;
            saveLearner.UserIdentities.AddressLine1 = addressLine1;
            saveLearner.UserIdentities.AddressLine2 = addressLine2;
            saveLearner.UserIdentities.City = city;
            saveLearner.UserIdentities.PostalCode = postalCode;
            saveLearner.UserIdentities.Telephone = telephone;
            saveLearner.UserIdentities.Gender = gender;
            saveLearner.UserIdentities.IdPassportNum = idPassportNum;
            saveLearner.UserIdentities.Id = userId;
            saveLearner.Grade = grade;


            DataContextSaveChanges();

            return saveLearner;
        }
       
        public IQueryable<Learner> GetLearners()
        {
            var q = from h in DataContext.LearnerSet
                    orderby h.UserIdentities.FirstName
                    select h;

            return q;
        }

        public void GetLearner(long id)
        {
            Authenticate(PrivilegeType.LearnerMaintenance);

            var learner = DataContext.LearnerSet.Where(a => a.Id == id).Single();
            learner.UserIdentities.Active = true;
            DataContextSaveChanges();


            //return learner;
        }




       
    }
}