using AftaScool.BL.Entities.Behaviour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AftaScool.BL.Provider.BehaviourData
{
    public interface IBehaviourQuestionProvider:IAftaScoolProvider
    {

        BehaviourQuestion bquestion(long? id, long behaviourId, double minimumWeighting, double maximumWeighting);

        BehaviourQuestion GetBehavior(long id);



    }
}
