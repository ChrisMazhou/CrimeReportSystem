using AftaScool.BL.Entities.AssessorData;
using AftaScool.BL.Entities.SecurityData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AftaScool.BL.Provider.AssessorData
{
    public interface IAssessorProvider:IAftaScoolProvider
    {
        #region method definition
        Assessor SaveAssessor(long? id, long userId, string userName, string password, string emailAddress, string title, string firstName, string surname, string idOrPassportNumber, GenderType gender, string telephone, string addressLine1, string addressLine2, string city, string postalCode);
        #endregion

        IQueryable<Assessor> GetAssessor();

        void GetAssessor(long id);

    }
}

