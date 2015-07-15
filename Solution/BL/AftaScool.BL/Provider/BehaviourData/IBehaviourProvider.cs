using AftaScool.BL.Entities.Behaviour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AftaScool.BL.Provider.BehaviourData
{
    public interface IBehaviourProvider:IAftaScoolProvider
    {


        Behaviour saveBehaviour(long? id, string type);

        IQueryable<Behaviour> GetBehaviourTypes();





    }
}
