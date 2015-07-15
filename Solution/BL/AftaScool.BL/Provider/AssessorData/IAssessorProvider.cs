using AftaScool.BL.Entities.AssessorData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AftaScool.BL.Provider.AssessorData
{
    public interface IAssessorProvider
    {
        #region method definition
        // This method should be Assessor SaveAssessor(long? id, long userId, string emailAddress, string title, string firstName,
        //   string surname, string idOrPassportNumber, string telephone, string addressLine1, string addressLine2, string city, string postalCode);
        //Flip over to the provider to see how it would be implemented.
        #endregion

        Assessor createAssessor(long? id, long userId);

        IQueryable<Assessor> GetAssessor();

        Assessor GetAssessor(long id);

    }
}

