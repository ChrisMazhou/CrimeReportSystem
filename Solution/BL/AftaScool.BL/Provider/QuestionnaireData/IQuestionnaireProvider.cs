using AftaScool.BL.Entities.QuestionnaireData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AftaScool.BL.Provider.QuestionnaireData
{
    public interface IQuestionnaireProvider : IAftaScoolProvider
    {

        Questionnaire saveQuestionnaire(long? id, long assessorId, long learnerId, DateTime questionnaireDate);

        IQueryable<Questionnaire> GetQuestionnaires();


    }
}
