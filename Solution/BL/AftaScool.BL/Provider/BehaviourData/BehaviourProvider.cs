using AftaScool.BL.Context;
using AftaScool.BL.Entities.Behaviour;
using AftaScool.BL.Entities.SecurityData;
using AftaScool.BL.Provider.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AftaScool.BL.Provider.BehaviourData
{
    public class BehaviourProvider : AftaScoolProvider, IBehaviourProvider
    {



        #region behaviour
        public BehaviourProvider(DataContext context, ICurrentUser currentUser)
            : base(context, currentUser)
        {
        }
        #endregion


        #region SaveBehaviourType

        public Behaviour saveBehaviour(long? id, string type)
        {

            Authenticate(PrivilegeType.BehaviourMaintenance);

            Behaviour info = DataContext.BehaviourSet.Where(a => a.Type == type && a.Id != id).SingleOrDefault();

            if (info != null)
                throw new BehaviourException("Behaviour type : " + type + " already exists.");


            if (id != null && id > 0) //check if we update or insert new behaviour type
            {

                info = DataContext.BehaviourSet.Where(a => a.Id == id).SingleOrDefault();

            }
            else
            {

                info = new Behaviour();
                DataContext.BehaviourSet.Add(info);


            }

            info.Type = type;

            DataContextSaveChanges();

            return info;

        }

        #endregion
        public IQueryable<Behaviour> GetBehaviourTypes()
        {

            var results = from typ in DataContext.BehaviourSet
                          orderby typ.Type
                          select typ;

            return results;

        }
              
        public Behaviour GetBehaviour(long id)
        {
            throw new NotImplementedException();
        }
    }
}