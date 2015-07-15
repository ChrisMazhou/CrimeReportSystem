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

        public Learner LearnerSave(long? id, string learnername, string learnersurname, string grade, string idPassportNum, GenderType gender, string addressLine1,string addressLine2, string city, string postalCode, string telephone)
        {


            Authenticate(PrivilegeType.LearnerMaintenance);

            Learner saveLearner = null;

            saveLearner = DataContext.LearnerSet.Where(a => a.LearnerName == learnername && a.Id != id).SingleOrDefault();


            if (saveLearner != null && saveLearner.IdPassportNum==idPassportNum)
                throw new LearnerException("Learner with the name : " + learnername + " already exists.");


            if (id != null && id > 0)
                saveLearner = DataContext.LearnerSet.Where(a => a.Id == id).SingleOrDefault();
            else
            {
                saveLearner = new Learner();
                saveLearner.Status = StatusType.Active;
                DataContext.LearnerSet.Add(saveLearner);
            }


            //set attributes
            saveLearner.LearnerName = learnername;
            saveLearner.LearnerName = learnersurname;
            saveLearner.Grade = grade;
            saveLearner.AddressLine1 = addressLine1;
            saveLearner.AddressLine2 = addressLine2; 
            saveLearner.City = city;
            saveLearner.PostalCode = postalCode;
            saveLearner.Telephone = telephone ;
            saveLearner.Gender = gender;
            saveLearner.IdPassportNum = idPassportNum;


            DataContextSaveChanges();

            return saveLearner;
        }
        public void ArchiveLearner(long id)
        {
            Authenticate(PrivilegeType.LearnerMaintenance);

            var client = DataContext.LearnerSet.Where(a => a.Id == id).Single();
            client.Status = StatusType.Archive;

            DataContextSaveChanges();
        }

        public IQueryable<Learner> GetLearners()
        {
            var q = from h in DataContext.LearnerSet
                    orderby h.LearnerName
                    select h;

            return q;
        }

     

        

        
    }
}