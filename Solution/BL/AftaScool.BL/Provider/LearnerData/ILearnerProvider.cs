using AftaScool.BL.Entities.LearnerData;
using AftaScool.BL.Entities.SecurityData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AftaScool.BL.Provider.LearnerData
{
    public interface ILearnerProvider : IAftaScoolProvider
    {

        Learner LearnerSave(long? id, long userId, string learnername, string learnersurname, string grade, string idPassportNum, GenderType gender, string addressLine1, string addressLine2,
             string city, string postalCode, string telephone);


        IQueryable<Learner> GetLearners();

        void GetLearner(long id);


    }
}
