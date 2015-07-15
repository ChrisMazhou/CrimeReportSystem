using AftaScool.BL.Entities.QuestionnaireData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AftaScool.BL.Provider.QuestionnaireData
{
    public interface IQuestionnaireQuestionProvider : IAftaScoolProvider
    {

        QuestionnaireQuestion saveQuestion(long? id, long questionnaireId, long behaviourId, string trait);
        IQueryable<QuestionnaireQuestion> getQuestions();

    }
}
